using System;
using static QuizzMaker.UI;

namespace QuizzMaker
{
    public class Question
    {
        string title;
        string allAnswers;



        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="qTitle"></param>
        /// <param name="qAnswers"></param>
        public Question()
        {
            EnterTitleMessage();
            string qTitle = Console.ReadLine();

            while (!qTitle.Contains("?") || qTitle.Length < 6)
            {
                ErrorTitleMessage();
                qTitle = Console.ReadLine();
            }

            EnterAnswersMessage();
            string answers = Console.ReadLine();

            while (!answers.Contains(",") || !answers.Contains("*"))
            {
                ErrorAnswersMessage();
                answers = Console.ReadLine();
            }

        }



    }
}

