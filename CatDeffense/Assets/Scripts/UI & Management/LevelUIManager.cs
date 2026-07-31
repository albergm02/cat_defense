using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [Header("Configuración del Nivel")]
    [SerializeField] private int scoreToWinThisLevel; // Meta específica de este nivel

    [Header("UI Elementos")]
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private CanvasGroup fadeOverlay;

    private void Start()
    {
        if (GameManager.instance != null)
        {
            // 1. Registramos la UI como antes
            GameManager.instance.RegisterLevelUI(levelCompletePanel, fadeOverlay);

            // 2. ACTUALIZAMOS LA META DEL GAMEMANAGER
            // Le decimos: "Para este nivel, la meta es X"
            GameManager.instance.UpdateScoreGoal(scoreToWinThisLevel);
        }
    }
}