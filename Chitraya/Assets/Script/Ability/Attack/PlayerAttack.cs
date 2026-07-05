using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] private GameObject weaponBulletPrefab;
    [SerializeField] private Transform weaponSpawner;
    [SerializeField] private float cooldownRange;
    [SerializeField] private bool canAttack;
    [SerializeField] private int bulletDamage = 1;

    [Header("Melee")]
    [SerializeField] private CapsuleCollider2D weaponCollider;
    [SerializeField] private int meleeDamage = 1;
    [SerializeField] private float cooldownMelee;


    [Header("Reference")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;


    private void Start()
    {
        canAttack = true;
    }

    public void MeleeAttack()
    {
        weaponCollider.enabled = true;
    }
    public void MeleeDoneAttack()
    {
        weaponCollider.enabled = false;
        DoneAttacking(cooldownMelee);
    }

    public void ShootProjectile()
    {
        canAttack = false;
        DoneAttacking(cooldownRange);
        GameObject x = Instantiate(weaponBulletPrefab, weaponSpawner.position, Quaternion.identity);
        PlayerBulletScript playerBulletScript = x.GetComponent<PlayerBulletScript>();
        playerBulletScript.SetUp(playerMovement.LastDirection, bulletDamage);
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
                enemy.TakeDamage(meleeDamage);
            }
        }
    }

    IEnumerator cooldownAttack(float x)
    {
        canAttack = false;
        yield return new WaitForSeconds(x);
        canAttack = true;
    }

    public void PlayerAttacking()
    {
        if (!canAttack) return;
        if(!playerMovement.IsGrounded) return;
        canAttack = false;
        int index = PlayerInputController.instance.Index;
        
        if (index == 1)
        {
            animator.SetTrigger("MeleeAttack");
            SoundManager.instance.PlaySFX(SoundManager.instance.melee);
        }
        if (index == 2)
        {
            animator.SetTrigger("RangedAttack");
            SoundManager.instance.PlaySFX(SoundManager.instance.range);
        }
    }

    public void DoneAttacking(float x)
    {
        StartCoroutine(cooldownAttack(x));
    }
}
