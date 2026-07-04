using System.Collections;
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
    public bool IsGrounded => isGrounded;


    [Header("State Setting")]
    [SerializeField] private bool onJumping;
    [SerializeField] private bool onDashing;
    [SerializeField] private bool onWindVent;


    [Header("Reference")]
    [SerializeField] private PlayerMovementStat stat;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Animator animator;

    [Header("Dash")]
    [SerializeField] private float dashBuffSpeed = 1f;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashCooldown;

    public void Update()
    {
        rb.linearVelocity = new Vector2(direction.x * stat.maxRunSpeed * dashBuffSpeed, rb.linearVelocityY);
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
    public void PlayerDash()
    {
        Debug.Log("Dashing");
        if (isDashing) return;
        isDashing = true;
        StartCoroutine(DashCountdown());
    }
    IEnumerator DashCountdown()
    {
        dashBuffSpeed = 5f;
        animator.SetBool("isDashing", true);
        yield return new WaitForSeconds(stat.dashingTime);
        dashBuffSpeed = 1f;
        animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
}
