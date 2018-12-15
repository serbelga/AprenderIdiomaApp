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
        private string currentTopic = "";
        private Dictionary<string, int> topics = new Dictionary<string, int>();
        private int questionIndex;
        public Form1()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            InitializeQuestions();
            InitializeTopics();
            pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.globe;
            this.Load += Form1_Load;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        /**
         * Questions Creation
         */
        private void InitializeQuestions()
        {
            string[][] responses = { new string[]{ "perro", "rojo", "gato_negro" }, new string[]{ "gato", "amarillo", "gato_negro" }, new string[] { "pajaro", "verde", "gato_negro" } };
            string[] topics = { "animales", "colores", "animales_colores" };
            questions = new Question[responses.Length];
            for (int i = 0; i < responses.Length; i++)
            {
                Topic[] t = new Topic[topics.Length];
                for(int j = 0; j < topics.Length; j++)
                {
                    Topic topic = new Topic(topics[j], topics[j] + "_" + i, responses[i][j]);
                    t[j] = topic;
                }
                questions[i] = new Question(t);
            }
        }

        private void InitializeTopics()
        {
            topics.Add("animales", 0);
            topics.Add("colores", 1);
            topics.Add("animales_colores", 2);
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            synth.Speak("Inicializando la aplicación");
            Grammar grammar = CreateGrammarBuilderSemantics(null);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            _recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);
            grammar.Enabled = true;
            _recognizer.LoadGrammar(grammar);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            synth.Speak("Aplicación preparada para reconocer su voz");
        }

        /**
         * Grammar Creation
         */ 
        private Grammar CreateGrammarBuilderSemantics(object p)
        {
            //Close Application
            GrammarBuilder close = "Cierra";
            GrammarBuilder exit = "Salir de";
            Choices closeCh = new Choices(close, exit);
            GrammarBuilder application = "la aplicacion";
            GrammarBuilder closeApplication = new GrammarBuilder(closeCh);
            closeApplication.Append(application);

            //Select Topics
            GrammarBuilder want = "Quiero";
            GrammarBuilder give = "Dame";
            Choices wantCh = new Choices(want, give);
            GrammarBuilder issues = "Cuestiones de";
            GrammarBuilder questions = "Preguntas de";
            Choices questionsCh = new Choices(issues, questions);

            SemanticResultValue semanticResultValue = new SemanticResultValue("animales", "animales");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(semanticResultValue);
            Choices topicsCh = new Choices();
            topicsCh.Add(resultValueBuilder);
            semanticResultValue = new SemanticResultValue("colores", "colores");
            resultValueBuilder = new GrammarBuilder(semanticResultValue);
            topicsCh.Add(resultValueBuilder);
            SemanticResultKey semanticResultKey = new SemanticResultKey("topic1", topicsCh);
            GrammarBuilder topics = new GrammarBuilder(semanticResultKey);
            GrammarBuilder wantQuestions = wantCh;
            wantQuestions.Append(questionsCh);
            wantQuestions.Append(topics);

            //Select Topics Extended
            GrammarBuilder wantQuestionsExtended = wantCh;
            wantQuestionsExtended.Append(questionsCh);
            wantQuestionsExtended.Append(topics);
            wantQuestionsExtended.Append("y");
            semanticResultKey = new SemanticResultKey("topic2", topicsCh);
            topics = new GrammarBuilder(semanticResultKey);
            wantQuestionsExtended.Append(topics);
            Choices select = new Choices(wantQuestions, wantQuestionsExtended);
            

            //Begin again
            GrammarBuilder beginAgain = "Empezar de nuevo";

            //Help
            GrammarBuilder needHelp = "Necesito ayuda";

            //Answers

            //Animals Answers
            GrammarBuilder animals = "Este animal es un";


            GrammarBuilder dog = "perro";
            GrammarBuilder cat = "gato";
            GrammarBuilder bird = "pajaro";
            Choices animalsCh = new Choices(dog, cat, bird);

            animals.Append(animalsCh);


            //Colors Answers
            GrammarBuilder colors = "Este color es el";


            GrammarBuilder red = "rojo";
            GrammarBuilder yellow = "amarillo";
            GrammarBuilder green = "verde";
            Choices colorsCh = new Choices(red, yellow, green);
            colors.Append(colorsCh);

            Choices next = new Choices(new GrammarBuilder("Pregunta siguiente"));


            Choices opciones = new Choices(beginAgain, needHelp, closeApplication, select, next, animals, colors);
            Grammar grammar = new Grammar(opciones);
            
            grammar.Name = "Questions";
            return grammar;
        }

        /**
         * Restarts the questions serie 
         */ 
        private void BeginAgain()
        {
            //Cuestiones Label a 0
            this.correctNumber.Text = "0";
            this.questionNumber.Text = "0";
            //Índice de cuestiones a 0
            this.questionIndex = 0;
            //Reset Texto de pregunta a Bienvenida
            this.questionStatement.Text = "Choose a question topic";
            //Eliminar imagen
            pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.globe;
            this.currentTopic = "";
        }

        /**
         * Passes to the next cuestion
         */ 
        private void NextCuestion()
        { 
            if (currentTopic.Length == 0)
            {
                this.questionStatement.Text = "You must choose a topic";
                return;
            }
            //Última pregunta? Mostrar resultado
            if (this.questionNumber.Text.Equals("3"))
            {
                ShowResult();
                return;
            }
            //Modificar Índice de cuestiones
            this.questionIndex++;
            SetImage();
            //Modificar label cuestiones
            int aux = int.Parse(this.questionNumber.Text) + 1;
            this.questionNumber.Text = aux.ToString();
        }

        /**
         * Checks if question is correct
         */ 
        private void CheckCorrect(String text)
        {
            int topicIndex = topics[currentTopic];
            Topic topic = questions[questionIndex].getTopics()[topicIndex];
            if (text.Contains(topic.getResponse()))
            {
                int aux = int.Parse(this.correctNumber.Text) + 1;
                this.correctNumber.Text = aux.ToString();
            }
            NextCuestion();
        }


        private void UpdateCuestion()
        {
            //Ver topico
            //Montar pregunta
        }

        private void ShowResult()
        {
            //Mostrar ventana resultado
            this.questionStatement.Text = "final";
        }

        /**
         * Exit from Application
         */ 
        private void CloseApplication()
        {
            System.Windows.Forms.Application.Exit();
        }

        private void showError()
        {
            this.questionStatement.Text = "Error";
        }


        private void SetImage()
        {
            int topicIndex = topics[currentTopic];
            String url = questions[questionIndex].getTopics()[topicIndex].getUrl();
            pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(url);
        }

        private void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SemanticValue semantics = e.Result.Semantics;
            string text = e.Result.Text;
            
            //Orders
            if (text.Contains("Cierra") || e.Result.Text.Contains("Salir"))
            {
                CloseApplication();
            }
            else if (text.Contains("siguiente"))
            {
                NextCuestion();   
            }
            else if (text.Contains("Empezar"))
            {
                BeginAgain();
            }
            else if (text.Contains("y"))
            {
                //controlar orden
                if (semantics["topic1"].Value.ToString().Equals("animales") && semantics["topic2"].Value.ToString().Equals("colores"))
                {
                    this.questionStatement.Text = "Este animal es un _________ de color _________";
                    currentTopic = "animales_colores";
                    int currentQuestion = questionIndex + 1;
                    this.questionNumber.Text = currentQuestion.ToString();
                    SetImage();
                } 
                else if (semantics["topic1"].Value.ToString().Equals("colores") && semantics["topic2"].Value.ToString().Equals("animales"))
                {
                    this.questionStatement.Text = "Este animal es un _________ de color _________";
                    currentTopic = "animales_colores";
                    int currentQuestion = questionIndex + 1;
                    this.questionNumber.Text = currentQuestion.ToString();
                    SetImage();
                }
                else if (semantics["topic1"].Value.ToString().Equals(semantics["topic2"].Value.ToString()))
                {
                    showError();
                }
            }
            else if (semantics.ContainsKey("topic1"))
            {
                if (semantics["topic1"].Value.ToString().Equals("animales"))
                {
                    this.questionStatement.Text = "Este animal es un _________";
                    currentTopic = "animales";
                    int currentQuestion = questionIndex + 1;
                    this.questionNumber.Text = currentQuestion.ToString();
                    SetImage();
                } else if (semantics["topic1"].Value.ToString().Equals("colores"))
                {
                    this.questionStatement.Text = "Este color es el _________";
                    currentTopic = "colores";
                    int currentQuestion = questionIndex + 1;
                    this.questionNumber.Text = currentQuestion.ToString();
                    SetImage();
                }
            }


            else if (text.Contains("Este animal es un"))
            {
                //Check if the topic is animales
                if (currentTopic.Equals("animales")) CheckCorrect(text);

            }
            else if (text.Contains("Este color es el"))
            {
                //Check if the topic is animales
                if (currentTopic.Equals("colores")) CheckCorrect(text);
            }
        }
    }
}
