using System.Collections;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        SoundManager.instance.PlaySFX(SoundManager.instance.bgmClip);
    }
}
