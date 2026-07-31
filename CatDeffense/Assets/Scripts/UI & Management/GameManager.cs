using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // NECESARIO PARA LA CORRUTINA (IEnumerator)

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References (Dinámicas)")]
    // No usamos SerializeField, se llenan por código desde los otros scripts
    private GameObject gameOverUI;
    private GameObject levelCompleteUI; // Referencia al panel de "Nivel Completado"
    private CanvasGroup fadeGroup;      // Referencia al panel negro para el fade

    [Header("Nivel")]
    public int scoreToNextLevel;

    // Estados del juego
    private bool isGameOver;
    private bool isLevelComplete; // Nuevo: ¿Hemos ganado?
    private bool isTransitioning; // Nuevo: ¿Estamos haciendo el fade?

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        isGameOver = false;
        DontDestroyOnLoad(gameObject);
    }



    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isLevelComplete && !isGameOver && !isTransitioning && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FadeAndLoadNextLevel());
        }

    }

    public void RegisterGameOverUI(GameObject uiPanel)
    {
        gameOverUI = uiPanel;
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void RegisterLevelUI(GameObject completePanel, CanvasGroup fadePanel)
    {
        levelCompleteUI = completePanel;
        fadeGroup = fadePanel;

        if (levelCompleteUI != null) levelCompleteUI.SetActive(false);

        if (fadeGroup != null)
        {
            fadeGroup.alpha = 0f;     
            fadeGroup.blocksRaycasts = false; 
        }

        isLevelComplete = false;
        isTransitioning = false;
    }

    public bool IsGameOver => isGameOver;

    public void EnableGameOverUI()
    {
        if (!isGameOver)
        {
            if (isLevelComplete)
            {
                isLevelComplete = false; 
                if (levelCompleteUI != null)
                    levelCompleteUI.SetActive(false); 
            }

            // Lógica normal de Game Over
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.SetPitch(0.5f);

            isGameOver = true;
            Time.timeScale = 0.5f;

            if (gameOverUI != null)
                gameOverUI.SetActive(true);

        }
    }

    public void RestartLevel()
    {
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.SetPitch(1f);

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();

        Time.timeScale = 1f;
        isGameOver = false;
        isLevelComplete = false;
        isTransitioning = false;

        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        if (ScoreManager.Instance != null)
            Destroy(ScoreManager.Instance.gameObject);

        if (BackgroundMusic.Instance != null) 
            Destroy(BackgroundMusic.Instance.gameObject);

        Destroy(gameObject);

        SceneManager.LoadScene(0);
    }


    // --- LÓGICA DE VICTORIA Y TRANSICIÓN ---

    private void OnScoreChanged(int newScore)
    {
        if (isGameOver)
            return;

        if (newScore >= scoreToNextLevel && !isLevelComplete)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        Debug.Log("¡Puntuación alcanzada! Esperando tecla Q...");
        isLevelComplete = true;

        // Mostramos el cartel de victoria
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(true);

        // NOTA: No paramos el tiempo (Time.timeScale) para que el juego siga "vivo" de fondo
    }

    private IEnumerator FadeAndLoadNextLevel()
    {
        isTransitioning = true;

        // 1. Hacer el Fade In (Pantalla a Negro)
        if (fadeGroup != null)
        {
            fadeGroup.blocksRaycasts = true; // Bloquear inputs durante el fade
            float timer = 0f;
            float duration = 1f; // Duración de 1 segundo

            while (timer < duration)
            {
                timer += Time.deltaTime;
                fadeGroup.alpha = Mathf.Lerp(0f, 1f, timer / duration);
                yield return null;
            }
            fadeGroup.alpha = 1f; // Asegurar que acaba negro total
        }

        // 2. Cargar Siguiente Nivel
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Comprobamos si existe la siguiente escena para no dar error
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¡Juego Terminado! Volviendo al menú...");
            SceneManager.LoadScene(0); // O volver al inicio si no hay más niveles
        }
    }

    // --- GESTIÓN DE ESCENAS ---

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= OnScoreChanged;
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        }

        isGameOver = false;
        isLevelComplete = false;
        isTransitioning = false;
        Time.timeScale = 1f;

        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.SetPitch(1f);

        foreach (var r in FindObjectsOfType<EnemyRespawner>()) r.ResetRespawner();
        foreach (var r in FindObjectsOfType<CoinRespawner>()) r.ResetRespawner();
    }

    public void UpdateScoreGoal(int newGoal)
    {
        scoreToNextLevel = newGoal;
        Debug.Log($"Meta del nivel actualizada a: {scoreToNextLevel} puntos.");
    }
}