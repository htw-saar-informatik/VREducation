using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Skript zum Pausieren der Simulationen vor einer neuen Audiodatei
public class PauseSimulation : MonoBehaviour {

    public static Boolean myPauseState = false;
    private Boolean buttonReady = true;
    public TextMesh buttonText;
    private string pauseButtonText = "Pausiere";
    private string resumeButtonText = "Setze Fort";



    // Bei Kollision zwischen den Touch Controllern und des PauseButtons wird der Pausezustand gewechselt.
    private void OnTriggerEnter(Collider other)
    {
        // Überprüft Warteschwelle, um Mehrfachklick zu vermeiden
        if (buttonReady == true)
        {
            buttonReady = false;

            // Wenn nicht pausiert ist, wird pausiert und der Buttontext dementsprechend geändert
            if (myPauseState == false)
            {
                myPauseState = true;
                buttonText.text = resumeButtonText;
            }

            // Wenn pausiert ist, wird fortgesetzt und der Buttontext dementsprechend geändert
            else if (myPauseState == true)
            {
                myPauseState = false;
                buttonText.text = pauseButtonText;
            }
           
            // Warteschwelle, um Mehrfachklicks zu vermeiden
            StartCoroutine(WaitingThreshhold());
        }
    }

    // Warteschwelle, um Mehrfachklicks zu vermeiden
    IEnumerator WaitingThreshhold()
    {
        yield return new WaitForSeconds(1);
        buttonReady = true;
        
    }
}
 