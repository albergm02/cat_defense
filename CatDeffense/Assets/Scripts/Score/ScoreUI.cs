using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = ScoreManager.Instance.Score.ToString();
            ScoreManager.Instance.OnScoreChanged += UpdateScore;
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}
