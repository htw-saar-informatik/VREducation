using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFieldManager : MonoBehaviour {

    public Text questionText;
    public Text answerText;
    public Text counterText;
    public int triesLeft;
    private string allLocationsAnswered = "Sie haben alle Fragen beantwortet";

    // Hier wird zur Berechnung der wirklich falsch getätigten Eingaben eine Zusatzvariable benötigt, da jede Berührung mit dem Globus als falscher Eintrag zählt, auch wenn darunter der Kollider  
    // eines momentan gesuchten Lands liegt und die Anzahl der Länder somit noch subtrahiert werden muss
    private int realWrongCounter = 0;



	// Jedes Frame wird die momentan gesuchte Umgebung und die Anzahl der übrigen Versuchen bestimmt und angezeigt
	void Update () {
        triesLeft = 5 - LocationManager.wrongCounter;              // Anzahl der übrigen Versuche wird berechnet
        string currentQuestion = LoadXmlFileLocations.locationArray[LocationManager.currentLocation];          // Momentan gesuchter Ort wird geladen
        questionText.GetComponent<Text>().text = currentQuestion;                                              // Momentan gesuchter Ort wird als Text angezeigt
        counterText.GetComponent<Text>().text = "Anzahl der weiteren Versuche: " + triesLeft;                  // Anzahl der übrigen Versuche wird als Text angezeigt.
        if (LocationManager.currentLocation == LoadXmlFileLocations.arraySize)
        {
            realWrongCounter = LocationManager.overallWrongCounter - LoadXmlFileLocations.arraySize;
            questionText.GetComponent<Text>().text = allLocationsAnswered;
            counterText.GetComponent<Text>().text = "Sie haben " + LocationManager.rightCounter + "/" + LoadXmlFileLocations.arraySize + " erfolgreich gefunden. Sie haben insgesamt " + realWrongCounter +  " falsche Eingaben gemacht";

        }

	}

}
