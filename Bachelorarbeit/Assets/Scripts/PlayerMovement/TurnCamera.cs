using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Skript, das durch Knopfdruck auf den Touch Controllern eine Drehung um 90 Grad nach links bzw. rechts bewirkt.
public class TurnCamera : MonoBehaviour {
    //90 Grad Drehung nach links
    private int turnCameraLeft = -90;

    //90 Grad Drehung nach rechs        
    private int turnCameraRight = 90;

    public GameObject player;
    private Boolean turningCamera = true;

    // Jedes Frame wird auf einen Knopfdruck geprüft und die Spielerkamera dementsprechend gedreht
     void Update()
    {
        // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
        if (turningCamera == true)
        {
            // Falls der B-Knopf des rechten Oculus Touch Controllers gedrückt wird, wird die Kamera um 90 grad nach links gedreht
            if (OVRInput.Get(OVRInput.Button.Two))
            {
                turningCamera = false;
                player.transform.Rotate(0, turnCameraLeft, 0);
                StartCoroutine(turnCameraEnable());
            }
        }
        // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
        if (turningCamera == true)
        {
            // Falls der Y-Knopf des linken Oculus Touch Controllers gedrückt wird, wird die Kamera um 90 grad nach rechts gedreht
            if (OVRInput.Get(OVRInput.Button.Four))
            {
                turningCamera = false;
                player.transform.Rotate(0, turnCameraRight, 0);
                StartCoroutine(turnCameraEnable());
            }
        }
    }


    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    IEnumerator turnCameraEnable()
    {
        yield return new WaitForSeconds(1);
        turningCamera = true;
    }
}
