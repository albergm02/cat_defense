using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panelContent;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton; 

    private void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterGameOverUI(panelContent);
        }
        else
        {
            Debug.LogError("GameOverUI: No se encontró el GameManager para registrarse.");
        }
    }

    private void Awake()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            {
                GameManager.instance?.RestartLevel();
            });
        }

        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(() =>
            {
                GameManager.instance?.ReturnToMenu();
            });
        }
    }
}