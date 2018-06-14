using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour {

    public Text question;
    public TextAsset xmlRawFileQuestion;
    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;
    public GameObject answer4;
    public Text correctAnswer;
    public static int currentQuestionIndex = 0;
    public QuestionObject[] questionArray;
    public Boolean buttonPressed;
    public static int rightAnswerCounter = 0;
    public static int wrongAnswerCounter = 0;


    // Beim Start wird erst überprüft, ob das Array leer ist. Falls es leer ist, wird die Funktion parseXmlFile aufgerufen, die aus der XML Datei die Fragen ausliest und in Fragenobjekte umwandelt.
    void Start() {
        if (LoadXmlFileQuestions.questionArray[0] == null)
        {
            string questionData = xmlRawFileQuestion.text;
            LoadXmlFileQuestions.parseXmlFile(questionData);
        }
        questionArray = LoadXmlFileQuestions.questionArray;

    }

    // Jedes Frame wird überprüft, ob das Array leer is, um die Fragen beim Szenenwechsel erneut einzulesen. Falls ja, werden die Fragen aus der XML Datei eingelesen und in Fragenobjekte umgewandelt
    // Falls nicht wird die aktuelle Frage eingelesen und auf die Arraygrenze geachtet.
    private void Update()
    {
        
        if (LoadXmlFileQuestions.questionArray[0] == null)
        {
            string questionData = xmlRawFileQuestion.text;
            LoadXmlFileQuestions.parseXmlFile(questionData);
        }
        else if (currentQuestionIndex < LoadXmlFileQuestions.arraySize)
        {
            question.GetComponentInChildren<Text>().text = questionArray[currentQuestionIndex].question;
            answer1.GetComponent<TextMesh>().text = questionArray[currentQuestionIndex].answer1;
            answer2.GetComponent<TextMesh>().text = questionArray[currentQuestionIndex].answer2;
            answer3.GetComponent<TextMesh>().text = questionArray[currentQuestionIndex].answer3;
            answer4.GetComponent<TextMesh>().text = questionArray[currentQuestionIndex].answer4;
     
        }
        else if ( currentQuestionIndex == LoadXmlFileQuestions.arraySize)
        {
         string result = "Sie haben " + QuestionCanvas.rightAnswerCounter + " Fragen richtig beantwort und " + QuestionCanvas.wrongAnswerCounter + " Fragen falsch beantwortet";
            question.GetComponentInChildren<Text>().text = result;
        }
    }


}
