using System;
using static QuizzMaker.UI;

namespace QuizzMaker
{
    public class Question
    {
        public string title;
        public string allAnswers;




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


        /// <summary>
        /// get all answers
        /// </summary>
        /// <returns></returns>
        public static string getAllAnswers()
        {
            PrintEnterAnswersMessage();
            string answers = Console.ReadLine();

            while (!answers.Contains(',') || !answers.Contains(','))
            {
                PrintErrorAnswersMessage();
                answers = Console.ReadLine();
            }
            return answers;
        }

    }
}

