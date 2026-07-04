using UnityEngine;

public class SmasherScript : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private bool canDamage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDamage)
        {
            GiveDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            GiveDamage();
        }
    }

    public void SetCanDamage()
    {
        canDamage = true;
    }
    public void SetCantDamage()
    {
        canDamage = false;
    }
    public void GiveDamage()
    {
        canDamage = false;
        PlayerScript.instance.TakeDamage(damage);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
