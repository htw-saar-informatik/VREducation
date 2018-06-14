using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Simulation_Shadow : MonoBehaviour
{
    // Lichtobjekte der Taschenlampen
    public GameObject lightSourceRight;
    public GameObject lightSourceMiddle;
    public GameObject lightSourceLeft;

    // Knöpfe auf den Taschenlampen, um an- und auszuschalten
    public GameObject lightSourceRightTrigger;
    public GameObject lightSourceMiddleTrigger;
    public GameObject lightSourceLeftTrigger;

    // Greifbare Taschenlampenobjekte, welche die Lichtobjekte und die Knöpfe als Kindsobjekte beinhalten
    public GameObject flashLightLeft;
    public GameObject flashLightRight;
    public GameObject flashLightMiddle;

    // Markierungen, die im 3D Raum angezeigt werden
    public GameObject randstrahlenMarker;
    public GameObject gegendstandsgrößeMarker;
    public GameObject gegenstandsweiteMarker;
    public GameObject bildgrößeMarker;
    public GameObject bildweiteMarker;
    public GameObject halbschattenMarker1;
    public GameObject halbschattenMarker2;
    public GameObject kernschattenMarker;

    // Textmeshs der Buttons, deren Text verändert wird
    public TextMesh buttonText;
    public TextMesh markerButtonText;
    public TextMesh lightSourceButtonText;

    // Strings für die wechselnden Texte der Buttons
    private string startSimulationText = "Starte Erklärung";
    private string endSimulationText = "Beende Simulation";
    private string activateMarkerText = "Aktiviere Marker";
    private string disableMarkerText = "Deaktiviere Marker";
    private string activateLightSourceText = "Aktiviere Lichter";
    private string disableLightSourceText = "Deaktiviere Lichter";


    // Nicht greifbare Taschenlampobjekte
    public GameObject flashLightLeftNotGrabbable;
    public GameObject flashLightRightNotGrabbable;
    public GameObject flashLightMiddleNotGrabbable;
    public GameObject flashLightLeftNotGrabbableLight;
    public GameObject flashLightRightNotGrabbableLight;
    public GameObject flashLightMiddleNotGrabbableLight;

    // Audiodateien, die die Erklärungen beinhalten
    public AudioSource physics_shadow_1;
    public AudioSource physics_shadow_2;
    public AudioSource physics_shadow_3;
    public AudioSource physics_shadow_4;

    // Booleans, die abgefragt werden, um Mehrfachklicks zu vermeiden
    public static Boolean simulationReady = true;
    private Boolean jumpTo1ButtonReady = true;
    private Boolean jumpTo2ButtonReady = true;
    public static Boolean simulationButtonReady = true;
    private Boolean toggleMarkerButtonReady = true;
    private Boolean toggleFlashLightButtonReady = true;
    private Boolean toggleLightSourceButtonReady = true;

    // Positionsdaten der Taschenlampen
    private float lightLeftPositionX = 12.76F;
    private float lightLeftPositionY = 1.384F;
    private float lightLeftPositionZ = 14.85808F;
    private float lightMiddlePositionX = 12.937F;
    private float lightMiddlePositionY = 1.3685F;
    private float lightMiddlePositionZ = 9.911133F;
    private float lightRightPositionX = 14.235F;
    private float lightRightPositionY = 1.381F;
    private float lightRightPositionZ = 3.863035F;
    private float lightLeftRotationX = -1.039F;
    private float lightLeftRotationY = 104.497F;
    private float lightLeftRotationZ = -89.71001F;
    private float lightMiddleRotationX = -1.039F;
    private float lightMiddleRotationY = 82.202F;
    private float lightMiddleRotationZ = -89.71001F;
    private float lightRightRotationX = -1.039F;
    private float lightRightRotationY = 93.284F;
    private float lightRightRotationZ = -89.71001F;

    public static Coroutine simulationCoroutine;
    
    // Booleam um abzufragen, ob die Markierungen bzw. die Lichter bereits aktiviert sind
    private Boolean markerActive = false;
    private Boolean lightSourceActive = true;
    
  
    // Überprüft jedes Frame, ob die Lichter bzw. die Markierungen aktiviert sind und ändert dementsprechend den Text der dazugehörigen Buttons
    private void Update()
    {

        // Überprüft, ob die Markierungen aktiviert sind  und ändert dementsprechend Buttontext
        CheckMarker();
        if (markerActive == true)
        {
            markerButtonText.text = disableMarkerText;
        }
        else markerButtonText.text = activateMarkerText;

        // Überprüft, ob die Lichter aktiviert sind und ändert dementsprechend Buttontext
        CheckLightSources();
        if (lightSourceActive == true)
        {
            lightSourceButtonText.text = disableLightSourceText;
        }
        else lightSourceButtonText.text = activateLightSourceText;
    }

    // Simulation in voller Länge
    IEnumerator StartSimulation()
    {
        // Andere Simulationen können nicht gestartet werden, bis die Simulation zuende ist oder abgebrochen wurde
        simulationReady = false;

        // Deaktiviert Markierungen
        ActivateMarker(false);

        // Text wird auf "Stoppe Simulation" gesetzt
        buttonText.text = endSimulationText;

        // Lichtquellen werden abgeschaltet
        SetLightSources(false);

        // Taschenlampen werden auf die richtige Position gebracht
        SetLightSourcePosition();

        // Greifbare Taschenlampen sind während der Simulation nicht greifbar
        SetGrabbable(false);

        // Erklärung wird abgespielt
        physics_shadow_1.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(physics_shadow_1.clip.length);

        // Rechte Taschenlampe wird eingeschaltet und erzeugt somit Licht
        flashLightRightNotGrabbableLight.SetActive(true);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {
            yield return new WaitForFixedUpdate();
        }

        // Erklärung wird abgespielt
        physics_shadow_2.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(13);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        randstrahlenMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(4);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        gegendstandsgrößeMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(10);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        gegenstandsweiteMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(3);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        bildgrößeMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(4);


        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        bildweiteMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(2);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {
      
            yield return new WaitForFixedUpdate();
        }

        // Vorherige Taschenlampe wird ausgeschaltet
        flashLightRightNotGrabbableLight.SetActive(false);

        // Erklärung wird abgespielt
        physics_shadow_3.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(8);

        // Für Simulation benötigten Taschenlampen werden angeschaltet und somit Licht erzeugt
        flashLightMiddleNotGrabbableLight.SetActive(true);
        flashLightLeftNotGrabbableLight.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(6);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        halbschattenMarker1.SetActive(true);
        halbschattenMarker2.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(1);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        kernschattenMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(7);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {
            
            yield return new WaitForFixedUpdate();
        }

        // Erklärung wird abgespielt
        physics_shadow_4.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(physics_shadow_4.clip.length);

        // Taschenlampen können an- und ausgeschaltet werden
        SetTrigger(true);
        
        // Taschenlampen sind wieder greifbar
        SetGrabbable(true);

        // Simulation kann wieder gestartet werden
        simulationReady = true;



    }
    // Wird bei Kollision zwischen den Touch Controllern und einem Button aufgerufen. Der Name des berührten Button wird abgefragt und dementsprechend reagiert
    private void OnTriggerEnter(Collider other)
    {

        // Falls der Button zum Starten der Erklärung berührt wurde
        if (gameObject.name == "StartExplanation")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (simulationButtonReady == true)
            {
                simulationButtonReady = false;
                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft, wird die Simulation gestartet
                if (simulationReady == true)
                {
                    simulationCoroutine = StartCoroutine(StartSimulation());
                }
                // Prüft, ob eine Simulation läuft. Falls eine Simulation läuft, wird die Simulation beendet
                else if (simulationReady == false)
                {
                    StopSimulation();
                }

                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshhold());
            }
        }
        else if (gameObject.name == "JumpTo1Button")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (jumpTo1ButtonReady == true)
            {
                jumpTo1ButtonReady = false;

                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft, wird die Simulation beginnend von der 1. Wand gestartet
                if (simulationReady == true)
                {
                    simulationCoroutine = StartCoroutine(StartSimulationFromWall1());
                }

                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdJumpTo1Button());
            }

        }
        else if (gameObject.name == "JumpTo2Button")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (jumpTo2ButtonReady == true)
            {
                jumpTo2ButtonReady = false;
                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft, wird die Simulation beginnend von der 2. Wand gestartet
                if (simulationReady == true)
                {
                    simulationCoroutine = StartCoroutine(StartSimulationFromWall2());
                }

                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdJumpTo2Button());
            }

        }
        else if (gameObject.name == "ToggleMarkerButton")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (toggleMarkerButtonReady == true)
            {
                toggleMarkerButtonReady = false;
                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft können Markierung aktiviert und deaktiviert werden
                if (simulationReady == true)
                {
                    if (markerActive == true)
                    {
                        ActivateMarker(false);
                    }
                    else ActivateMarker(true);
                }

                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdToggleMarkerButton());
            }
        }
        else if (gameObject.name == "ToggleFlashLightButton")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (toggleFlashLightButtonReady == true)
            {
                // Prüft, ob eine Simulation läuft.  Falls keine Simulation läuft können die Taschenlampen greifbar gemacht werden
                if (simulationReady == true)
                {
                    SetGrabbable(true);
                }

                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdToggleFlashLightButton());
            }
        }
        else if (gameObject.name == "ToggleLightSourceButton")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (toggleLightSourceButtonReady == true)
            {
                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft können alle Lichter aktiviert und deaktiviert werden
                if (simulationReady == true)
                {
                    if (lightSourceActive == true)
                    {
                        ActivateLightSources(false);
                    }
                    else ActivateLightSources(true);  
                }
                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdToggleLightSourceButton());
            }
        }

    }

    // Lichter können aktiviert und deaktiviert werden
    private void SetLightSources(Boolean value)
    {
        lightSourceLeft.SetActive(value);
        lightSourceMiddle.SetActive(value);
        lightSourceRight.SetActive(value);
        lightSourceRightTrigger.SetActive(value);
        lightSourceMiddleTrigger.SetActive(value);
        lightSourceLeftTrigger.SetActive(value);
    }

    // Funktion, um die Knöpfe auf den Taschenlampen zum ein- und ausschalten der Taschenlampen zu aktivieren oder deaktivieren
    private void SetTrigger(Boolean value)
    {
        lightSourceRightTrigger.SetActive(value);
        lightSourceMiddleTrigger.SetActive(value);
        lightSourceLeftTrigger.SetActive(value);

    }

    // Setzt Taschenlampen auf die Position für die Simulation zurück
    private void SetLightSourcePosition()
    {
        Vector3 lightSourceLeftPosition = new Vector3(lightLeftPositionX, lightLeftPositionY, lightLeftPositionZ);
        Vector3 lightSourceMiddlePosition = new Vector3(lightMiddlePositionX, lightMiddlePositionY, lightMiddlePositionZ);
        Vector3 lightSourceRightPosition = new Vector3(lightRightPositionX, lightRightPositionY, lightRightPositionZ);
        Vector3 lightSourceLeftRotation = new Vector3(lightLeftRotationX, lightLeftRotationY, lightLeftRotationZ);
        Vector3 lightSourceMiddleRotation = new Vector3(lightMiddleRotationX, lightMiddleRotationY, lightMiddleRotationZ);
        Vector3 lightSourceRightRotation = new Vector3(lightRightRotationX, lightRightRotationY, lightRightRotationZ);
        flashLightLeft.transform.position = lightSourceLeftPosition;
        flashLightMiddle.transform.position = lightSourceMiddlePosition;
        flashLightRight.transform.position = lightSourceRightPosition;
        flashLightLeft.transform.eulerAngles = lightSourceLeftRotation;
        flashLightMiddle.transform.eulerAngles = lightSourceMiddleRotation;
        flashLightRight.transform.eulerAngles = lightSourceRightRotation;
    }

    // Funktion, um Taschenlampen greifbar oder nicht greifbar zu machen. Es wird zwischen Objekten gewechselt. Falls value = true, werden die greifbaren Taschenlampen aktiviert und die nicht
    // greifbaren Taschenlampen deaktivert. Falls value = false, werden die nicht greifbaren Taschenlampen aktiviert und die greifbaren Taschenlampen deaktiviert
    private void SetGrabbable(Boolean value)
    {
        if (value == true)
        {
            flashLightLeftNotGrabbable.SetActive(false);
            flashLightMiddleNotGrabbable.SetActive(false);
            flashLightRightNotGrabbable.SetActive(false);
            flashLightLeft.SetActive(true);
            flashLightMiddle.SetActive(true);
            flashLightRight.SetActive(true);
            SetTrigger(true);
        }
        if (value == false)
        {
            flashLightLeftNotGrabbableLight.SetActive(false);
            flashLightMiddleNotGrabbableLight.SetActive(false);
            flashLightRightNotGrabbableLight.SetActive(false);
            flashLightLeftNotGrabbable.SetActive(true);
            flashLightMiddleNotGrabbable.SetActive(true);
            flashLightRightNotGrabbable.SetActive(true);
            flashLightLeft.SetActive(false);
            flashLightMiddle.SetActive(false);
            flashLightRight.SetActive(false);
        }
    }

    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshhold()
    {
        yield return new WaitForSeconds(1);
        simulationButtonReady = true;
    }

    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdJumpTo1Button()
    {
        yield return new WaitForSeconds(1);
        jumpTo1ButtonReady = true;
    }

    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdJumpTo2Button()
    {
        yield return new WaitForSeconds(1);
        jumpTo2ButtonReady = true;
    }

    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdToggleMarkerButton()
    {
        yield return new WaitForSeconds(1);
        toggleMarkerButtonReady = true;
    }

    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdToggleFlashLightButton()
    {
        yield return new WaitForSeconds(1);
        toggleFlashLightButtonReady = true;
    }

    // Warteschwelle für zugehörigen Button. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdToggleLightSourceButton()
    {
        yield return new WaitForSeconds(1);
        toggleLightSourceButtonReady = true;
    }

    // Funktion zum Beenden der Simulation. Die Coroutine und die Audiodateien werden beendet, die Taschenlampen werden greifbar gemacht, die Markierungen werden deaktiviert und die Simulation
    // kann erneut gestartet werden
    private void StopSimulation()
    {
        StopCoroutine(simulationCoroutine);
        ActivateMarker(false);
        SetGrabbable(true);
        buttonText.text = startSimulationText;
        physics_shadow_1.Stop();
        physics_shadow_2.Stop();
        physics_shadow_3.Stop();
        physics_shadow_4.Stop();
        simulationReady = true;
    }


    // Simulation, beginnend von der 1. Wand
    private IEnumerator StartSimulationFromWall1()
    {
        // Andere Simulationen können nicht gestartet werden, bis die Simulation zuende ist oder abgebrochen wurde
        simulationReady = false;

        // Deaktiviert Markierungen
        ActivateMarker(false);

        // Text wird auf "Stoppe Simulation" gesetzt
        buttonText.text = endSimulationText;

        // Lichtquellen werden abgeschaltet
        SetLightSources(false);

        // Taschenlampen werden auf die richtige Position gebracht
        SetLightSourcePosition();

        // Greifbare Taschenlampen sind während der Simulation nicht greifbar
        SetGrabbable(false);

        // Rechte Taschenlampe wird eingeschaltet und erzeugt somit Licht
        flashLightRightNotGrabbableLight.SetActive(true);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {
            yield return new WaitForFixedUpdate();
        }

        // Erklärung wird abgespielt
        physics_shadow_2.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(13);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        randstrahlenMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(4);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        gegendstandsgrößeMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(10);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        gegenstandsweiteMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(3);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        bildgrößeMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(4);


        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        bildweiteMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(2);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {

            yield return new WaitForFixedUpdate();
        }

        // Vorherige Taschenlampe wird ausgeschaltet
        flashLightRightNotGrabbableLight.SetActive(false);

        // Erklärung wird abgespielt
        physics_shadow_3.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(8);

        // Für Simulation benötigten Taschenlampen werden angeschaltet und somit Licht erzeugt
        flashLightMiddleNotGrabbableLight.SetActive(true);
        flashLightLeftNotGrabbableLight.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(6);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        halbschattenMarker1.SetActive(true);
        halbschattenMarker2.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(1);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        kernschattenMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(7);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {

            yield return new WaitForFixedUpdate();
        }

        // Erklärung wird abgespielt
        physics_shadow_4.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(physics_shadow_4.clip.length);

        // Taschenlampen können an- und ausgeschaltet werden
        SetTrigger(true);

        // Taschenlampen sind wieder greifbar
        SetGrabbable(true);

        // Simulation kann wieder gestartet werden
        simulationReady = true;



    }

    // Funktion zum ein- und ausschalten aller Lichtquellen
    private void ActivateLightSources(Boolean value)
    {
        lightSourceLeft.SetActive(value);
        lightSourceRight.SetActive(value);
        lightSourceMiddle.SetActive(value);

    }

    // Funktion zum aktivieren und deaktivieren aller Markierungen
    private void ActivateMarker(Boolean value)
    {
        randstrahlenMarker.SetActive(value);
        gegendstandsgrößeMarker.SetActive(value);
        gegenstandsweiteMarker.SetActive(value);
        bildgrößeMarker.SetActive(value);
        bildweiteMarker.SetActive(value);
        halbschattenMarker1.SetActive(value);
        halbschattenMarker2.SetActive(value);
        kernschattenMarker.SetActive(value);
    }

    // Simulation beginnend von der 2. Wand
    private IEnumerator StartSimulationFromWall2()
    {
        // Andere Simulationen können nicht gestartet werden, bis die Simulation zuende ist oder abgebrochen wurde
        simulationReady = false;

        // Deaktiviert Markierungen
        ActivateMarker(false);

        // Text wird auf "Stoppe Simulation" gesetzt
        buttonText.text = endSimulationText;

        // Lichtquellen werden abgeschaltet
        SetLightSources(false);

        // Taschenlampen werden auf die richtige Position gebracht
        SetLightSourcePosition();

        // Greifbare Taschenlampen sind während der Simulation nicht greifbar
        SetGrabbable(false);

        // Vorherige Taschenlampe wird ausgeschaltet
        flashLightRightNotGrabbableLight.SetActive(false);

        // Erklärung wird abgespielt
        physics_shadow_3.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(8);

        // Für Simulation benötigten Taschenlampen werden angeschaltet und somit Licht erzeugt
        flashLightMiddleNotGrabbableLight.SetActive(true);
        flashLightLeftNotGrabbableLight.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(6);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        halbschattenMarker1.SetActive(true);
        halbschattenMarker2.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(1);

        // Markierung wird synchron zur Erklärung für eine bessere Visualisierung eingeschaltet
        kernschattenMarker.SetActive(true);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(7);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulation.myPauseState)
        {

            yield return new WaitForFixedUpdate();
        }

        // Erklärung wird abgespielt
        physics_shadow_4.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(physics_shadow_4.clip.length);

        // Taschenlampen können an- und ausgeschaltet werden
        SetTrigger(true);

        // Taschenlampen sind wieder greifbar
        SetGrabbable(true);

        // Simulation kann wieder gestartet werden
        simulationReady = true;


    }

    // Funktion zum Überprüfen, ob Markierungen aktiviert sind, da z.B. Markierungen beim vorzeitigen Abbruch einer Simulation aktiviert bleiben
    private void CheckMarker()
    {
        if (randstrahlenMarker.activeSelf | gegendstandsgrößeMarker.activeSelf | gegenstandsweiteMarker.activeSelf | bildgrößeMarker.activeSelf | bildweiteMarker.activeSelf | halbschattenMarker1.activeSelf | halbschattenMarker2.activeSelf | kernschattenMarker.activeSelf)
        {
            markerActive = true;
        }
        else markerActive = false;
    }

    // Funktion zum Überprüfen, ob Lichter aktiviert sind, da z.B. Lichter beim vorzeitigen Abbruch einer Simulation aktiviert bleiben
    private void CheckLightSources()
    {
        if (lightSourceLeft.activeSelf | lightSourceMiddle.activeSelf | lightSourceRight.activeSelf)
        {
            lightSourceActive = true;
        }
        else lightSourceActive = false;
    }
}