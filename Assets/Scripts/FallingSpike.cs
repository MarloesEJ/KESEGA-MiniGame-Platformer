using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public Rigidbody2D rb;
    public float detectionDistance = 10f;
    public LayerMask playerLayer;
    private bool _hasFallen = false;

    void Update()
    {
        if(_hasFallen) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionDistance, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("Player detected below spike, falling!");
            rb.gravityScale = 3f; // Laat de spike vallen
            _hasFallen = true; // Zorg ervoor dat de spike niet opnieuw valt
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        
        Vector3 localEndPoint = Vector3.down * detectionDistance;
        Gizmos.DrawLine(Vector3.zero, localEndPoint);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.CompareTag("Player"))
    //     {
    //         Debug.Log("Player hit by falling spike!");
    //         PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
    //         if (playerHealth != null)
    //         {
    //             playerHealth.TakeDamage(1); // Geef de speler 1 schade
    //         }

    //         Destroy(gameObject, 0.1f); // Vernietig de spike na het raken van de speler
    //     }
    // }
}
