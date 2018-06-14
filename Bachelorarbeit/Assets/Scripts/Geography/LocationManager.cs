using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour {

    // Globale Variablen, die den Index des LocationArrays angibt
    public static int currentLocation = 0;

    // Globale Variable, die angibt, wieviele Versuche der Nutzer hat
    public static int wrongCounter = 0;

    // Globale Variable als Warteschwelle für Eingabe
    public static Boolean tapAllowed = true;

    // Globale Variable die angibt, wie oft der Nutzer ein Land finden konnte
    public static int rightCounter = 0;

    // Globale Variable die angibt, wie oft der Nutzer eine falsche Eingabe getätigt hat
    public static int overallWrongCounter = 0; 
  
}
