using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Direction")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 lastDirection;


    [Header("State Setting")]
    [SerializeField] private bool onDashing;
    [SerializeField] private bool onWindVent;


    [Header("Reference")]

    [SerializeField] private PlayerMovementStat stat;

    [SerializeField] private Rigidbody2D rb;

    public void Update()
    {
        rb.linearVelocity = new Vector2(direction.x * stat.maxRunSpeed , rb.linearVelocityY);

        if (onWindVent)
        {
            rb.AddForce(Vector2.up * stat.windForce, ForceMode2D.Force);
        }

    }



    public void PlayerJump()
    {
        rb.linearVelocityY = stat.jumpForced;
    }



    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
    public void SetLastDirection(Vector2 dir)
    {
        lastDirection = dir;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("tRIGGER");
        if (collision.CompareTag("WindVent"))
        {
            onWindVent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WindVent"))
        {
            onWindVent = false;
        }
    }

}
