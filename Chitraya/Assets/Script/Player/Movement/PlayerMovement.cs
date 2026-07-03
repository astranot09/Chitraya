using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Direction")]
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 lastDirection;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundRadius;
    [SerializeField] private bool isGrounded;


    [Header("State Setting")]
    [SerializeField] private bool onJumping;
    [SerializeField] private bool onDashing;
    [SerializeField] private bool onWindVent;


    [Header("Reference")]
    [SerializeField] private PlayerMovementStat stat;
    [SerializeField] private Rigidbody2D rb;

    public void Update()
    {
        rb.linearVelocity = new Vector2(direction.x * stat.maxRunSpeed , rb.linearVelocityY);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundRadius, stat.groundLayer);
    }



    public void PlayerJump()
    {
        if (isGrounded)
        {
            rb.linearVelocityY = stat.jumpForced;
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
    public void SetLastDirection(Vector2 dir)
    {
        lastDirection = dir;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundRadius);
        }
    }
}
