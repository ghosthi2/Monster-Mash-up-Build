using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void SwitchScene(int scene)
    {
        Debug.Log("Attempting to load scene " + scene);
        SceneManager.LoadScene(scene);
    }

    public void SwitchScene(string scene)
    {
        Debug.Log("Attempting to load scene " + scene);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
