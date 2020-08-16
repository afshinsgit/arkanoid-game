using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void ExitToWindow()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }
}
