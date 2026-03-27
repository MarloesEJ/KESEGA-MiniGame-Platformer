using UnityEngine;

public class EggPickup : MonoBehaviour
{
    public int scoreValue = 1; // Hoeveel punten deze eieren waard zijn

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddEgg(scoreValue); // Voeg punten toe aan de score
            Destroy(gameObject); // Vernietig het ei na het oppakken
        }
    }
}
