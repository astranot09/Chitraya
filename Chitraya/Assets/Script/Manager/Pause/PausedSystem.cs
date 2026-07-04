using UnityEngine;

public class PausedSystem : MonoBehaviour
{
    public static PausedSystem instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool OnPaused { get; private set; }

    public void PausedGame()
    {
        OnPaused=true;
        Time.timeScale = 0;
    }

    public void UnPausedGame()
    {
        OnPaused = false;
        Time.timeScale = 1;
    }

}
