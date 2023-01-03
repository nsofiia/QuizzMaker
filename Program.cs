using static QuizzMaker.UI;
using System.Xml.Serialization;

namespace QuizzMaker;
class Program
{
    static void Main(string[] args)
    {
        Intro();
        char newQ = 'Y';
        var questionsList = new List<Question>();

        while(newQ == 'Y')
        {
            var question = new Question();
            questionsList.Add(question);
            newQ = Continue();
        }

        XmlSerializer createXml = new XmlSerializer(typeof(List<Question>));

        var path = @"/Users/sofi/Documents/ListOfQuestions.xml";
        using (FileStream file = File.Create(path))
        {
            createXml.Serialize(file, questionsList);
        }

        //save list of questions

        //start playing
        // retrieve the list from saved
        //get random question
        //remove selected question from the list 
        //create list of ordered answers
        // get correct answers
        // clean list frm hints
        //display clean answers
        // get input
        // compare input with correct answes
        // score
        //repeat
        //end when no more questions left
    }


}

