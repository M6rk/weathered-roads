using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
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
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadStartMenu() 
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}