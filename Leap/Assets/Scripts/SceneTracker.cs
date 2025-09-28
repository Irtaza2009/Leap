using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static string lastLevel;


    public static void GoToGameOver()
    {
        lastLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameOver");
    }

    public static void RestartLevel()
    {
        if (!string.IsNullOrEmpty(lastLevel))
        {
            SceneManager.LoadScene(lastLevel);
        }
        else
        {
            // fallback if lastLevel wasn't set
            Debug.LogWarning("No lastLevel stored. Restarting first scene instead.");
            SceneManager.LoadScene(0);
        }
    }

    public static void RestartFullGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
