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
        private int correct = 0;
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
            string[][] responses = { new string[]{ "perro", "rojo", "gato_negro" }, new string[]{ "gato", "amarillo", "gato_negro" }, new string[] { "rana", "verde", "canario_amarillo" } };
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
            //Multiple Answers
            
            GrammarBuilder animalsColorsAnswers = new GrammarBuilder("Este animal es un");

            Choices animalsCh = new Choices();
            string[] animalsList = new string[] { "perro", "gato", "pajaro", "caballo", "rana", "canario" };
            for (int i = 0; i < animalsList.Length; i++)
            {
                semanticResultValue = new SemanticResultValue(animalsList[i], animalsList[i]);
                resultValueBuilder = new GrammarBuilder(semanticResultValue);
                animalsCh.Add(resultValueBuilder);
            }
            semanticResultKey = new SemanticResultKey("animals", animalsCh);
            GrammarBuilder animals = new GrammarBuilder(semanticResultKey);
            animalsColorsAnswers.Append(animals);

            animalsColorsAnswers.Append(new GrammarBuilder("de color"));


            Choices colorsCh = new Choices();
            string[] colorsList = new string[] { "negro", "blanco", "rojo", "azul", "naranja", "morado", "amarillo", "verde" };
            for(int i = 0; i < colorsList.Length; i++)
            {
                semanticResultValue = new SemanticResultValue(colorsList[i], colorsList[i]);
                resultValueBuilder = new GrammarBuilder(semanticResultValue);
                colorsCh.Add(resultValueBuilder);
            }
            semanticResultKey = new SemanticResultKey("colors", colorsCh);
            GrammarBuilder colors = new GrammarBuilder(semanticResultKey);
            animalsColorsAnswers.Append(colors);
            
            //Animals Answers
            GrammarBuilder animalsAnswers = "Este animal es un";
            animalsAnswers.Append(animals);

            //Colors Answers
            GrammarBuilder colorsAnswers = "Este color es el";
            colorsAnswers.Append(colors);

            //Next question
            Choices nextQuestion = new Choices("pregunta siguiente", "siguiente pregunta");

        
            Choices opciones = new Choices(beginAgain, needHelp, closeApplication, select, nextQuestion, animalsAnswers, colorsAnswers, animalsColorsAnswers);
            Grammar grammar = new Grammar(opciones);
            
            grammar.Name = "Questions";
            return grammar;
        }

        #region Recognizer
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
                //Control order
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
                    ShowError();
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
                }
                else if (semantics["topic1"].Value.ToString().Equals("colores"))
                {
                    this.questionStatement.Text = "Este color es el _________";
                    currentTopic = "colores";
                    int currentQuestion = questionIndex + 1;
                    this.questionNumber.Text = currentQuestion.ToString();
                    SetImage();
                }
            }
            else if (text.Contains("de color"))
            {
                if (currentTopic.Equals("animales_colores")) CheckCorrect(semantics["animals"].Value.ToString() + "_" + semantics["colors"].Value.ToString());
            }

            else if (text.Contains("Este animal es un"))
            {
                //Check if the topic is animales
                if (currentTopic.Equals("animales")) CheckCorrect(semantics["animals"].Value.ToString());

            }
            else if (text.Contains("Este color es el"))
            {
                //Check if the topic is colores
                if (currentTopic.Equals("colores")) CheckCorrect(semantics["colors"].Value.ToString());
            }
            
            
        }
        #endregion

        #region Actions
        /**
         * Restarts the questions serie 
         */
        private void BeginAgain()
        {
            //Numbers to 0
            this.correctNumber.Text = "0";
            this.correct = 0;
            this.questionNumber.Text = "0";
            this.questionIndex = 0;
            //Reset the question text
            this.questionStatement.Text = "Choose a question topic";
            //Presentation image
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
            //Last question? Show Result
            if (this.questionNumber.Text.Equals("3"))
            {
                ShowResult();
                return;
            }
            //Modify questions index
            this.questionIndex++;
            SetImage();
            //Modify questions label
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
                correct++;
                this.correctNumber.Text = correct.ToString();
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
            //Show results window
            if(this.correct == 0)
            {
                pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.very_dissatisfied;
            } else
            {
                pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.very_satisfied;
            }
            this.questionStatement.Text = "final";
        }

        /**
         * Exit from Application
         */ 
        private void CloseApplication()
        {
            System.Windows.Forms.Application.Exit();
        }

        /**
         * Shows an error message
         */ 
        private void ShowError()
        {
            this.questionStatement.Text = "Error";
        }

        /**
         * Sets the current question image
         */ 
        private void SetImage()
        {
            int topicIndex = topics[currentTopic];
            String url = questions[questionIndex].getTopics()[topicIndex].getUrl();
            pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(url);
        }
        #endregion


    }
}
