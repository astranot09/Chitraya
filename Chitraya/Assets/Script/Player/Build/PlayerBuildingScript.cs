using UnityEngine;

public class PlayerBuildingScript : MonoBehaviour
{

    public static PlayerBuildingScript instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    [Header("Platform")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform platformSpawner;

    [Header("Setting Platform")]
    [SerializeField] private int maxPlatformSpawned = 2;
    [SerializeField] private int currPlatform;

    [Header("WindVent")]
    [SerializeField] private GameObject windVentPrefab;
    [SerializeField] private Transform windVentSpawner;

    [Header("Setting WindVent")]
    [SerializeField] private int maxWindVentSpawned = 1;
    [SerializeField] private int currWindVent;



    [Header("Change Build")]
    [SerializeField] private bool onActivated;
    [SerializeField] private int indexBuild; //1 untuk platform, 2 untuk WindVent
    public void PlayerBuilding(Vector2 location)
    {
        switch (indexBuild)
        {
            case 1:
                if (currPlatform >= maxPlatformSpawned) return;

                Instantiate(platformPrefab, location, Quaternion.identity, platformSpawner);
                currPlatform++;
                break;
            case 2:
                if (currWindVent >= maxWindVentSpawned) return;

                Instantiate(windVentPrefab, location, Quaternion.identity, platformSpawner);
                currWindVent++;
                break;
        }
        
    }

    public void ResetCurrentPlatformSpawn()
    {
        currPlatform--;
    }
    public void ResetCurrentWindVentSpawn()
    {
        currWindVent--;
    }
}
