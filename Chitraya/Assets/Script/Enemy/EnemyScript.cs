using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public bool isDeath { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private BossAttack bossAttack;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("aduh");
        SoundManager.instance.PlaySFX(SoundManager.instance.hit);
        currHealth -= damage;
        StartCoroutine(FlashAnimationDamage());

        if(currHealth <= 0)
        {
            isDeath = true;
            animator.SetTrigger("Death");
        }
    }

    IEnumerator FlashAnimationDamage()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public void Death()
    {
        if (bossAttack != null)
        {
            bossAttack.DestroyPembatas();
        }
        Destroy(gameObject);
    }
}
