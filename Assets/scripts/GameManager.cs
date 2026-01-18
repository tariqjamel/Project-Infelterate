using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class GameManager : MonoBehaviour
{
    public bool hasBriefcase = false; 

    public void CollectBriefcase()
    {
        hasBriefcase = true;
        Debug.Log("Objective Secured!"); 
    }

    public void WinGame()
    {
        // Only win if we have the briefcase
        if (hasBriefcase)
        {
            Debug.Log("YOU WIN!");
            
            // 1. Unlock the mouse cursor (So you can click buttons in the Win screen)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // 2. Load the Win Scene
            // IMPORTANT: The name inside "" must match your scene name EXACTLY.
            SceneManager.LoadScene("WinScene"); 
        }
        else
        {
            Debug.Log("You cannot leave without the Briefcase!");
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
