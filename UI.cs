using System;
namespace QuizzMaker
{
    public class UI
    {       
        public static void PrintIntro()
        {
            Console.WriteLine("Test knowledge!\n");
        }


        public static char PrintLoadOrCreate()
        {
            string validInput = "QT";
            Console.WriteLine($"Create new quiz: {validInput[0]}\nLoad saved test: {validInput[1]}\n");

            char userAnswer = Char.ToUpper(Console.ReadKey().KeyChar);
            while (!validInput.Contains(userAnswer))
            {
                Console.WriteLine($"\nchoose {validInput[0]} or {validInput[1]} to continue\n");
                userAnswer = Char.ToUpper(Console.ReadKey().KeyChar);
            }
            return userAnswer;
        }




        public static void PrintEnterTitleMessage()
        {
            Console.WriteLine("\nEnter the question, following the question mark at the end (?)\n");
        }

        public static void PrintErrorTitleMessage()
        {
            Console.WriteLine("\nQuestion must contain more than 6 characters and a question mark (?)\n");
        }

        public static void PrintEnterAnswersMessage()
        {
            Console.WriteLine("\nEnter 1 answer at a time and press Enter. Add an asterisk symbol (*) to indicate the correct answers\n");
        }

        public static void PrintErrorAnswersMessage()
        {
            Console.WriteLine("\nEnter at least 2 answers, separated with coma; add at least 1 correct answer, indicated with asteriks (*)\n");
        }


        /// <summary>
        /// get title string
        /// </summary>
        /// <returns></returns>
        public static string GetQuestionTitle()
        {
            PrintEnterTitleMessage();
            string qTitle = Console.ReadLine();

            while (!qTitle.Contains('?') || qTitle.Length < 6)
            {
                PrintErrorTitleMessage();
                qTitle = Console.ReadLine();
            }
            return qTitle;
        }


        public static void PrintEmptyError()
        {
            Console.WriteLine("Answer can not be empty\n");
        }


        public static void PrintAnswerSavedStatus(char choice)
        {
            switch (choice)
            {
                case 'B':
                    Console.WriteLine("Enter answer again:\n");
                    break;
                case 'Y':
                    Console.WriteLine("\nALL answers recorded!\n");
                    break;
                default:
                    Console.WriteLine("Saved answer\n");
                    break;

            }
        }

        public static void PrintAllAnswerSavedMessage()
        {
            Console.WriteLine("\nALL answers recorded!\n");
        }


        public static void PrintAddAnotherAnswer()
        {
            Console.WriteLine("ANSWER: Add another? y - to add one more answer, any other key to save and continue\n");

        }

        public static void PrintAnswerIsNotMarkedError()
        {
            Console.WriteLine("\nnot marked as correct answer, would you like to go back? Press B to rewrite the answer, press any other key to save and continue\n");
        }

        public static char PrintContinueChoises()
        {
            Console.WriteLine("\nQUESTION: Add another? Q - to add another, any other key to continue\n");
            //save and exit
            char oneMore = Char.ToUpper(Console.ReadKey().KeyChar);
            return oneMore;
        }

        public static char PrintStartTestChoises()
        {
            Console.WriteLine("\nReady to test your knowledge? \ny - start the test; \nany other key to save and exit \n");
            char test = Char.ToUpper(Console.ReadKey().KeyChar);
            return test;
        }


        public static void PrintTestKnowledgeIntro()
        {
            Console.Clear();
            Console.WriteLine("Intro:\nFor each randomly selected question you will be presented with a list of answers. \nEnter all the letters that are corresponding with correct answers.\n");
        }


        public static void PrintPressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue\n");
            Console.ReadKey();

        }


        public static void PrintQuestion(int numberOfQuestion, string question)
        {
            Console.WriteLine("Question " + numberOfQuestion + ":\n" + question);
        }


        public static void PrintAnswers(List<string> orderedAndCleaned)
        {
            foreach (string item in orderedAndCleaned)
            {
                {
                    Console.WriteLine(item);
                }
            }
            Console.Write(">");
        }




        public static void PrintPassFail(double presentScore)
        {
            if (presentScore >= Program.PASSING_SCORE)
            {
                Console.WriteLine("You passed!");
            }
            else
            {
                Console.WriteLine("Didn't pass.");
            }
            Console.WriteLine("Test exit :)");
        }



        public static void PrintScore(double presentScore, double maxScore)
        {
            Console.WriteLine($"All questions are completed with score: {presentScore}% out of {maxScore}%");
        }

    }
}

