using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("ChessGameScene");
    }
    public void QuitGame()
    {
        Debug.Log("game over");
        Application.Quit();
    }
}
