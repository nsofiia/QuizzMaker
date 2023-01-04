using static QuizzMaker.UI;
using static QuizzMaker.Logic;
using System.Xml.Serialization;

namespace QuizzMaker;
class Program
{
    const string PATH = @"/Users/sofi/Documents/ListOfQuestions.xml";
    const double MAX_SCORE = 100.0;

    static void Main(string[] args)
    {
        XmlSerializer xml = new XmlSerializer(typeof(List<Question>));
        Random rnd = new Random();

        var questionsList = new List<Question>();
        char createNewQuestion = 'Y';

        PrintIntro();

        while (createNewQuestion == 'Y')
        {
            var question = new Question();
            question.title = Question.GetQuestionTitle();
            question.allAnswers = Question.getAllAnswers();
            questionsList.Add(question);
            createNewQuestion = PrintContinueChoises(); //user's desision: create another question||continue
        }

        SaveListToExternalXml(PATH, xml, questionsList);

        char test = PrintStartTestChoises(); //user's desision if to start test or exit

        if (test != 'Y')
        {
            return;
        }
        else
        {
            PrintTestKnowledgeIntro();
            PrintPressAnyKeyToContinue();
            RetrieveDataFromXml(PATH, xml, questionsList);

            double oneCorrectAnswer = 100 / questionsList.Count; //getting worth in % on corect answer to the question 
            double score = 0.0;

            while (questionsList.Count > 0)
            {
                int randomQuestion = rnd.Next(questionsList.Count);
                int questionNumber = 1;

                Console.WriteLine("Question " + questionNumber + ":\n" + questionsList[randomQuestion].title);
                Console.WriteLine("Enter the letter that is corresponding with correct answer:\n*side note: questions can have more than 1 correct answer, separate each answer with coma before submitting\n");

                var orderedAnswers = AnswersAddOrder(questionsList[randomQuestion].allAnswers);//create list of ordered answers
                var correctAnswers = GetCorrectAnswersList(orderedAnswers);  // get correct answers to compare input
                var cleanAnswers = CleanAnswers(orderedAnswers); // clean list from hints for printing

                foreach (string item in orderedAnswers)
                {
                    Console.WriteLine(item);
                }
                Console.Write(">");

                string answer = (Console.ReadLine()).ToUpper(); // get input

                int answeredCorrectlyTimes = CheckUserAnswers(answer, cleanAnswers); //check if answer is in correct answers list, score/not score count

                //analyze the answer
                //ask to reenter if contains numbers, symbols, anything but letters and commas




                questionNumber++;
                questionsList.Remove(questionsList[randomQuestion]);
                //if correct
                score += oneCorrectAnswer * answeredCorrectlyTimes;
                //else score stays the same
            }
            Console.WriteLine($"All questions are completed, score: {score.ToString("P1")}");
            PrintPressAnyKeyToContinue();
            return;
            //end , show score, ask if want to take the test again
        }


    }
}
