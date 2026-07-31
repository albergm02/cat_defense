using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; // Asegúrate de que esto esté

public class VictoryManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button menuButton;  
    [SerializeField] private Button playAgainButton;

    private const string HighScoreKey = "HighScore";

    private void Start()
    {

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(GoToMainMenu);
        }

        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgain);
        }

        int finalScore = 0;
        if (ScoreManager.Instance != null)
        {
            finalScore = ScoreManager.Instance.Score;
            scoreText.text = finalScore.ToString();
        }
        else
        {
            scoreText.text = "N/A";
        }

        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (finalScore > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, finalScore);
            PlayerPrefs.Save();
            highScoreText.text = "¡NUEVO RECORD!: " + finalScore.ToString();
        }
        else
        {
            highScoreText.text = "RECORD: " + currentHighScore.ToString();
        }

        if (GameManager.instance != null) Destroy(GameManager.instance.gameObject);
        if (ScoreManager.Instance != null) Destroy(ScoreManager.Instance.gameObject);
        if (BackgroundMusic.Instance != null) BackgroundMusic.Instance.SetPitch(1f);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}