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

    [Header("Icon Build")]
    [SerializeField] private GameObject iconBuild;
    [SerializeField] private float iconOffSetX = -0.2f;
    [SerializeField] private float iconOffSetY = 0.2f;

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
    public void PlayerBuilding(Vector2 location)
    {
        switch (PlayerInputController.instance.Index)
        {
            case 3:
                if (currPlatform >= maxPlatformSpawned) return;

                Instantiate(platformPrefab, location, Quaternion.identity, platformSpawner);
                Instantiate(iconBuild, new Vector2(location.x + iconOffSetX, location.y + iconOffSetY), Quaternion.identity, platformSpawner);
                currPlatform++;
                break;
            //case 4:
            //    if (currWindVent >= maxWindVentSpawned) return;

            //    Instantiate(windVentPrefab, location, Quaternion.identity, platformSpawner);
            //    currWindVent++;
            //    break;
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
