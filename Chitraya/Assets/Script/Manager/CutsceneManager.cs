using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] private PlayableDirector cutsceneDirector;

    private void Start()
    {
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
        }
    }

    public void LoadScene()
    {
        if (SceneController.instance != null)
        {
            SceneController.instance.LoadSceneName(sceneName);
        }
    }
}