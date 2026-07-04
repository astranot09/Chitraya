using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    [SerializeField] private int maxHealth = 3;
    public int MaxHealth => maxHealth;
    [SerializeField] private int health;
    public int Health => health;
    private SpriteRenderer spriteRenderer;

    [Header("Reference")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private bool alreadyRotate;
    [SerializeField] private UIManager uiManager;
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        uiManager.UpdateHealthUI();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerMovement != null)
        {
            if(playerMovement.LastDirection.x < 0 && !alreadyRotate)
            {
                transform.Rotate(0, 180, 0);
                alreadyRotate = true;
            }
            else if(playerMovement.LastDirection.x > 0 && alreadyRotate)
            {
                transform.Rotate(0, -180, 0);
                alreadyRotate = false;
            
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        uiManager.UpdateHealthUI();
        StartCoroutine(FlashAnimationDamage());

        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }
    }

    public void OpenDeathUI()
    {
        uiManager.PlayerDeath();
    }

    IEnumerator FlashAnimationDamage()
    {
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
