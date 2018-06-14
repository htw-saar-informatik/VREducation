using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightTrigger : MonoBehaviour {

    public GameObject flashLight;


    // Wird bei Berührung der An- und Ausschalter der Taschenlampen aufgerufen. Damit können sie dementsprechend an- oder ausgeschaltet werden
    private void OnTriggerEnter(Collider other)
    {

        // Falls die Taschenlampe bereits an ist, wird sie ausgeschaltet
        if (flashLight.activeSelf)
        {
            flashLight.SetActive(false);

            // Warteschwelle, um Mehrfachklicks zu vermeiden
            StartCoroutine(WaitingThreshhold());
        }

        // Falls die Taschenlampe aus ist, wird sie angeschaltet
        else if (flashLight.activeSelf == false)
        {
            flashLight.SetActive(true);
      
            // Warteschwelle, um Mehrfachklicks zu vermeiden
            StartCoroutine(WaitingThreshhold());
        }


    }

    // Warteschwelle, um Mehrfachklicks zu vermeiden
    IEnumerator WaitingThreshhold()
    {
        yield return new WaitForSeconds(1.5F);   
    }
}
