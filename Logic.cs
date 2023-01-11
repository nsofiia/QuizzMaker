using System;
using System.Xml.Serialization;

namespace QuizzMaker
{
    public class Logic
    {
        const string PATH = @"/Users/sofi/Documents/ListOfQuestions.xml";
        const string ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static readonly XmlSerializer xml = new XmlSerializer(typeof(List<Question>));

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
        public static List<string> AnswersAddOrder(List<string> allAns)
        {
            List<string> outputOrderedAnswers = new List<string>();

            int i = 0;
            foreach (string answer in allAns)
            {
                string ans = ch[i] + ". " + allAns[i].Trim();
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
        /// removing all '*' from list of strings
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static List<string> CleanAnswers(List<string> answers)
        {
            List<string> cleanAnswers = new List<string>();

            foreach (string answer in answers)
            {

                string clean = answer.Trim('*');
                cleanAnswers.Add(clean);

            }
            return cleanAnswers;
        }




        //method for answer input check



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

