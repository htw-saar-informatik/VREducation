using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSimulation : MonoBehaviour {
    
    
    // Halterungsobjekte
    public GameObject sphereHolder1;
    public GameObject sphereHolder2;
    public GameObject sphereHolder3;
    public GameObject sphereHolder4;

    //Fallende Objekte
    public GameObject sphereLeft;
    public GameObject sphereMiddleLeft;
    public GameObject sphereMiddleRight;
    public GameObject woodenPlate;

    //Skyboxen, also Material, das als Himmel angezeigt wird
    public Material skyboxSpace;
    public Material skyboxTown;

    // Audiodateien, die die Erklärungen beinhalten
    public AudioSource freefall1;
    public AudioSource freefall2;
    public AudioSource freefall3;

    // Variable zum Prüfen, ob eine Simulation läuft
    public static Boolean simulationReady = true;

    // Variable zum vermeiden von Mehrfachklicks
    private Boolean simulationButtonReady = true;
    private Boolean jumpTo1ButtonReady = true;
    private Boolean jumpTo2ButtonReady = true;

    // Coroutine, die der Simulation entspricht
    public static Coroutine simulationCoroutine;

    // Texte als Strings, die abwechselend als Buttontext angezeigt werden abhängig davon, ob eine Simulation läuft oder nicht
    private string startSimulationText = "Starte Erklärung";
    private string endSimulationText = "Beende Simulation";
    public TextMesh buttonText;
    

    // Positionsdaten der fallenden Objekte, um sie nach dem Fall wieder auf ihre ursprüngliche Position zu platzieren
    private float sphereLeftPositionX = 20.23F;
    private float sphereLeftPositionY = 18.06F;
    private float sphereLeftPositionZ = -15.35F;
    private float sphereMiddlePositionX = 10.55F;
    private float sphereMiddlePositionY = 18.06F;
    private float sphereMiddlePositionZ = -15.35F;
    private float sphereRightPositionX = 1.399F;
    private float sphereRightPositionY = 18.023F;
    private float sphereRightPositionZ = -15.255F;
    private float platePositionX = -8.302F;
    private float platePositionY = 17.588F;
    private float platePositionZ = -14.86F;
    private float platePositionRotationX = 0F;
    private float platePositionRotationY =0F;
    private float platePositionRotationZ = 0F;


    // Wird bei Kollision zwischen den Touch Controllern und einem Button aufgerufen. Der Name des berührten Button wird abgefragt und dementsprechend reagiert
    private void OnTriggerEnter(Collider other)
    {

        // Falls der Button zum Starten der Erklärung berührt wurde
        if (gameObject.name == "StartExplanation")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (simulationButtonReady == true)
            {
                // Button wird auf nicht bereit gesetzt
                simulationButtonReady = false;

                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft, wird die Simulation gestartet
                if (simulationReady == true)
                {
                    simulationCoroutine = StartCoroutine(StartFallSimulation());
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
        // Falls der Button zum Starten der Erklärung anfangend bei dem 1. Fall berührt wurde
        else if (gameObject.name == "JumpTo1Button")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (jumpTo1ButtonReady == true)
            {
                // Button wird auf nicht bereit gesetzt
                jumpTo1ButtonReady = false;

                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft, wird die Simulation gestartet
                if (simulationReady == true)
                {
                    simulationCoroutine = StartCoroutine(StartSimulationFromFall());
                }
                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdJumpTo1Button());
            }

        }
        // Falls der Button zum Starten der Erklärung anfangend bei dem 2. Fall, im Vakuum, berührt wurde
        else if (gameObject.name == "JumpTo2Button")
        {
            // Prüfung, ob der Button bereit ist. Vermeidet Mehrfachklicks
            if (jumpTo2ButtonReady == true)
            {
                // Button wird auf nicht bereit gesetzt
                jumpTo2ButtonReady = false;

                // Prüft, ob eine Simulation läuft. Falls keine Simulation läuft, wird die Simulation gestartet
                if (simulationReady == true)
                {
                    simulationCoroutine = StartCoroutine(StartSimulationFromVacuum());
                }
                // Nach Klick des Buttons wird 1 Sekunde gewartet, bis er wieder gedrückt werden kann
                StartCoroutine(WaitingThreshholdJumpTo2Button());
            }

        }
   
    }

    // Funktion zum Beenden der Simulation
    private void StopSimulation()
    {
        // RigidBody der Holzplatte wird gesucht und der Drag des Rigidbody bei jedem Stopp der Simulation auf 1 gesetzt, sodass sie langsamer fällt als die anderen Objekte
        Rigidbody rb = woodenPlate.GetComponent<Rigidbody>();
        rb.drag = 1;

        // Nach Beenden der Simulation wird der Text des Buttons wieder auf "Starte Erklärung" gesetzt
        buttonText.text = startSimulationText;
        // Audiodateien werden alle beendet
        freefall1.Stop();
        freefall2.Stop();
        freefall3.Stop();

        // Als Skybox wird die Startskybox ausgewählt
        RenderSettings.skybox = skyboxTown;

        // Objekte werden wieder auf die Häuser platziert
        positionObjects();

        // Die Coroutinen werden gestoppt
        StopCoroutine(simulationCoroutine);

        // Simulation kann wieder gestartet werden
        simulationReady = true;
    }

    // Die Simulation in voller Länge. Objekte werden synchron mit Erklärungen fallengelassen. Dannach wird in ein Vakuum gewechselt und die Objekte erneut fallengelassen
    IEnumerator StartFallSimulation()
    {
        // Andere Simulationen können nicht gestartet werden, bis die Simulation zuende ist oder abgebrochen wurde
        simulationReady = false;

        // Text wird auf "Stoppe Simulation" gesetzt
        buttonText.text = endSimulationText;

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Audioerklärung wird gestartet
        freefall1.Play();

        // Es wird gewartet, bis die Audiodatei beendet ist
        yield return new WaitForSeconds(freefall1.clip.length);

        // Objektehalterungen werden deaktiviert. Fallobjekte fallen dadurch
        setObjectHolder(false);

        // Es wird gewartet, bis die Objekte auf dem Boden angekommen sind
        yield return new WaitForSeconds(1.5F);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulationFreeFall.myPauseState)
        {
            yield return new WaitForFixedUpdate();
        }
        // Audioerklärung wird gestartet
        freefall2.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(freefall2.clip.length - 7);

        // Skybox wird gewechselt, um ein Vakuum zu simulieren
        RenderSettings.skybox = skyboxSpace;

        //Objekthalterungen werden aktiviert. Objekte können nun darauf wieder platziert werden, ohne zu fallen
        setObjectHolder(true);

        // RigidBody der Holzplatte wird gesucht und der Drag des Rigidbody auf 0 gesetzt, sodass sie gleich schnell fällt wie die anderen Objekte, da man nun im Vakuum ist
        Rigidbody rb = woodenPlate.GetComponent<Rigidbody>();
        rb.drag = 0;

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Warten bis Audiodatei beendet ist
        yield return new WaitForSeconds(7);

        // Objektehalterungen werden deaktiviert. Fallobjekte fallen dadurch
        setObjectHolder(false);

        // Es wird gewartet, bis die Objekte auf dem Boden angekommen sind
        yield return new WaitForSeconds(2);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulationFreeFall.myPauseState)
        {
            Debug.Log("bin in loop");
            yield return new WaitForFixedUpdate();
        }

        // Audioerklärung wird gestartet
        freefall3.Play();

        // Es wird gewartet, bis die Audiodatei beendet ist
        yield return new WaitForSeconds(freefall3.clip.length);

        // Drag des Rigidbody der Holzplatte wird auf 1 gesetzt, sodass sie langsamer fällt als die anderen Objekte
        rb.drag = 1;

        // Als Skybox wird die Startskybox ausgewählt
        RenderSettings.skybox = skyboxTown;

        //Objekthalterungen werden aktiviert
        setObjectHolder(true);

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Simulation kann wieder gestartet werden
        simulationReady = true;
    }

    // Die Simulation beginnend vom ersten Fall. Objekte werden synchron mit Erklärungen fallengelassen. Dannach wird in ein Vakuum gewechselt und die Objekte erneut fallengelassen
    private IEnumerator StartSimulationFromFall()
    {
        // Andere Simulationen können nicht gestartet werden, bis die Simulation zuende ist oder abgebrochen wurde
        simulationReady = false;

        // Text wird auf "Stoppe Simulation" gesetzt
        buttonText.text = endSimulationText;

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(4);

        // Objektehalterungen werden deaktiviert. Fallobjekte fallen dadurch
        setObjectHolder(false);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(1.5F);

        // Audioerklärung wird gestartet
        freefall2.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(freefall2.clip.length - 7);

        // Skybox wird gewechselt, um ein Vakuum zu simulieren
        RenderSettings.skybox = skyboxSpace;

        //Objekthalterungen werden aktiviert. Objekte können nun darauf wieder platziert werden, ohne zu fallen
        setObjectHolder(true);

        // RigidBody der Holzplatte wird gesucht und der Drag des Rigidbody bei jedem Stopp der Simulation auf 0 gesetzt, sodass sie gleich schnell fällt wie die anderen Objekte, da man nun im Vakuum ist
        Rigidbody rb = woodenPlate.GetComponent<Rigidbody>();
        rb.drag = 0;

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(7);

        // Objektehalterungen werden deaktiviert. Fallobjekte fallen dadurch
        setObjectHolder(false);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(2);

        // Falls der Pausebutton betätigt wurde wird gewartet bis auf Setze Fort gedrückt wurde
        while (PauseSimulationFreeFall.myPauseState)
        {
            Debug.Log("bin in loop");
            yield return new WaitForFixedUpdate();
        }
        // Audioerklärung wird gestartet
        freefall3.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(freefall3.clip.length);

        // Drag des Rigidbody der Holzplatte wird auf 1 gesetzt, sodass sie langsamer fällt als die anderen Objekte
        rb.drag = 1;

        // Als Skybox wird die Startskybox ausgewählt
        RenderSettings.skybox = skyboxTown;

        //Objekthalterungen werden aktiviert. Objekte können nun darauf wieder platziert werden, ohne zu fallen
        setObjectHolder(true);

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Simulation kann wieder gestartet werden
        simulationReady = true;


    }

    private IEnumerator StartSimulationFromVacuum()
    {
        // Andere Simulationen können nicht gestartet werden, bis die Simulation zuende ist oder abgebrochen wurde
        simulationReady = false;

        // Text wird auf "Stoppe Simulation" gesetzt
        buttonText.text = endSimulationText;

        // Skybox wird gewechselt, um ein Vakuum zu simulieren
        RenderSettings.skybox = skyboxSpace;

        //Objekthalterungen werden aktiviert. Objekte können nun darauf wieder platziert werden, ohne zu fallen
        setObjectHolder(true);

        // Drag des Rigidbody der Holzplatte wird auf 1 gesetzt, sodass sie langsamer fällt als die anderen Objekte
        Rigidbody rb = woodenPlate.GetComponent<Rigidbody>();
        rb.drag = 0;

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(4);

        // Objektehalterungen werden deaktiviert. Fallobjekte fallen dadurch
        setObjectHolder(false);

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(2);

        // Audioerklärung wird gestartet
        freefall3.Play();

        // Warteschwelle, um Erklärung mit Simulation zu synchronisieren
        yield return new WaitForSeconds(freefall3.clip.length);

        // Drag des Rigidbody der Holzplatte wird auf 1 gesetzt, sodass sie langsamer fällt als die anderen Objekte
        rb.drag = 1;

        // Als Skybox wird die Startskybox ausgewählt
        RenderSettings.skybox = skyboxTown;

        //Objekthalterungen werden aktiviert. Objekte können nun darauf wieder platziert werden, ohne zu fallen
        setObjectHolder(true);

        // Objekte werden auf Häuser platziert
        positionObjects();

        // Simulation kann wieder gestartet werden
        simulationReady = true;
    }


    // Warteschwelle für den Knopf zum Starten der Simulation in voller Länge. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshhold()
    {
        yield return new WaitForSeconds(1);
        simulationButtonReady = true;
    }


    // Warteschwelle für den Knopf zum Starten der Simulation beginnend beim 1. Fall. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdJumpTo1Button()
    {
        yield return new WaitForSeconds(1);
        jumpTo1ButtonReady = true;
    }

    // Warteschwelle für den Knopf zum Starten der Simulation beginnend beim 2. Fall. Vermeidet Mehrfachklicks
    private IEnumerator WaitingThreshholdJumpTo2Button()
    {
        yield return new WaitForSeconds(1);
        jumpTo2ButtonReady = true;
    }

    // Objekte werden auf Häuser platziert
    private void positionObjects()
    {
        setObjectHolder(true);
        Vector3 sphereLeftPosition = new Vector3(sphereLeftPositionX, sphereLeftPositionY, sphereLeftPositionZ);
        Vector3 sphereMiddlePosition = new Vector3(sphereMiddlePositionX, sphereMiddlePositionY, sphereMiddlePositionZ);
        Vector3 sphereRightPosition = new Vector3(sphereRightPositionX, sphereRightPositionY, sphereRightPositionZ);
        Vector3 woodenPlatePosition = new Vector3(platePositionX, platePositionY, platePositionZ);
        sphereMiddleLeft.transform.position = sphereMiddlePosition;
        sphereLeft.transform.position = sphereLeftPosition;
        sphereMiddleRight.transform.position = sphereRightPosition;
        woodenPlate.transform.position = woodenPlatePosition;
    }

    // Objekthalterung können aktiviert und deaktiviert werden. Damit können die Objekte dementsprechend darauf platziert werden oder fallengelassen werden
    private void setObjectHolder(Boolean value)
    {
        sphereHolder1.SetActive(value);
        sphereHolder2.SetActive(value);
        sphereHolder3.SetActive(value);
        sphereHolder4.SetActive(value);
    }
}
