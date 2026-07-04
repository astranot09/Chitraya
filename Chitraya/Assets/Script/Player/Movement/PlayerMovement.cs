using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Direction")]
    [SerializeField] private Vector2 direction;
    public Vector2 Direction => direction;
    [SerializeField] private Vector2 lastDirection;
    public Vector2 LastDirection => lastDirection;

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
    [SerializeField] Animator animator;
    public void Update()
    {
        rb.linearVelocity = new Vector2(direction.x * stat.maxRunSpeed , rb.linearVelocityY);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundRadius, stat.groundLayer);
        animator.SetFloat("velocityX", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("velocityY", rb.linearVelocity.y);
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
