using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
  public void GameStart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main_Scene");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
