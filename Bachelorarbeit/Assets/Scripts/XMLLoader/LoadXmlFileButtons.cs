using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadXmlFileButtons : MonoBehaviour {

    public TextAsset xmlRawFile;
    public TextAsset xmlRawFileQuestion;
 //   public TextAsset xmlLocationFile;
    public Material buttonMaterial;

    // Positionsdaten für die Erstellung der Buttons
    private float xValueScale = 1.5F;
    private float yValueScale = 0.7F;
    private float zValueScale = 0.1F;
    private float xValueLocation = 9F;
    private float yValueLocation = 2.7F;
    private float zValueLocation = 23.8F;
    private float xValueRotation = 0F;
    private float yValueRotation = 0F;
    private float zValueRotation = 0F;
    private int buttonCounter = 0;

    private Scene currentScene;

    // Beim Start wird die momentane Szene auf Lobby und Geographie überprüft. Falls die Szene nicht Lobby oder Geographie ist, werden die Fragen aus der XML Datei eingelesen und in Fragenobjekte 
    // umgewandelt. Die Fragen der Geographieszene haben ein anderes Format, daher werden die Fragen für Geographie aus einem anderen Skript gelesen
    void Start () {

        // Liest momentane Szene ein
        currentScene = SceneManager.GetActiveScene();
        string data = xmlRawFile.text;

        // Überprüft auf Geographie und Lobby, falls die aktuelle Szene keine der beiden ist werden die Fragen eingelesen
        if (!currentScene.name.Equals("Lobby") && !currentScene.name.Equals("Geography"))
        {
            if (xmlRawFileQuestion.text != null)
            {
                string questionData = xmlRawFileQuestion.text;
                LoadXmlFileQuestions.parseXmlFile(questionData);
            }
        }
      
        parseXmlFile(data);
    }
	
    // Funktion, der ein String übergeben wird, der dem Namen der XML Datei entspricht. Es werden die Namen und Szene der Buttons zum für den Wechsel zu den entsprechenden Szenen eingelesen.
    // Es wird ein Würfelobjekt als Button erstellt, über den ein Textmesh erstellt wird, der den Namen des Buttons beinhaltet. Würfel und Textmesh werden an die entsprechende Position gebracht
    // , die bei jeder Erstellung eines weiteren Button weiter verschoben wird.
    void parseXmlFile(string xmlData)
    {
        string value = "";
       
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//ButtonCollection/Button";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach(XmlNode node in myNodeList)
        {

            if(buttonCounter == 5)
            {
                yValueLocation = 1.9F;
                xValueLocation = 9F;
            }
            if(buttonCounter == 10)
            {
                yValueLocation = 1.1F;
                xValueLocation = 9F;
            }
            XmlNode buttonName = node.FirstChild;
            XmlNode sceneName = buttonName.NextSibling;
            GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Cube.transform.position = new Vector3(xValueLocation, yValueLocation, zValueLocation);
            Cube.transform.localEulerAngles = new Vector3(xValueRotation, yValueRotation, zValueRotation);
            Cube.transform.localScale= new Vector3(xValueScale, yValueScale, zValueScale);
            Cube.GetComponent<Renderer>().material = buttonMaterial;
            var buttonScript = Cube.AddComponent<SceneChangeButton>();
            BoxCollider bc = Cube.AddComponent<BoxCollider>();
            GameObject TextHolderObject = new GameObject();
            TextHolderObject.transform.position = new Vector3(xValueLocation, yValueLocation, zValueLocation);
            TextHolderObject.transform.localEulerAngles = new Vector3(xValueRotation, yValueRotation, zValueRotation);
            TextMesh t =TextHolderObject.AddComponent<TextMesh>();
            t.text = buttonName.InnerXml;
            t.GetComponent<TextMesh>().alignment = TextAlignment.Center;
            t.color = Color.black;
            t.transform.position = new Vector3(xValueLocation-0.7F, yValueLocation+0.15F, zValueLocation - 0.05F);
            t.transform.localEulerAngles = new Vector3(xValueRotation, yValueRotation, zValueRotation);
            t.transform.localScale = new Vector3(0.135F, 0.135F, 1);
            t.fontSize = 20;
            xValueLocation = xValueLocation + 1.6F;
            Cube.name = sceneName.InnerXml;
            value = "Buttonname:" + buttonName.InnerXml + "Scenename:" + sceneName.InnerXml;
            Debug.Log(value);
            buttonCounter++;
        }

    }
}
