using UnityEngine;

public class FinishManager : MonoBehaviour
{
    public static FinishManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private bool finished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !finished)
        {
            finished = true;
            GameFinished();
        }
    }

    public void GameFinished()
    {
        SceneController.instance.LoadSceneName("EndingScene");
    }
}
