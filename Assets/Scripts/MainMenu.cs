using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject controlsCanvas;

    public void LoadModeSelectScene()
    {
        LoadSceneByName("ModeSelectMenu");
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    void Start()
    {
        // Show main menu by default, hide options
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
    }

    public void ShowOptions()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }
    public void ShowControls()
    {
        mainMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }
}