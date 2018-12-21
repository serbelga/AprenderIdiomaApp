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
        //Current topic
        private string currentTopic = "";
        //Correct and answered cuestions
        private int correct = 0;
        private int answered = 0;
        //Current Question Index
        private int questionIndex = 0;
        //Level difficulty
        private int difficultyLevel = 1;
        //Result dialog is shown
        private Boolean resultsDialog = false;
        
        private List<string> currentQuestions = new List<string>();
        //Animals List
        private string[] animalsList = new string[] { "perro", "gato", "loro", "caballo", "rana", "canario", "leon", "leopardo" };
        //Colors List
        private string[] colorsList = new string[] { "negro", "blanco", "rojo", "azul", "naranja", "morado", "amarillo", "verde" };
        //AnimalsColorsList
        private string[] animalsColorsList = new string[] { "gato_negro", "loro_rojo", "canario_amarillo", "perro_blanco" };

        private Dictionary<string, string[]> topicsAnswers = new Dictionary<string, string[]>();
        
        public Form1()
        {
            InitializeComponent();
            InitializeTopics();
            pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.globe;
            this.Load += Form1_Load;
        }

        /**
         * Diccionario de Topicos y respuestas para generar las opciones cuando baja la dificultad
         */ 
        private void InitializeTopics()
        {
            topicsAnswers.Add("animales", animalsList);
            topicsAnswers.Add("colores", colorsList);
            topicsAnswers.Add("animales_colores", animalsColorsList);
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
         * Grammar creation
         */ 
        private Grammar CreateGrammarBuilderSemantics(object p)
        {
            //1. Close application
            GrammarBuilder close = "Cierra";
            GrammarBuilder close1 = "Cerrar";
            GrammarBuilder exit = "Salir de";
            Choices closeCh = new Choices(close, exit);
            GrammarBuilder application = "la aplicacion";
            GrammarBuilder closeApplication = new GrammarBuilder(closeCh);
            closeApplication.Append(application);

            //2. Select topics
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

            //3. Select topic extended
            GrammarBuilder wantQuestionsExtended = wantCh;
            wantQuestionsExtended.Append(questionsCh);
            wantQuestionsExtended.Append(topics);
            wantQuestionsExtended.Append("y");
            semanticResultKey = new SemanticResultKey("topic2", topicsCh);
            topics = new GrammarBuilder(semanticResultKey);
            wantQuestionsExtended.Append(topics);
            Choices select = new Choices(wantQuestions, wantQuestionsExtended);

            //4. Begin again
            GrammarBuilder beginAgain = "Empezar de nuevo";

            //5. Need help
            GrammarBuilder needHelp = "Necesito ayuda";

            //Answers
            //6. Multiple answers
            GrammarBuilder animalsColorsAnswers = new GrammarBuilder("Este animal es un");

            Choices animalsCh = new Choices();
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
            for(int i = 0; i < colorsList.Length; i++)
            {
                semanticResultValue = new SemanticResultValue(colorsList[i], colorsList[i]);
                resultValueBuilder = new GrammarBuilder(semanticResultValue);
                colorsCh.Add(resultValueBuilder);
            }
            semanticResultKey = new SemanticResultKey("colors", colorsCh);
            GrammarBuilder colors = new GrammarBuilder(semanticResultKey);
            animalsColorsAnswers.Append(colors);
            
            //7. Animals answer
            GrammarBuilder animalsAnswers = "Este animal es un";
            animalsAnswers.Append(animals);

            //8. Colors answer
            GrammarBuilder colorsAnswers = "Este color es el";
            colorsAnswers.Append(colors);

            //9. Next Question
            Choices nextQuestion = new Choices("pregunta siguiente", "siguiente pregunta");

            //10. Increase and decrease difficulty
            GrammarBuilder increaseDifficulty = "aumentar dificultad";
            GrammarBuilder decreaseDifficulty = "disminuir dificultad";
            Choices difficulty = new Choices(increaseDifficulty, decreaseDifficulty);
            Choices choices = new Choices(beginAgain, needHelp, closeApplication, select, nextQuestion, animalsAnswers, colorsAnswers, animalsColorsAnswers, difficulty);
            Grammar grammar = new Grammar(choices);
            
            //Grammar name
            grammar.Name = "Questions";
            return grammar;
        }

        #region Recognizer
        private void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SemanticValue semantics = e.Result.Semantics;
            string text = e.Result.Text;

            //Orders
            if (text.Contains("Cierra") || e.Result.Text.Contains("Salir") || e.Result.Text.Contains("Cerrar"))
            {
                CloseApplication();
            }
            else if (text.Contains("siguiente"))
            {
                NextCuestion();
            }
            else if (text.Contains("ayuda"))
            {
                NeedHelp();
            }
            else if (text.Contains("Empezar"))
            {
                BeginAgain();
            }
            else if (text.Contains("y"))
            {
                if (resultsDialog)
                {
                    return;
                }
                currentQuestions = new List<string>();
                //Control order
                if (semantics["topic1"].Value.ToString().Equals("animales") && semantics["topic2"].Value.ToString().Equals("colores"))
                {
                    currentTopic = "animales_colores";
                    currentQuestions.AddRange(animalsColorsList);
                    UpdateCuestion();
                }
                else if (semantics["topic1"].Value.ToString().Equals("colores") && semantics["topic2"].Value.ToString().Equals("animales"))
                {
                    currentTopic = "animales_colores";
                    currentQuestions.AddRange(animalsList);
                    UpdateCuestion();
                }
                else if (semantics["topic1"].Value.ToString().Equals(semantics["topic2"].Value.ToString()))
                {
                    ShowError();
                }
            }
            else if (semantics.ContainsKey("topic1"))
            {
                if (resultsDialog)
                {
                    return;
                }
                currentQuestions = new List<string>();
                if (semantics["topic1"].Value.ToString().Equals("animales"))
                {
                    currentTopic = "animales";
                    currentQuestions.AddRange(animalsList);
                    UpdateCuestion();
                }
                else if (semantics["topic1"].Value.ToString().Equals("colores"))
                {
                   
                    currentTopic = "colores";
                    currentQuestions.AddRange(colorsList);
                    UpdateCuestion();
                }
            }
            else if (text.Contains("de color"))
            {
                if (resultsDialog)
                {
                    return;
                }
                if (currentTopic.Equals("animales_colores")) CheckCorrect(semantics["animals"].Value.ToString() + "_" + semantics["colors"].Value.ToString());
            }

            else if (text.Contains("Este animal es un"))
            {
                if (resultsDialog)
                {
                    return;
                }
                //Check if the topic is animales
                if (currentTopic.Equals("animales")) CheckCorrect(semantics["animals"].Value.ToString());

            }
            else if (text.Contains("Este color es el"))
            {
                if (resultsDialog)
                {
                    return;
                }
                //Check if the topic is colores
                if (currentTopic.Equals("colores")) CheckCorrect(semantics["colors"].Value.ToString());
            }
            else if (text.Contains("dificultad"))
            {
                if (text.Contains("aumentar"))
                {
                    if(difficultyLevel < 2)
                    {
                        difficultyLevel++;
                        SetQuestionStatement();
                    }
                } else
                {
                    if(difficultyLevel > 0)
                    {
                        difficultyLevel--;
                        SetQuestionStatement();
                    }
                }
            }
        }
        #endregion

        #region Actions
        /**
         * Empezar de nuevo
         */
        private void BeginAgain()
        {
            //Variables a valores iniciales
            this.difficultyLevel = 1;
            this.resultsDialog = false;
            this.correctNumber.Text = "0";
            this.correct = 0;
            this.answered = 0;
            this.questionNumber.Text = "0";
            this.questionIndex = 0;
            //Reset texto de la pregunta
            this.questionStatement.Text = "Choose a question topic";
            //Imagen de bienvenida
            pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.globe;
            this.currentTopic = "";
            //this.currentQuestions = new List<Question1>();
            this.currentQuestions = new List<string>();
        }

        

        /**
         * Checks if question is correct
         */ 
        private void CheckCorrect(String text)
        {
            if (text.Contains(GetCurrentAnswer()))
            {
                correct++;
                this.correctNumber.Text = correct.ToString();
                synth.Speak("Correcta");
            } else
            {
                synth.Speak("Incorrecta");
            }
            //this.currentQuestions.RemoveAt(questionIndex);
            this.currentQuestions.RemoveAt(questionIndex);
            this.answered++;
            NextCuestion();
        }


        private void UpdateCuestion()
        {
            Random rnd = new Random();
            questionIndex = rnd.Next(0, currentQuestions.Count);
            
            
            if (questionNumber.Text.Equals("0"))
            {
                questionNumber.Text = "1";
            }
            SetQuestionStatement();
            SetImage();
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
            if (this.currentQuestions.Count == 0)
            {
                ShowResult();
                return;
            }
            //Modificar el índice de la cuestion
            this.UpdateCuestion();
            //Modificar el número de pregunta
            int aux = int.Parse(this.questionNumber.Text) + 1;
            this.questionNumber.Text = aux.ToString();
        }

        /**
         * Muestra el resultado cuando no quedan más preguntas
         */ 
        private void ShowResult()
        {
            this.options.Text = "";
            if(this.correct == 0 || this.answered / this.correct >= 2)
            {
                pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.very_dissatisfied;
                this.questionStatement.Text = "Vuelve a intentarlo";
            } else
            {
                pictureBox1.Image = AprenderIdiomaApp.Properties.Resources.very_satisfied;
                this.questionStatement.Text = "Muy bien";
            }
            resultsDialog = true;
        }

        /**
         * Exit from Application
         */ 
        private void CloseApplication()
        {
            System.Windows.Forms.Application.Exit();
        }

        /**
         * Muestra mensaje de error
         */ 
        private void ShowError()
        {
            this.questionStatement.Text = "You must choose different topics";
        }

        /**
         * Modifica la imagen
         */ 
        private void SetImage()
        {
            string answer = this.GetCurrentAnswer();
            pictureBox1.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(answer);
        }

        /**
         * Necesito ayuda
         */ 
        private void NeedHelp()
        {
            if (currentTopic.Length == 0)
            {
                this.questionStatement.Text = "You must choose a topic";
                return;
            }
            string answer = this.GetCurrentAnswer();
            synth.Speak("La palabra empieza por la letra " + answer.Substring(0,1));
        }

        /**
         * Obtiene la respuesta actual
         */ 
        private string GetCurrentAnswer()
        {
            string answer = currentQuestions[questionIndex];
            return answer;
        }

        /**
         * Actualizar parte inferior de opciones
         * Cuando se aumenta o disminuye la dificultad se ofrecen las posibles respuestas al usuario
         */ 
        private void SetQuestionStatement()
        {
            if (currentTopic.Length == 0)
            {
                this.questionStatement.Text = "You must choose a topic";
                return;
            }
            if (resultsDialog)
            {
                this.questionStatement.Text = "You must begin again";
                return;
            }
            this.options.Text = "";
            switch (difficultyLevel)
            {
                case 0:
                    //Genera una lista de cuatro opciones con la respuesta actual y
                    //los muestra de forma aleatoria
                    List<String> options = new List<string>();
                    string[] answers = topicsAnswers[currentTopic];
                    options.Add(GetCurrentAnswer());
                    while (options.Count < 4)
                    {
                        Random rnd = new Random();
                        int index = rnd.Next(0, answers.Length);
                        if (!options.Contains(answers[index]))
                        {
                            options.Add(answers[index]);
                        }
                    }
                    while (options.Count != 0)
                    {
                        Random rnd = new Random();
                        int index = rnd.Next(0, options.Count);
                        this.options.Text += " " + options[index];
                        options.RemoveAt(index);
                    }
                    break;
                case 1:
                    if (currentTopic.Equals("animales"))
                    {
                        this.questionStatement.Text = "Este animal es un _________";
                    }
                    else if (currentTopic.Equals("colores"))
                    {
                        this.questionStatement.Text = "Este color es el __________";
                    }
                    else if (currentTopic.Equals("animales_colores"))
                    {
                        this.questionStatement.Text = "Este animal es un __________ de color __________";
                    }
                    this.options.Text = "";
                    break;
                case 2:
                    if (currentTopic.Equals("animales"))
                    {
                        this.questionStatement.Text = "Translate: This animal is a _________";
                    }
                    else if (currentTopic.Equals("colores"))
                    {
                        this.questionStatement.Text = "Translate: This color is __________";
                    }
                    else if (currentTopic.Equals("animales_colores"))
                    {
                        this.questionStatement.Text = "Translate: This animal is a __________ of colour __________";
                    }
                    this.options.Text = "";
                    break;
            }
        }
        #endregion

        private void questionStatement_Click(object sender, EventArgs e)
        {

        }
    }
}
