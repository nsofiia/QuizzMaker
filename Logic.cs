using System;
using static QuizzMaker.UI;
using System.Xml.Serialization;

namespace QuizzMaker
{
    public class Logic
    {
        const string PATH = @"/Users/sofi/Documents/ListOfQuestions.xml";
        const string ANSWERS_KEYS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //used for orderly answers display and as a key to each answer frokm user's input 
        static readonly XmlSerializer xml = new XmlSerializer(typeof(List<Question>));



        /// <summary>
        /// get all answers
        /// </summary>
        /// <returns></returns>
        public static List<string> getAllAnswers()
        {
            PrintEnterAnswersMessage();
            List<string> AnswerList = new List<string>();
            char oneMoreAnswer = 'Y';
            while (oneMoreAnswer == 'Y')
            {
                string entry = Console.ReadLine();

                while (string.IsNullOrEmpty(entry) || string.IsNullOrWhiteSpace(entry))
                {
                    PrintEmptyError();
                    entry = Console.ReadLine();
                }
                if (!entry.Contains('*'))
                {
                    PrintAnswerIsNotMarkedError();
                    char saveOrnot = Char.ToUpper(Console.ReadKey().KeyChar);
                    if (saveOrnot == 'B')
                    {
                        PrintAnswerSavedStatus(saveOrnot);
                        entry = Console.ReadLine();
                    }
                    PrintAnswerSavedStatus(saveOrnot);
                }
                AnswerList.Add(entry);
                PrintAddAnotherAnswer();
                oneMoreAnswer = Char.ToUpper(Console.ReadKey().KeyChar);
            }
            if (oneMoreAnswer != 'Y')
            {
                PrintAllAnswerSavedMessage();
            }
            return AnswerList;
        }


        /// <summary>
        /// save datat to xml
        /// </summary>
        /// <param name="path">pathe where file created on the local machine</param>
        /// <param name="fileXml"></param>
        /// <param name="list"></param>
        public static void SaveListToExternalXml(List<Question> list)
        {
            using (FileStream file = File.Create(PATH)) //write list of questions into the external file.xml
            {
                xml.Serialize(file, list);
            }
        }

        /// <summary>
        /// retrieve data from xml
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <param name="fileXml"></param>
        /// <param name="list"></param>
        public static List<Question> RetrieveDataFromXml()
        {
            List<Question> list = new List<Question>();

            try
            {
                using (FileStream file = File.OpenRead(PATH)) // retrieve the list from saved xml
                {
                    list = xml.Deserialize(file) as List<Question>;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("No saved tests found");
            }

            return list;
        }




        /// <summary>
        /// prepend a letter to each answer
        /// </summary>
        /// <param name="allAs"></param>
        /// <returns></returns>
        public static List<string> OrderAnswersAndKeys(List<string> allAns)
        {
            List<string> outputOrderedAnswers = new List<string>();

            int i = 0;
            foreach (string answer in allAns)
            {
                string ans = ANSWERS_KEYS[i] + ". " + allAns[i].Trim();
                outputOrderedAnswers.Add(ans);
                i++;
            }
            return outputOrderedAnswers;
        }


        /// <summary>
        /// create a list of correct answers (that contain *)
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static List<char> GetCorrectAnswersList(List<string> answers)
        {
            List<string> rawAnswers = answers;
            List<char> correct = new List<char>();

            foreach (string item in rawAnswers)
            {
                if (item.Contains('*'))
                {
                    correct.Add(item[0]);
                }
            }
            return correct;
        }


        /// <summary>
        ///used for display of answers to user, removing hints
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static List<string> CleanAnswers(List<string> answers)
        {
            List<string> cleanAnswers = new List<string>();

            foreach (string answer in answers)
            {

                string cleaned = answer.Trim('*');
                cleanAnswers.Add(cleaned);

            }
            return cleanAnswers;
        }



        /// <summary>
        /// checks how many correct answers was given in the input
        /// </summary>
        /// <param name="userAns"></param>
        /// <param name="correctList"></param>
        /// <returns></returns>
        public static bool CheckUserAnswers(string userAns, List<char> correctList)
        {
            bool answeredCorrect = false;

            for (int i = 0; i < correctList.Count; i++) //comparing letters in the user answer with letters in the correct list
            {
                if (userAns.Contains(correctList[i]))
                {
                    answeredCorrect = true;
                }
            }

            return answeredCorrect;
        }



    }
}

