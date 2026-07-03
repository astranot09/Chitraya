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

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform platformSpawner;

    [Header("Setting")]
    [SerializeField] private int maxSpawner = 2;
    [SerializeField] private int currSpawner;
    
    public void PlayerBuilding(Vector2 location)
    {
        if (currSpawner >= maxSpawner) return;

        Instantiate(platformPrefab, location, Quaternion.identity, platformSpawner);
        currSpawner++;
    }


    public void ResetCurrentPlatformSpawn()
    {
        currSpawner--;
    }
}
