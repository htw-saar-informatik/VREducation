using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLocation : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        LocationManager.currentLocation = 0;
        LocationManager.wrongCounter = 0;
        LocationManager.rightCounter = 0;
        LocationManager.overallWrongCounter = 0;
    }
}
