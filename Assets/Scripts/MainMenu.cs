using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject optionsCanvas;
    public void LoadNextScene()
    {
        // Load the scene with the next index from build settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviousScene()
    {
        // Go back by 1 index from current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0)
            SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        // Load a scene by index
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadCCSelectScene()
    {
        LoadSceneByName("CCSelectMenu");
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
    }

    public void ShowOptions()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }
}