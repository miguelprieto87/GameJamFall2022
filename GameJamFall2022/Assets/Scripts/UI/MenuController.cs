using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void RetryLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
