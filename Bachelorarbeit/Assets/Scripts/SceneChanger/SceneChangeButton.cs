using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class SceneChangeButton : MonoBehaviour {
    
   


    // Funktion zum wechseln zu einer bestimmten Szene, sobald eine Kollision zwischen Oculus Touch Controller und dem Szenenbutton ausgelöst wurde
    void OnTriggerEnter(Collider collider)
    {
        
        SceneChanger.ChangeScene(this.gameObject.name);
    }

}
