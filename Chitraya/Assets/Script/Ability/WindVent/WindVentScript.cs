using UnityEngine;

public class WindVentScript : MonoBehaviour
{
    [Header("WindVent")]
    public float windForce;
    public float windForceDuration;

    private void Start()
    {
        Destroy(gameObject,windForceDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hola");
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            rb.AddForce(Vector2.up * windForce,ForceMode2D.Force);
            rb.linearVelocity = new Vector2(rb.linearVelocityX, windForce);
        }
    }

    private void OnDestroy()
    {
        PlayerBuildingScript.instance.ResetCurrentWindVentSpawn();
    }
}
