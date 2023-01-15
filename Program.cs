using static QuizzMaker.UI;
using static QuizzMaker.Logic;

namespace QuizzMaker;

class Program
{
    public const double MAX_SCORE = 100.0;
    public const double PASSING_SCORE = 85.0;
    public const string ANSWERS_KEYS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    // used for orderly answers display and as a key to each answer from user's input 

    static void Main(string[] args)
    {
        Random rnd = new Random();
        PrintIntro();      
        char userChoice = PrintLoadFromStorageGetAnswer();
        // chice to open existing saved file in local storage or create new file
        var questionsList = new List<Question>();

        if (userChoice == 'Q')
        // Q - create new Test; T - load existing file from local 
        {
            while (userChoice == 'Q')
            {
                var newQuestion = new Question();
                newQuestion.question = GetQuestionTitle();
                newQuestion.answers = GetAllAnswers();
                questionsList.Add(newQuestion);
                userChoice = PrintContinueChoises();
                // user's desision: create another question||continue
            }

            SaveListToExternalXml(questionsList);
            userChoice = PrintStartTestChoises();
            //user's desision if to start test or exit

            if (userChoice != 'Y')
            {
                return;
            }
        }

        questionsList = RetrieveDataFromXml();

        if (questionsList.Count > 0)
        {
            PrintTestKnowledgeIntro();
            PrintPressAnyKeyToContinue();
            double correctAnswerScore = 100 / questionsList.Count;
            double score = 0.0;
            int questionNumberCounter = 1;

            while (questionsList.Count > 0)
            {
                int selectedRandomQuestion = rnd.Next(questionsList.Count);

                PrintQuestion(questionNumberCounter, questionsList[selectedRandomQuestion].question);
                questionNumberCounter++;

                var orderedAnswers = OrderAnswersAndKeys(questionsList[selectedRandomQuestion].answers);
                //create list of ordered answers
                var correctAnswers = GetCorrectAnswersList(orderedAnswers);
                // get correct answers to compare input
                var cleanAnswersForPrint = CleanAnswers(orderedAnswers);
                // clean list from hints for printing

                PrintAnswers(cleanAnswersForPrint);

                string answer = UserEntryValidation();
                // get answers for this question

                bool answeredCorrectly = CheckUserAnswers(answer, correctAnswers);
                //check if answer is in correct answers list, score/not score count

                if (answeredCorrectly)
                {
                    score += correctAnswerScore;
                }

                questionsList.Remove(questionsList[selectedRandomQuestion]);
                //remove answered question from list to display

            }

            PrintPassFail(score);
        }
        else
        {
            NoFileMessage();
        }

        return;
    }
}
