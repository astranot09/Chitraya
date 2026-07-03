using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("Width Settings")]
    [SerializeField] private float startWidth = 1f;
    [SerializeField] private float endWidth = 1f;

    [Header("Laser Line")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PolygonCollider2D polygonCollider;
    private Vector2[] colliderPoints = new Vector2[4];

    [Header("PlayerHit")]
    [SerializeField] private bool playerHit;

    [Header("Reference")]
    [SerializeField] private EnemyAttack enemyAttack;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        polygonCollider.enabled = false;
    }

    private void Update()
    {
        if (enemyAttack.OnAiming && player != null)
        {
            lineRenderer.enabled = true;
            polygonCollider.enabled = true;

            Vector3 startPos = transform.position;
            Vector3 endPos = player.transform.position;

            if (!enemyAttack.OnPrepShoot)
            {
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, endPos);
                UpdatePolygonCollider(startPos, endPos);
            }
            
        }
        else
        {
            lineRenderer.enabled = false;
            polygonCollider.enabled = false;
        }
    }

    private void UpdatePolygonCollider(Vector3 start, Vector3 end)
    {
        // Hubungkan posisi awal dan akhir dalam koordinat lokal langsung
        Vector2 localStart = transform.InverseTransformPoint(start);
        Vector2 localEnd = transform.InverseTransformPoint(end);

        // Cari arah lurus lokal
        Vector2 direction = (localEnd - localStart).normalized;

        // Cari arah tegak lurus lokal (atas-bawah)
        Vector2 normal = new Vector2(-direction.y, direction.x);

        // Susun 4 titik sejajar dengan ketebalan visualnya
        colliderPoints[0] = localStart + normal * (startWidth / 2f); // Pangkal Atas
        colliderPoints[1] = localStart - normal * (startWidth / 2f); // Pangkal Bawah
        colliderPoints[2] = localEnd - normal * (endWidth / 2f);     // Ujung Bawah
        colliderPoints[3] = localEnd + normal * (endWidth / 2f);     // Ujung Atas

        // Terapkan ke PolygonCollider2D
        polygonCollider.SetPath(0, colliderPoints);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHit = false;
        }
    }

    public bool PlayerHit()
    {
        return playerHit;
    }
}
