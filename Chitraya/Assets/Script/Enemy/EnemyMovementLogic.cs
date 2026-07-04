using System.Collections;
using UnityEngine;

public class EnemyMovementLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForced;

    private float currMoveSpeed;

    [SerializeField] private Vector2 dir;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("AI Setting")]
    [SerializeField] private float checkGroundDistance;
    [SerializeField] private float checkWallDistance = 0.5f;
    [SerializeField] private float cliffCheckForwardOffset = 0.5f;


    [Header("AddForced")]
    private bool isKnockback = false;
    [SerializeField] private float durationForced;

    [Header("Error Helper")]
    [SerializeField] private float maxTimeError;
    [SerializeField] private Vector2 currLoc;
    [SerializeField] private Vector2 maxLoc;

    [Header("Reference")]
    private Rigidbody2D rb;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyScript enemyScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomX = (Random.value > 0.5f) ? 1f : -1f;
        dir = new Vector2(randomX, dir.y);
        StartCoroutine(ErrorHelper());
    }

    private void Update()
    {
        if (enemyScript.isDeath)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        // Jika sedang knockback, biarkan Physics Unity bekerja (jangan ditimpa jadi 0)
        if (isKnockback)
        {
            return; // Keluar dari Update agar tidak mengganggu jalannya knockback
        }
        if (enemyAttack.OnAttack)
        {
            currMoveSpeed = 0;
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            Debug.Log("ppp");
            return;
        }
        else
        {
            currMoveSpeed = moveSpeed;
            Debug.Log("KK");
        }

        rb.linearVelocity = new Vector2 (dir.x * currMoveSpeed, rb.linearVelocityY);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, checkGroundDistance, groundLayer);

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, new Vector2(dir.x, 0), checkWallDistance, wallLayer);
        Vector2 cliffCheckOrigin = new Vector2(transform.position.x + (dir.x * cliffCheckForwardOffset), transform.position.y);
        RaycastHit2D groundFront = Physics2D.Raycast(cliffCheckOrigin, Vector2.down, checkGroundDistance, groundLayer);

        if (wallCheck.collider != null || groundFront.collider == null)
        {
            if (isGrounded)
            {
                dir.x *= -1;
            }
        }


    }

    public void AddForced(float forced, float effectDirX, float effectDirY, float dirX)
    {
        isKnockback = true; // Kunci agar Update tidak memaksa velocity menjadi 0
        Vector2 knockbackDirection = new Vector2(dirX * effectDirX, effectDirY);
        knockbackDirection.Normalize();

        // Reset velocity sejenak sebelum dorong agar kekuatannya konsisten
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockbackDirection * forced, ForceMode2D.Impulse);
        StartCoroutine(ForcedBackNormal());
    }

    IEnumerator ForcedBackNormal()
    {
        yield return new WaitForSeconds(durationForced);
        isKnockback = false;    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Garis cek tanah di bawah musuh
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkGroundDistance);

        // Garis cek dinding di depan musuh
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(dir.x, 0) * checkWallDistance);

        //Garis cek jurang di depan bawah musuh
        Gizmos.color = Color.yellow;
        Vector3 cliffCheckOrigin = new Vector3(transform.position.x + (dir.x * cliffCheckForwardOffset), transform.position.y, transform.position.z);
        Gizmos.DrawLine(cliffCheckOrigin, cliffCheckOrigin + Vector3.down * checkGroundDistance);
    }

    IEnumerator ErrorHelper()
    {
        while (true)
        {
            currLoc = transform.position;

            yield return new WaitForSeconds(maxTimeError);

            if (enemyScript.isDeath || isKnockback) continue;

            maxLoc = transform.position;

            float distanceMoved = Vector2.Distance(currLoc, maxLoc);

            if (!enemyAttack.OnAttack && distanceMoved < 0.05f)
            {
                dir.x *= -1; // Putar balik!

                // PENTING: Paksa reset posisi maxLoc setelah memutar arah 
                // agar siklus berikutnya tidak membaca jarak yang salah
                maxLoc = transform.position;
                Debug.Log("AI Macet, memutar arah!");
            }
        }
    }

}
