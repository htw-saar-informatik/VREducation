using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcelandCollider : MonoBehaviour {

    private string currentLocationStr;

    // Jedes Frame wird die momentan gesuchte Umgebung aus dem LocationArray geladen
    private void Update()
    {
        currentLocationStr = LoadXmlFileLocations.locationArray[LocationManager.currentLocation];
    }

    // Sobald der Globus mit den Oculus-Händen kollidiert, während der "A" Knopf der Touch Controller gedrückt ist, wird das Ergebnis anerkannt 
    void OnTriggerEnter()
    {

        // Falls der "A" Knopf der Touch Controller betätigt ist
        if (OVRInput.Get(OVRInput.Button.One))
        {

            // Prüfung des gezeigten Ortes auf richtige Antwort
            if (currentLocationStr.Equals("Zeigen Sie das Land Island"))
            {
             
                LocationManager.currentLocation++;            // Nächster gesuchter Ort      
                LocationManager.wrongCounter = 0;             // Zähler der Fehler wird auf 0 gesetzt.
                LocationManager.rightCounter++;
            }

        }

    }
   
}
