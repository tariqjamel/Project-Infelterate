using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Call this to load the Game Level
    public void PlayGame()
    {
        SceneManager.LoadScene("Level"); 
    }

    // Call this to go back to Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("First");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level");
    }

    // Call this to Quit
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
    
    public void LoadFailedScene()
    {
        // IMPORTANT: Make sure your scene is named "FailedScene" exactly!
        SceneManager.LoadScene("FailedScene");
    }
}
