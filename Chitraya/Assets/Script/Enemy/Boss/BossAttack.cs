using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossAttack : MonoBehaviour
{
    [Header("OnTrigger")]
    [SerializeField] private Coroutine bossStart;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown = 3f;


    [Header("Attack Type Laser")]
    [SerializeField] private float fovAngle = 90f;
    [SerializeField] private Transform fovPoint;
    [SerializeField] private float range = 8f;
    [SerializeField] private Light2D lightWarning;
    [SerializeField] private Sprite laserSprite;
    [SerializeField] private bool laserTypeAttack;

    [Header("Attack Setting Type Laser")]
    [SerializeField] private float laserCountdown = 1f;
    [SerializeField] private float laserDuration = 1f;

    [Header("Multiple Raycast Settings")]
    [Tooltip("Jumlah total garis raycast yang ditembakkan. Makin banyak makin akurat tapi beban CPU naik.")]
    [SerializeField] private int rayCount = 10;

    [Header("Targeting")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform playerLocation;

    private void Start()
    {
        TrackingPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bossStart == null)
        {
            bossStart = StartCoroutine(EnemyAttackLoop());
        }
    }


    IEnumerator EnemyAttackLoop()
    {
        while (true)
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
    }


    private int RandomizeAttackType()
    {
        return Random.Range(0, 2);
    }






    private void Update()
    {
        if (playerLocation == null) return;
        RotateTowardsPlayer();

        // ============================== LASER TYPE =========================================

        if (laserTypeAttack)
        {
            Vector2 forwardDirection = fovPoint.up;

            // Hitung sudut awal dan sudut akhir dari jangkauan kipas FOV
            float halfFOV = fovAngle / 2f;
            float baseAngle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;

            float startAngle = baseAngle - halfFOV;
            float endAngle = baseAngle + halfFOV;

            bool canSeePlayer = false;

            // Loop untuk menembakkan raycast satu per satu membentuk kipas
            for (int i = 0; i <= rayCount; i++)
            {
                // Interpolasi sudut dari startAngle ke endAngle
                float t = (float)i / rayCount;
                float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

                // Ubah sudut derajat menjadi Vector2 arah
                Vector2 rayDirection = AngleToVector(currentAngle);

                // Tembakkan Raycast
                RaycastHit2D hit = Physics2D.Raycast(fovPoint.position, rayDirection, range, targetMask);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // Salah satu raycast berhasil mengenai player tanpa terhalang tembok!
                        canSeePlayer = true;
                        Debug.DrawLine(fovPoint.position, hit.point, Color.green);
                    }
                    else
                    {
                        // Raycast menabrak tembok/ground sebelum mencapai jarak maksimal
                        Debug.DrawLine(fovPoint.position, hit.point, Color.yellow);
                    }
                }
                else
                {
                    // Raycast tidak menabrak apa-apa sampai batas range
                    Debug.DrawRay(fovPoint.position, rayDirection * range, Color.red);
                }
            }

            // Eksekusi keputusan akhir setelah semua ray selesai memeriksa area
            if (canSeePlayer)
            {
                Debug.Log("Liat Player!");
                // Jalankan fungsi nembak/serang kamu di sini
            }
            else
            {
                Debug.Log("Ga liat - Player sembunyi atau di luar area.");
            }
        }

    }

    private void RotateTowardsPlayer()
    {
        // Hitung arah vector dari fovPoint menuju posisi Player
        Vector2 direction = playerLocation.position - fovPoint.position;

        // Hitung sudut derajat menggunakan Atan2
        // Dikurang 90 derajat karena acuan utama kode kamu menggunakan sumbu Y (fovPoint.up)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Ubah rotasi fovPoint secara absolut pada sumbu Z
        fovPoint.rotation = Quaternion.Euler(0, 0, angle);
    }


    // Fungsi pembantu untuk mengubah sudut derajat menjadi Vector2 Arah
    private Vector2 AngleToVector(float angleInDegrees)
    {
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    public void SmashingAttackLogic()
    {
        StartCoroutine(LaserShoot());
    }
    public void LaserAttackLogic()
    {
        StartCoroutine(LaserShoot());
    }

    IEnumerator LaserShoot()
    {
        //Warning Nyala
        lightWarning.enabled = true;
        yield return new WaitForSeconds(laserCountdown);


        //Tembak Laser
        lightWarning.lightCookieSprite = laserSprite;
        lightWarning.enabled = false;
    }


    private void LaserSetUp()
    {
        
    }

    private void TrackingPlayer()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

}