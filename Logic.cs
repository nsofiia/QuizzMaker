using System;
using System.IO;
using System.Xml.Serialization;

namespace QuizzMaker
{
	public class Logic
	{





        /// <summary>
        /// save datat to xml
        /// </summary>
        /// <param name="path">pathe where file created on the local machine</param>
        /// <param name="fileXml"></param>
        /// <param name="list"></param>
        public static void SaveListToExternalXml(string path, XmlSerializer fileXml, List<Question> list)
		{
            using (FileStream file = File.Create(path)) //write list of questions into the external file.xml
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
        public static void RetrieveDataFromXml(string path, XmlSerializer fileXml, List<Question> list)
        {
            using (FileStream file = File.OpenRead(path)) // retrieve the list from saved xml
            {
                list = fileXml.Deserialize(file) as List<Question>;
            }
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
                string ans = ch[i] + ". " + splittedAnswers[i].Trim();
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
        public static List<string> GetCorrectAnswersList(List<string> answers)
        {
            List<string> rawAnswers = answers;
            List<string> correctAnswers = new List<string>();

            foreach (string item in rawAnswers)
            {
                if (item.Contains('*'))
                {
                    correctAnswers.Add(item);
                }
            }
            return correctAnswers;
        }


        /// <summary>
        /// removing all '*' from list of strings
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static List<string> CleanAnswers(List<string> answers)
        {
            List<string> rawAnswers = new List<string> { "***string", "*str*", "*i", "..." };
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





        public static int CheckUserAnswers(string userAns, List<string> correctList)
        {
            int correctAnswerCount = 0;
            string[] userA = userAns.Split(',');

            for(int i = 0; i < correctList.Count; i++)
            {
                for(int j = 0; j < userA.Length; j++)
                {
                    if (correctList[i].Contains(userA[j]))
                    {
                        correctAnswerCount++;
                    }
                }
            }

            return correctAnswerCount;
        }

    }
}

