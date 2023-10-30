using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
