using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    [SerializeField] private int health;
    public int Health => health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
