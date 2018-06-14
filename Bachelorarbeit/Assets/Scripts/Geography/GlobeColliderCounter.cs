using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeColliderCounter : MonoBehaviour {

    // Jedes Frame wird der Fehlerzähler überprüft, sobald mehr als 4 Fehler erkannt wurden, wird die Antwort als falsch gewertet und die nächste gesuchte Umgebung wird geladen
    void Update()
    {
         
        // Wenn der Fehlerzähler 5 erreicht
          if (LocationManager.wrongCounter >= 5)
            {
               
            LocationManager.currentLocation++;                // Nächster gesuchter Ort
            LocationManager.wrongCounter = 0;                // Zähler der Fehler wird auf 0 gesetzt
        }

        }

    // Sobald der Globus mit den Oculus-Händen kollidiert, während der "A" Knopf der Touch Controller gedrückt ist wird das Ergebnis anerkannt 
    // Zeitspanne wird überprüft, damit Mehrfachklicks nicht gewertet werden
    void OnTriggerEnter()
    {
        // Falls der "A" Knopf der Touch Controller betätigt ist und die Zeitschwelle überschritten ist
        if (OVRInput.Get(OVRInput.Button.One) && LocationManager.tapAllowed == true)
        {

            LocationManager.wrongCounter++;    // Inkrementiert Fehlerzähler, der bei richtiger Antwort resetet wird
            LocationManager.overallWrongCounter++;
            StartCoroutine(WaitingThreshhold());  // Startet die Zeitspanne
        }
    }

    // Zeitspanne von 1,5 Sekunden, um Mehrfachinteraktionen mit dem Globus zu vermeiden
    IEnumerator WaitingThreshhold()
    {
        LocationManager.tapAllowed = false;       
        yield return new WaitForSeconds(1.5F);         // Wartet 1,5 Sekunden
        LocationManager.tapAllowed = true;

    }
}
