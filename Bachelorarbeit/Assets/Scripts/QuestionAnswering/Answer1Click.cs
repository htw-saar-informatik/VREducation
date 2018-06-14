using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer1Click : MonoBehaviour
{

    public GameObject answer;
    public GameObject textholderAnswer;
    public Text correctAnswer;
    public QuestionObject[] questionArray;
    public Boolean clickAllowed = true;


    // Funktion wird aufgerufen, sobald Kollision zwischen Oculus Touch Controllern und dem Button erkannt wurde
    void OnTriggerEnter(Collider col)
    {
        if (clickAllowed == true)
        {
            {
                //Überprüft, ob die letzte Frage erreicht wurde
                if (QuestionCanvas.currentQuestionIndex < LoadXmlFileQuestions.arraySize)
                {
                    // Lädt das QuestionObject
                    questionArray = LoadXmlFileQuestions.questionArray;

                    // Prüft auf falsche Antwort
                    if (!textholderAnswer.GetComponent<TextMesh>().text.Equals(questionArray[QuestionCanvas.currentQuestionIndex].correctAnswer))
                    {
                        correctAnswer.text = "Falsche Antwort! Die richtige Antwort war " + questionArray[QuestionCanvas.currentQuestionIndex].correctAnswer;
                        QuestionCanvas.currentQuestionIndex++;
                        QuestionCanvas.wrongAnswerCounter++;
                        StartCoroutine(WaitingThreshhold());
                    }
                    // Prüft auf richtige Antwort
                    if (textholderAnswer.GetComponent<TextMesh>().text.Equals(questionArray[QuestionCanvas.currentQuestionIndex].correctAnswer))
                    {
                        QuestionCanvas.rightAnswerCounter++;
                        correctAnswer.text = "Richtige Antwort!";
                        QuestionCanvas.currentQuestionIndex++;
                        StartCoroutine(WaitingThreshhold());
                    }


                }
            }
        }
    }
    IEnumerator WaitingThreshhold()
    {
        clickAllowed = false;
        yield return new WaitForSeconds(1.5F);
        clickAllowed = true;
    }
}
