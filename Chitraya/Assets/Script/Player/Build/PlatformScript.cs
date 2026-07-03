using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    [SerializeField] private float platformDuration = 2f;
    void Start()
    {
        Destroy(gameObject, platformDuration);
    }

    private void OnDestroy()
    {
        PlayerBuildingScript.instance.ResetCurrentPlatformSpawn();
    }

}
