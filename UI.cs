using System;
namespace QuizzMaker
{
    public class UI
    {
        
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
            Console.WriteLine("\nEnter at least 2 answers, each answer separated with coma. Add an asterisk symbol (*) to indicate the correct answers\n");
        }

        public static void PrintErrorAnswersMessage()
        {
            Console.WriteLine("\nEnter at least 2 comma separated answers, add at least 1 correct answer, indicated with asteriks (*)\n");
        }

        public static void PrintIntro()
        {
            Console.WriteLine("Test knowledge by creating a quizz!\n");
        }

        public static char PrintContinueChoises()
        {
            Console.WriteLine("\nAdd another question? Y - to add another, any other key to continue\n");
            //save and exit
            char oneMore = Char.ToUpper(Console.ReadKey().KeyChar);
            return oneMore;
        }

        public static char PrintStartTestChoises()
        {
            Console.WriteLine("\nRun test? Y - to start, any other key to SAVE and EXIT\n");
            char test = Char.ToUpper(Console.ReadKey().KeyChar);
            return test;
        }

        public static void PrintTestKnowledgeIntro()
        {
            Console.Clear();
            Console.WriteLine("\nTest is about to start!\nFor each randomly selected question you will be presented with a list of answers. Enter the letters that correspond with the correct answers.\n");
        }

        public static void PrintPressAnyKeyToContinue()
        {
            Console.WriteLine("\n\nWhen you are ready press any key to continue\n");
            Console.ReadKey();

        }
    }
}

