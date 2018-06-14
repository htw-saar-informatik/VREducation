using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {



    // Funktion zum wechseln zu einer bestimmten Szene
	public static void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
