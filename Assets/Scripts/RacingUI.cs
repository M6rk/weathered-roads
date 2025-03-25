using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RacingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float raceTime = 0f;
    private bool isRacing = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRacing)
        {
            raceTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetTimer()
    {
        raceTime = 0f;
        isRacing = true;
        UpdateTimerDisplay();
    }

    public void FinishRace()
    {
        isRacing = false;
        // save race time 
        PlayerPrefs.SetFloat("LastRaceTime", raceTime);
        
        // Load race finish scene
        Invoke("LoadFinishScene", 2f); // delay slightly before loading finish scene, TODO: add race finish animation
    }
    private void LoadFinishScene()
    {
        SceneManager.LoadScene("RaceFinish");
    }
}