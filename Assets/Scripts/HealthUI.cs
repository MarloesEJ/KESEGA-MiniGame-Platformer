using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts; // Array van hartafbeeldingen
    public Sprite fullHeart; // Vol hart sprite
    public Sprite emptyHeart; // Leeg hart sprite

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart; // Toon vol hart
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Toon leeg hart
            }
        }
    }
}
