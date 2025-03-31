using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Canvas racingUICanvas;
    
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    
    private bool isPaused = false;
    
    void Start()
    {
        // Hide pause menu initially
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.gameObject.SetActive(false);
        
        // Set up button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitToMainMenu);
    }
    void Update()
    {
        // Toggle pause with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    private void PauseGame()
    {
        // Freeze time
        Time.timeScale = 0f;
        
        // Show pause menu, hide racing UI
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.gameObject.SetActive(true);
        
        if (racingUICanvas != null)
            racingUICanvas.gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        // Resume time
        Time.timeScale = 1f;
        isPaused = false;
        // Hide pause menu, show racing UI
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.gameObject.SetActive(false);
        
        if (racingUICanvas != null)
            racingUICanvas.gameObject.SetActive(true);
    }
    
    public void QuitToMainMenu()
    {
        // Reset time scale before loading main menu
        Time.timeScale = 1f;
        isPaused = false;
        // Load main menu scene
        SceneManager.LoadScene("StartMenu");
    }
}