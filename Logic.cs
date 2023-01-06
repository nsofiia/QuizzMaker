using System;
using System.IO;
using System.Xml.Serialization;

namespace QuizzMaker
{
    public class Logic
    {
        const string PATH = @"/Users/sofi/Documents/ListOfQuestions.xml";

        /// <summary>
        /// save datat to xml
        /// </summary>
        /// <param name="path">pathe where file created on the local machine</param>
        /// <param name="fileXml"></param>
        /// <param name="list"></param>
        public static void SaveListToExternalXml(XmlSerializer fileXml, List<Question> list)
        {
            using (FileStream file = File.Create(PATH)) //write list of questions into the external file.xml
            {
                fileXml.Serialize(file, list);
            }
        }

        /// <summary>
        /// retrieve data from xml
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <param name="fileXml"></param>
        /// <param name="list"></param>
        public static List<Question> RetrieveDataFromXml(XmlSerializer fileXml, List<Question> list)
        {
            try {
                using (FileStream file = File.OpenRead(PATH)) // retrieve the list from saved xml
                {
                    list = fileXml.Deserialize(file) as List<Question>;
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
        public static List<string> AnswersAddOrder(string allAns)
        {
            string allA = allAns;
            string[] splittedAnswers = allA.Split(',');
            string ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //order is alphabetical
            List<string> outputOrderedAnswers = new List<string>()
;
            int i = 0;
            foreach (string answer in splittedAnswers)
            {
                if (answer.Length == 1 && answer.Contains(' '))
                {
                    continue;
                }
                else
                {
                    string ans = ch[i] + ". " + splittedAnswers[i].Trim();
                    outputOrderedAnswers.Add(ans);
                }
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
                    for(int i = 0; i < 2; i++)
                    {
                        correct.Add(item[i]);
                    }
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
            List<string> rawAnswers = answers;
            List<string> cleanAnswers = new List<string>();

            foreach (string answer in rawAnswers)
            {
                if (answer.Contains('*'))
                {
                    string clean = answer.Trim('*');
                    cleanAnswers.Add(clean);
                }
                else
                {
                    cleanAnswers.Add(answer);
                }
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
        public static int CheckUserAnswers(string userAns, List<char> correctList)
        {
            int correctAnswerCount = 0;

            for (int i = 0; i < correctList.Count; i++)
            {
                for (int j = 0; j < userAns.Length; j++)
                {
                    if (correctList[i] == userAns[j])
                    {
                        correctAnswerCount++;
                    }
                }
            }
            if(correctAnswerCount > 1)   //untill I come up with multiple answers suppport
            {
                correctAnswerCount = 1;
            }
            return correctAnswerCount;
        }



    }
}

