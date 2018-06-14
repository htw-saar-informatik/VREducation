using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class LoadXmlFileQuestions  {


    public Text correctAnswer;
    public Text questionText;
    public static QuestionObject[] questionArray;
    public static int arraySize = 0;



  
    void Start()
    {
        
    }

    // Funktion zum Einlesen der Fragen inklusive 4 Antwortmöglichkeiten und einer korrekten Antwort aus der XML-Datei. Es wird in ein Fragenobjekt umgewandelt und in einem Array gespeichert
   public static void parseXmlFile(string xmlData)
    {
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        arraySize = 0;
        questionArray = new QuestionObject[100];
        string xmlPathPattern = "//QuestionCollection/Question";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {
                
            XmlNode questionText = node.FirstChild;
            XmlNode answer1 = questionText.NextSibling;
            XmlNode answer2 = answer1.NextSibling;
            XmlNode answer3 = answer2.NextSibling;
            XmlNode answer4 = answer3.NextSibling;
            XmlNode correctAnswer = answer4.NextSibling;
            string questionTextStr = questionText.InnerXml;
            string answer1Str = answer1.InnerXml;
            string answer2Str = answer2.InnerXml;
            string answer3Str = answer3.InnerXml;
            string answer4Str = answer4.InnerXml;
            string correctAnswerStr = correctAnswer.InnerXml;
            QuestionObject question = new QuestionObject(questionTextStr, answer1Str, answer2Str, answer3Str, answer4Str,correctAnswerStr);
            questionArray[arraySize] = question;
            Debug.Log(question.question+ question.answer1);
            Debug.Log(questionArray[arraySize].question + questionArray[arraySize].answer1 + questionArray[arraySize].answer2 + questionArray[arraySize].answer3 + questionArray[arraySize].answer4 + questionArray[arraySize].correctAnswer);
            arraySize++;
            
        }
        Debug.Log("Arraysize:" + arraySize);
    }
}
