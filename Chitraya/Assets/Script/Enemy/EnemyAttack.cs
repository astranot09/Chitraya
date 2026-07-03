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

    [Header("Attack")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackDelay;
    [SerializeField] private float warningDelay;
    private Coroutine attackCoroutine;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float currCooldown;

    [Header("Knockback")]
    [SerializeField] private float knockbackForced = 2;
    [SerializeField] private float effectX = -1;
    [SerializeField] private float effectY = 1;
    

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;

    [Header("Reference")]
    [SerializeField] private EnemyMovementLogic enemyMovementLogic;
    [SerializeField] private TrajectoryLine trajectoryLine;

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
            onAttack = true;
            onAiming = true;
            yield return new WaitForSeconds(attackDelay);

            onPrepShoot = true;
            yield return new WaitForSeconds(warningDelay);

            onPrepShoot = false;
            onAiming = false;
            enemyMovementLogic.AddForced(knockbackForced, effectX, effectY);
            CheckHit();
            //yield return new WaitForSeconds(attackDuration);
            currCooldown = attackCooldown;
            onAttack = false;
        }
        attackCoroutine = null;
    }

    public void CheckHit()
    {
        if (trajectoryLine.PlayerHit())
        {
            Debug.Log("Kena Damage");
            PlayerScript.instance.TakeDamage(damage);
        }
    }

}