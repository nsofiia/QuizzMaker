using System;
namespace QuizzMaker
{
    public class UI
    {
        
        public static void EnterTitleMessage()
        {
            Console.WriteLine("\nEnter the question following the question mark at the end (?)");
        }

        public static void ErrorTitleMessage()
        {
            Console.WriteLine("Question must contain more than 6 characters and a question mark (?)");
        }

        public static void EnterAnswersMessage()
        {
            Console.WriteLine("Enter at least 2 answers, each answer separated with coma. Add an asterisk symbol (*) to indicate the correct answers");
        }

        public static void ErrorAnswersMessage()
        {
            Console.WriteLine("Enter at least 2 comma separated answers, add at least 1 correct answer, indicated with asteriks (*)");
        }

        public static void Intro()
        {
            Console.WriteLine("Test knowledge by creating a quizz!\n");
        }

        public static char Continue()
        {
            Console.WriteLine("Add another question? Y - to add another, any other key to continue");
            char oneMore = Char.ToUpper(Console.ReadKey().KeyChar);
            return oneMore;
        }
    }
}

