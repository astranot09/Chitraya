using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void StartIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadSceneName(string x)
    {
        SceneManager.LoadScene(x);
    }
}
