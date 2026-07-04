using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        StartCoroutine(FlashAnimationDamage());

        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FlashAnimationDamage()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
