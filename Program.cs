using static QuizzMaker.UI;
using static QuizzMaker.Logic;
using System.Xml.Serialization;

namespace QuizzMaker;
class Program
{
    const double MAX_SCORE = 100.0;

    static void Main(string[] args)
    {
        XmlSerializer xml = new XmlSerializer(typeof(List<Question>));
        Random rnd = new Random();

        var questionsList = new List<Question>();
        PrintIntro();
        char createNewQuestion = PrintQuestionsChoise(); // chice to open existing or create new

        if (createNewQuestion == 'Q')
        {
            while (createNewQuestion == 'Q')
            {
                var question = new Question();
                question.title = Question.GetQuestionTitle();
                question.allAnswers = Question.getAllAnswers();
                questionsList.Add(question);
                createNewQuestion = PrintContinueChoises(); //user's desision: create another question||continue
            }

            SaveListToExternalXml(xml, questionsList);
            char startTest = PrintStartTestChoises(); //user's desision if to start test or exit

            if (startTest != 'Y')
            {
                return;
            }
            else
            {
                createNewQuestion = 'T';
            }
        }
        else
        {
            PrintTestKnowledgeIntro();
            PrintPressAnyKeyToContinue();
            questionsList = RetrieveDataFromXml(xml, questionsList);

            if(questionsList.Count < 1)
            {
                Console.WriteLine("No questions saved, restart the app and create new test");
                Console.ReadKey();
                return;
            }

            double oneCorrectAnswer = 100 / questionsList.Count; //getting worth in % on corect answer to the question 
            double score = 0.0;
            int questionNumberCounter = 1;

            while (questionsList.Count > 0)
            {
                Console.Clear();
                int randomQuestion = rnd.Next(questionsList.Count);

                PrintQuestion(questionNumberCounter, questionsList[randomQuestion].title);
                questionNumberCounter++;

                var orderedAnswers = AnswersAddOrder(questionsList[randomQuestion].allAnswers);//create list of ordered answers
                var correctAnswers = GetCorrectAnswersList(orderedAnswers);  // get correct answers to compare input
                var cleanAnswersForPrint = CleanAnswers(orderedAnswers); // clean list from hints for printing

                PrintAnswers(cleanAnswersForPrint);

                string answer = (Console.ReadLine()).ToUpper(); // get answers

                int answeredCorrectlyTimes = CheckUserAnswers(answer, correctAnswers);
                //check if answer is in correct answers list, score/not score count

                questionsList.Remove(questionsList[randomQuestion]); //remove answered question from list of questions to display

                score += oneCorrectAnswer * answeredCorrectlyTimes;

            }
            PrintScore(score, MAX_SCORE);
            PrintPressAnyKeyToContinue();
            return;
        }


    }
}
