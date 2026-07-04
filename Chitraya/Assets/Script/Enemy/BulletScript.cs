using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxAngle;
    [SerializeField] private float destroyTime = 2f;
    [SerializeField] private int damage = 1;

    [SerializeField] private Vector2 playerLocation;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerLocation = player.transform.position;

            Vector2 forwardDirection;
            if(playerLocation.x < transform.position.x)
            {
                forwardDirection = Vector2.left;
            }
            else
            {
                forwardDirection = Vector2.right;
            }

            // 2. Hitung arah asli menuju Player
            Vector2 directionToPlayer = (playerLocation - (Vector2)transform.position).normalized;

            // 3. Hitung sudut antara arah depan peluru dengan arah ke Player
            float angleToPlayer = Vector2.SignedAngle(forwardDirection, directionToPlayer);

            // 4. Batasi (Clamp) sudut tersebut berdasarkan maxAngle
            float clampedAngle = Mathf.Clamp(angleToPlayer, -maxAngle, maxAngle);

            // 5. Putar arah forwardDirection tadi berdasarkan sudut yang sudah dibatasi
            Vector2 finalDirection = Quaternion.Euler(0, 0, clampedAngle) * forwardDirection;

            // 6. Putar rotasi peluru agar menghadap ke arah terbangnya (opsional)
            float lookAngle = Mathf.Atan2(finalDirection.y, finalDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            // 7. Berikan kecepatan ke Rigidbody2D
            rb.linearVelocity = finalDirection * bulletSpeed;
            // Catatan: Jika kamu menggunakan Unity versi lama (< 2023), ganti 'linearVelocity' menjadi 'velocity'
        }
        else
        {
            // Jika player tidak ditemukan, peluru lurus saja ke depan
            rb.linearVelocity = transform.right * bulletSpeed;
        }


        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerScript.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }




}
