using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private MenuController menuController;

    private void Start()
    {
        menuController = FindObjectOfType<MenuController>();
    }

    public void StartGame()
    {
        menuController.StartGame();
    }

    public void QuitGame()
    {
        menuController.QuitGame();

    }

    public void RetryLevel()
    {
        menuController.RetryLevel();
    }

    public void MainMenu()
    {
        menuController.MainMenu();
    }

    public void Credits()
    {
        menuController.Credits();
    }
}
