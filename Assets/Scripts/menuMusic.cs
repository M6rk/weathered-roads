using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management

public class menuMusic : MonoBehaviour
{

    private void Awake(){
  
           DontDestroyOnLoad(this.gameObject); // This will make sure the object is not destroyed when loading a new scene
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "TrackOne" || currentScene == "TrackTwo" || currentScene == "TrackThree")
        {
            Destroy(gameObject);
        }
    }
}
