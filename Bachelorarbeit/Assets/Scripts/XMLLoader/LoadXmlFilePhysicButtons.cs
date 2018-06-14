using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Skript zum Einlesen der Buttons, die zu Unterszenen des Physikbereichs wechseln
public class LoadXmlFilePhysicButtons : MonoBehaviour {

    public TextAsset xmlRawFile;
    public Material buttonMaterial;

    // Positionsdaten für die Erstellung der Buttons
    private float xValueScale = 1F;
    private float yValueScale = 0.4F;
    private float zValueScale = 0.1F;
    private float xValueLocation = 19.525F;
    private float yValueLocation = 2.639F;
    private float zValueLocation = 23.944F;
    private float xValueRotation = 0F;
    private float yValueRotation = 0F;
    private float zValueRotation = 0F;
    private int buttonCounter = 0;

    private Scene currentScene;

    // Beim Start wird der Name der Datei eingelesen und die Funktion aufgerufen, die die Buttonnamen und Szenennamen aus der XML-Datei einliest und dafür ein Würfelobjekt und ein Textmesh erstellt,
    // der den Namen des Buttons beinhaltet und der Button wird an die entsprechende Stelle positioniert.
    void Start () {
       
        string data = xmlRawFile.text;
        parseXmlFile(data);
    }
    //Funktion, die die Buttonnamen und Szenennamen aus der XML-Datei einliest und dafür ein Würfelobjekt und ein Textmesh erstellt,
    // der den Namen des Buttons beinhaltet und der Button wird an die entsprechende Stelle positioniert.
    void parseXmlFile(string xmlData)
    {
        string Value = "";
       
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//ButtonCollection/Button";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach(XmlNode node in myNodeList)
        {

            if(buttonCounter == 3)
            {
                xValueLocation = 19.525F;
                yValueLocation = 2.14F;
            }
            if(buttonCounter == 6)
            {
                xValueLocation = 19.525F;
                yValueLocation = 1.639F;
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
            t.transform.position = new Vector3(xValueLocation-0.48F, yValueLocation+0.1F, zValueLocation - 0.05F);
            t.transform.localEulerAngles = new Vector3(xValueRotation, yValueRotation, zValueRotation);
            t.transform.localScale = new Vector3(0.135F, 0.135F, 1);
            t.fontSize = 13;
            xValueLocation = xValueLocation + 1.07F;
            Cube.name = sceneName.InnerXml;
            Value = "Buttonname:" + buttonName.InnerXml + "Scenename:" + sceneName.InnerXml;
            Debug.Log(Value);
            buttonCounter++;
        }

    }
}

