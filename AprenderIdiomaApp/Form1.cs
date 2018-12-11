using AprenderIdiomaApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprenderIdiomaApp
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        private SpeechSynthesizer synth = new SpeechSynthesizer();
        private Question[] questions;
        private int questionIndex;
        public Form1()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            InitializeQuestions();
            this.Load += Form1_Load;
        }

        private void InitializeQuestions()
        {
            Topic topic = new Topic("animales", "", "perro");
            Topic topic1 = new Topic("colores", "", "blanco");
            Topic[] topics = new Topic[2];
            topics[0] = topic;
            topics[1] = topic1;
            Question question = new Question(topics);

            questions = new Question[1];
            questions[0] = question;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            synth.Speak("Inicializando la aplicación");
            Grammar grammar = CreateGrammarBuilderSemantics(null);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            _recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 40);
            grammar.Enabled = true;
            _recognizer.LoadGrammar(grammar);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            synth.Speak("Aplicación preparada para reconocer su voz");
        }

        private void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SemanticValue semantics = e.Result.Semantics;
            if (e.Result.Text.Contains("Cerrar") || e.Result.Text.Contains("Salir"))
            {
                System.Windows.Forms.Application.Exit();
            } else if (e.Result.Text.Contains("siguiente")) {
                int aux = int.Parse(this.question.Text) + 1;
                this.question.Text = aux.ToString();
            } else if (e.Result.Semantics.ContainsKey("topics")) {
                if (semantics["topics"].Value.ToString().Equals("animales"))
                {
                    this.questionStatement.Text = "Este animal es un _________";
                }
            } else if (e.Result.Text.Contains("Este animal es un"))
            {
                Topic topic = questions[questionIndex].getTopics()[0];
                if (e.Result.Text.Contains(topic.getResponse())) this.questionStatement.Text = "correct";
                else this.questionStatement.Text = "incorrect";
            }
        }

        private Grammar CreateGrammarBuilderSemantics(object p)
        {
            //Close Application
            GrammarBuilder close = "Cerrar";
            GrammarBuilder exit = "Salir de";
            Choices closeCh = new Choices(close, exit);
            GrammarBuilder application = "la aplicacion";
            GrammarBuilder closePhrase = new GrammarBuilder(closeCh);
            closePhrase.Append(application);

            //Select questions
            Choices topicsCh = new Choices();
            GrammarBuilder want = "Quiero cuestiones de";
            SemanticResultValue semanticResultValue = new SemanticResultValue("animales", "animales");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(semanticResultValue);
            topicsCh.Add(resultValueBuilder);
            SemanticResultKey semanticResultKey = new SemanticResultKey("topics", topicsCh);
            GrammarBuilder topics = new GrammarBuilder(semanticResultKey);
            GrammarBuilder selectPhrase = want;
            selectPhrase.Append(topics);

            //Begin again
            GrammarBuilder begin = "Quiero empezar de nuevo";

            //Help
            GrammarBuilder help = "Necesito ayuda";


            GrammarBuilder animals = "Este animal es un";
            GrammarBuilder dog = "perro";
            GrammarBuilder cat = "gato";
            Choices animalsCh = new Choices(dog, cat);

            animals.Append(animalsCh);

          
            
            Choices next = new Choices(new GrammarBuilder("Pregunta siguiente"));


            Choices opciones = new Choices(begin, help, closePhrase, selectPhrase, next, animals);
            Grammar grammar = new Grammar(opciones);
            
            grammar.Name = "Questions";
            return grammar;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
