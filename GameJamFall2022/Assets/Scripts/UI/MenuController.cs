using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int index;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("level");
    }

    public void QuitGame()
    {
        print("quitting");
        Application.Quit();
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(index);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
