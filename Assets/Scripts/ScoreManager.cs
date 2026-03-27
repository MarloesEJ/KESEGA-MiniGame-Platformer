using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI eggText;
    private int _eggCount;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateEggText();
    }

    public void AddEgg(int amount)
    {
        _eggCount += amount;
        UpdateEggText();
    }

    void UpdateEggText()
    {
        eggText.text = _eggCount.ToString();
    }
}
