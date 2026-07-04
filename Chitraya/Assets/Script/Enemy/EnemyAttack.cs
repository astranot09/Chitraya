using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private bool onAttack;
    public bool OnAttack => onAttack;
    [SerializeField] private bool onAiming;
    public bool OnAiming => onAiming;

    [SerializeField] private bool onPrepShoot;
    public bool OnPrepShoot => onPrepShoot;

    [Header("Player")]
    [SerializeField] private bool inRange;
    public bool InRange => inRange;

    [Header("Attack")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackDelay;
    [SerializeField] private float warningDelay;
    private Coroutine attackCoroutine;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float currCooldown;
    private bool alreadyRotate;

    [Header("Knockback")]
    [SerializeField] private float knockbackForced = 2;
    [SerializeField] private float effectX = -1;
    [SerializeField] private float effectY = 1;
    

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawner;

    [Header("Reference")]
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyMovementLogic enemyMovementLogic;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            if(attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(EnemyShoot());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine); // Hentikan lewat variabelnya
                attackCoroutine = null;
            }
            onAiming = false;
            onPrepShoot = false;
            onAttack = false; // Pastikan status menyerang mati saat ditinggal player
        }
    }


    private void Update()
    {

        if (InRange)
        {
            EnemyTracking();
        }


        if(currCooldown > 0)
        {
            currCooldown -= Time.deltaTime;
        }
        else
        {
            currCooldown = 0;
        }
    }


    public IEnumerator EnemyShoot()
    {
        while (inRange)
        {
            if (currCooldown > 0)
            {
                yield return null; // Tunggu 1 frame, lalu cek kondisi while lagi
                continue;          // Skip kode di bawah, balik lagi ke atas loop
            }

            //1 diem
            onAttack = true;
            onAiming = true;
            yield return new WaitForSeconds(attackDelay);


            //2 delay dikit
            onPrepShoot = true;
            yield return new WaitForSeconds(warningDelay);

            //3 nembak
            onPrepShoot = false;
            onAiming = false;

            SpawnBullet();
            enemyMovementLogic.AddForced(knockbackForced, effectX, effectY, Mathf.Clamp(player.transform.position.x, -1, 1));

            //CheckHit();
            //yield return new WaitForSeconds(attackDuration);

            currCooldown = attackCooldown;
            onAttack = false;
        }
        attackCoroutine = null;
    }

    //public void CheckHit()
    //{
    //    if (trajectoryLine.PlayerHit())
    //    {
    //        Debug.Log("Kena Damage");
    //        PlayerScript.instance.TakeDamage(damage);
    //    }
    //}

    public void SpawnBullet()
    {
        Debug.Log("tembak");

        if(bullet != null)
            Instantiate(bullet, bulletSpawner.position, Quaternion.identity);
    }

    public void EnemyTracking()
    {
        if (player.transform.position.x < transform.position.x && alreadyRotate)
        {
            // Set rotasi absolut menghadap kanan (0 derajat)
            transform.parent.rotation = Quaternion.Euler(0, 0, 0);
            alreadyRotate = false;
        }
        // Jika player di sebelah kiri dan musuh sedang menghadap kanan (alreadyRotate = false)
        else if (player.transform.position.x > transform.position.x && !alreadyRotate)
        {
            // Set rotasi absolut menghadap kiri (180 derajat)
            transform.parent.rotation = Quaternion.Euler(0, 180, 0);
            alreadyRotate = true;
        }
    }
}