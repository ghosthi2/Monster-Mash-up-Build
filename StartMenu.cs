using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource musicPlayer;
    public AudioSource uiPlayer;
    public IEnumerator StartGame()
    {
        musicPlayer.Stop();
        uiPlayer.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ControlsScene"); //load ControlsScene
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGameButton()
    {
       StartGame();
    }

    private void Update()
    {
        if (Input.anyKey || Time.time > 132f)
        {
            StartCoroutine("StartGame");
        }
    }
}
