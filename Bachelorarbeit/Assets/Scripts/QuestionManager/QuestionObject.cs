using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;


public class QuestionObject {

   
    public string question { get; set; }
    
    public string answer1 { get; set; }
    
    public string answer2 { get; set; }
  
    public string answer3 { get; set; }
    
    public string answer4 { get; set; }
   
    public string correctAnswer { get; set; }

    // Konstruktor des Frageobjekts
    public QuestionObject(string question, string answer1, string answer2, string answer3, string answer4, string correctAnswer)
    {
        this.question = question;
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer3 = answer3;
        this.answer4 = answer4;
        this.correctAnswer = correctAnswer;
    }
}
