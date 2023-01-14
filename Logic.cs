using System;
using static QuizzMaker.UI;
using System.Xml.Serialization;

namespace QuizzMaker
{
    public class Logic
    {
        const string PATH = @"/Users/sofi/Documents/ListOfQuestions.xml";
        static readonly XmlSerializer xml = new XmlSerializer(typeof(List<Question>));


        public static string UserEntryValidation()
        {
            string entry = Console.ReadLine();

            while (string.IsNullOrEmpty(entry) || string.IsNullOrWhiteSpace(entry))
            {
                PrintEmptyError();
                entry = Console.ReadLine();
            }
            return entry;
        }


        /// <summary>
        /// get all answers
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllAnswers()
        {
            PrintEnterAnswersMessage();
            string entry = UserEntryValidation();

            while (!entry.Contains('*') || !entry.Contains(','))
            {
                PrintAnswerError();
                entry = UserEntryValidation();
            }

            List<string> AnswerList = new List<string>();
            string[] arrayOfAnswers = entry.Split(',');

            foreach (string answer in arrayOfAnswers)
            {
                if (string.IsNullOrEmpty(answer) || string.IsNullOrWhiteSpace(answer))
                {
                    continue;
                }
                AnswerList.Add(answer);
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
            try
            {
                using (FileStream file = File.Create(PATH)) //write list of questions into the external file.xml
                {
                    xml.Serialize(file, list);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
            List<string> orderedAnswersList = new List<string>();

            int i = 0;
            foreach (string answer in allAns)
            {
                string answerAndKey = Program.ANSWERS_KEYS[i] + ". " + allAns[i].Trim();
                orderedAnswersList.Add(answerAndKey);
                i++;
            }
            return orderedAnswersList;
        }


        /// <summary>
        /// create a list of correct answers (that contain *)
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static List<char> GetCorrectAnswersList(List<string> answers)
        {
            List<char> correctOnes = new List<char>();

            foreach (string answer in answers)
            {
                if (answer.Contains('*'))
                {
                    correctOnes.Add(answer[0]); // getting only Keys of correct answers located in ANSWERS_KEYS
                }
            }
            return correctOnes;
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
                if (answer.Contains('*'))
                {
                    List<char> answerParsed = new List<char>();

                    foreach (char letter in answer)
                    {
                        if(letter != '*')
                        {
                            answerParsed.Add(letter);
                        }                        
                    }

                    string cleaned = string.Concat(answerParsed);                   
                    cleanAnswers.Add(cleaned);
                }
                else
                {
                    cleanAnswers.Add(answer);
                }
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

