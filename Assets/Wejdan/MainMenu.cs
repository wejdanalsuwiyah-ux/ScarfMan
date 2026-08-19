using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // البدء باللعبة وتحميل المشهد القادم في الـ Build Settings
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // الخروج من اللعبة
    public void QuitGame()
    {
        Debug.Log("تم الخروج من اللعبة!"); // يظهر فقط في محرر Unity
        Application.Quit();
    }
}