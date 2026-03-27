using UnityEngine;
using System.Collections;

public class SimplePatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float walkTime = 4f;
    public float waitTime = 1f;

    [Header("Damage")]
    public int damageAmount = 1;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            isWaiting = false;
            yield return new WaitForSeconds(walkTime);

            isWaiting = true;
            rb.linearVelocity = Vector2.zero; // Stop bewegen tijdens wachten
            yield return new WaitForSeconds(waitTime);

            movingRight = !movingRight; // Richting omkeren
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (!isWaiting)
        {
            float moveDirection = movingRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
        }
    }


    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Horizontaal spiegelen
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player! Dealing damage...");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
