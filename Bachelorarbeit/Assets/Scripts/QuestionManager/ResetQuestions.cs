using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetQuestions : MonoBehaviour {

    public Text correctAnswer;

    void OnTriggerEnter(Collider col)

    {
        QuestionCanvas.wrongAnswerCounter = 0;
        QuestionCanvas.rightAnswerCounter = 0;
        QuestionCanvas.currentQuestionIndex = 0;
        correctAnswer.GetComponentInChildren<Text>().text = "";
    }
}
