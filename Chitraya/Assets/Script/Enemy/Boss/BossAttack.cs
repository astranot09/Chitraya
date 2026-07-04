using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossAttack : MonoBehaviour
{
    [Header("OnTrigger")]
    [SerializeField] private Coroutine bossStart;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown = 3f;

    [SerializeField] private int damage = 1;
    [SerializeField] private bool onAiming;

    [Header("Attack Type Smashing")]
    [SerializeField] private GameObject smashingPrefab;
    [SerializeField] private int maxSmashing = 3;
    [SerializeField] private float delayBeforeNextSmashing = 1f;
    [SerializeField] private float yOffSet = 1;

    [Header("Attack Type Laser (Sprite Parallel)")]
    [Tooltip("Lebar total jangkauan laser kiri-ke-kanan dalam satuan unit Unity.")]
    [SerializeField] private float laserWidth = 3f;
    [SerializeField] private Transform fovPoint;
    [SerializeField] private float range = 8f;
    [SerializeField] private Light2D lightWarning;

    [Header("Attack Setting Type Laser")]
    [SerializeField] private float laserCountdown = 1f;
    [SerializeField] private float laserDelayBeforeAttack = 1f;
    [SerializeField] private float laserDuration = 1f;
    [SerializeField] private bool onLaser;

    [Header("Multiple Raycast Settings")]
    [Tooltip("Jumlah garis raycast sejajar. Makin rapat makin akurat.")]
    [SerializeField] private int rayCount = 10;

    [Header("Targeting")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform playerLocation;

    [Header("Reference")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        TrackingPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bossStart == null && collision.CompareTag("Player"))
        {
            bossStart = StartCoroutine(EnemyAttackLoop());
            SoundManager.instance.PlayBGM(SoundManager.instance.bossClip);
        }
    }

    IEnumerator EnemyAttackLoop()
    {
        yield return new WaitForSeconds(attackCooldown);
        switch (RandomizeAttackType())
        {
            case 0:
                SmashingAttackLogic();
                break;
            case 1:
                LaserAttackLogic();
                break;
        }
    }

    private int RandomizeAttackType()
    {
        return Random.Range(0, 2);
    }

    private void Update()
    {
        if (playerLocation == null) return;
        RotateTowardsPlayer();

        if (onLaser)
            LaserCheckDamage();
    }

    private void RotateTowardsPlayer()
    {
        if (!onAiming) return;

        Vector2 direction = fovPoint.position - playerLocation.position;

        // Memperbaiki typo comment '//' dan mengaktifkan -90f agar moncong atas (Up) yang membidik
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        fovPoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SmashingAttackLogic()
    {
        StartCoroutine(SmashingTime());
    }
    public void LaserAttackLogic()
    {
        StartCoroutine(LaserShoot());
    }


    IEnumerator SmashingTime()
    {
        for (int i = 0; i < maxSmashing; i++)
        {
            Instantiate(smashingPrefab, new Vector2(playerLocation.position.x, playerLocation.position.y + yOffSet), Quaternion.identity);
            yield return new WaitForSeconds(delayBeforeNextSmashing);
        }
        StartCoroutine(EnemyAttackLoop());
    }


    IEnumerator LaserShoot()
    {
        animator.SetBool("isAttack", true);

        lightWarning.color = Color.red;

        lightWarning.enabled = true;
        onAiming = true;
        yield return new WaitForSeconds(laserCountdown);

        onAiming = false;
        lightWarning.color = Color.yellow;
        yield return new WaitForSeconds(laserDelayBeforeAttack);
        SoundManager.instance.PlaySFX(SoundManager.instance.laserBoss);
        lightWarning.color = Color.red;
        onLaser = true;

        float timer = 0f;
        while (timer < laserDuration && onLaser)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        onLaser = false;
        lightWarning.enabled = false;
        animator.SetBool("isAttack", false);
        StartCoroutine(EnemyAttackLoop());
    }

    private void LaserCheckDamage()
    {
        Vector2 forwardDirection = -fovPoint.right; // Menembak ke arah kiri objek
        Vector2 rightDirection = fovPoint.up;

        bool canSeePlayer = false;

        // Jarak spasi antar garis raycast sejajar
        float step = laserWidth / rayCount;

        // Loop untuk menembakkan raycast sejajar (Paralel) dari kiri ke kanan balok laser
        for (int i = 0; i <= rayCount; i++)
        {
            // Menghitung titik offset dari kiri (-laserWidth/2) ke kanan (+laserWidth/2)
            float offset = (-laserWidth / 2f) + (i * step);

            // Titik pangkal tembak digeser ke samping sesuai ketebalan laser kotak kamu
            Vector2 rayOrigin = (Vector2)fovPoint.position + (rightDirection * offset);

            // Tembakkan ke depan lurus secara paralel!
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, forwardDirection, range, targetMask);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    canSeePlayer = true;
                    Debug.DrawLine(rayOrigin, hit.point, Color.green);
                    break;
                }
                else
                {
                    Debug.DrawLine(rayOrigin, hit.point, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, forwardDirection * range, Color.red);
            }
        }

        if (canSeePlayer)
        {
            Debug.Log("Player Terkena Laser Kotak! Matikan Laser.");
            GiveDamageToPlayer(damage);
            onLaser = false;
        }
    }

    private void TrackingPlayer()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null) playerLocation = target.transform;
    }

    private void GiveDamageToPlayer(int damage)
    {
        if (PlayerScript.instance != null) PlayerScript.instance.TakeDamage(damage);
    }
}