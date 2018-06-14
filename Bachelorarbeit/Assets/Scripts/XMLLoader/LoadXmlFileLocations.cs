using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
public class LoadXmlFileLocations : MonoBehaviour {


   
    public Text correctAnswer;
    public TextAsset xmlLocationFile;
    public Text questionText;
    public static string[] locationArray;
    public static int arraySize = 0;
   



    // Beim Start wird der Name der XML Datei ausgelesen und die Funktion parseXmlFile aufgerufen, die die gesuchten Umgebungen aus der XML Datei ausliest.
    void Start()
    {
  
            string locationData = xmlLocationFile.text;
            parseXmlFile(locationData);

        
    }
    // Funktion zum Auslesen der gesuchten Umgebungen des Geographiebereichts. Es wird der Name der Datei als String übergeben und ein Array erstellt, das die gesuchten Umgebungen beinhaltet.
    public static void parseXmlFile(string xmlData)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        arraySize = 0;
        locationArray = new string[100];
        string xmlPathPattern = "//LocationCollection/Location";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {

            XmlNode Location = node.FirstChild;
            locationArray[arraySize] = Location.InnerXml;
            arraySize++;
        }
        for(int i = 0; i<arraySize; i++)
        {
            Debug.Log(locationArray[i]);
        }
    }
}
