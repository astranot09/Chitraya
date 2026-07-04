using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private Vector2 dir;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }
    public void SetUp(Vector2 dir, int x)
    {
        this.dir = dir;
        if(this.dir.x == 0)
        {
            this.dir.x = 1;
        }
        bulletDamage = x;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(dir.x * bulletSpeed, rb.linearVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attackkk");
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("PPPP");
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
        else if(collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
