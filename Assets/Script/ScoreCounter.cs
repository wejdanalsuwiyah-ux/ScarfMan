using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static int Score = 0;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = Score.ToString();
    }

    public static void AddScore(int amount)
    {
        Score += amount;
    }
}
