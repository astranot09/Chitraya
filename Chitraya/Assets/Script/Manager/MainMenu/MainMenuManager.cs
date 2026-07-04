using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PlayBGM(SoundManager.instance.bgmClip);
    }
}
