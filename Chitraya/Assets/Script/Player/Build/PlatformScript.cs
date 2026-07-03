using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnDestroy()
    {
        PlayerBuildingScript.instance.ResetCurrentPlatformSpawn();
    }

}
