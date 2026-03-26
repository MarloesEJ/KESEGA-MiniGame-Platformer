using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Animator animator;
    public PlayerMovement movementScript;
    private bool _isDead = false;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player died.");
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    void Die()
    {
        _isDead = true;
        movementScript.enabled = false; // Stop de beweging van de speler
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; // Stop alle beweging
        animator.SetTrigger("Die");

        // Wacht een korte tijd voordat het level opnieuw wordt geladen, zodat de sterfanimatie kan afspelen
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.2f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(1f); // Wacht 1.5 seconden (pas aan op basis van de lengte van je sterfanimatie)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Laad het huidige level opnieuw
    }
}
