using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RacingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject racingUIContainer; 
    [SerializeField] private GameObject countdownCanvas;   
    [SerializeField] private TMP_Text countdownText;      
    [SerializeField] private MonoBehaviour carController; 
    private float raceTime = 0f;
    private bool isRacing = false;
    private float countdownTime = 3f; 
    private bool isCountingDown = false;

    void Start()
    {
        //disables user controls on entry 
        if (carController != null)
        {
            carController.enabled = false; 
        }
        StartCountdown();
    }

    private void StartCountdown()
    {
        isRacing = false;
        isCountingDown = true;
        racingUIContainer.SetActive(false);
        countdownCanvas.SetActive(true);
        Invoke("BeginCountdownSequence", 2f); 
    }

    private void BeginCountdownSequence()
    {
        countdownTime = 3f;
        InvokeRepeating("UpdateCountdown", 0f, 1f);
    }

    private void UpdateCountdown()
    {
        if (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString("0");
            //sets color based off the time of countdown
            switch (countdownTime)
            {
                case 3:
                    countdownText.color = Color.red;
                    break;
                case 2:
                    countdownText.color = Color.yellow;
                    break;
                case 1:
                    countdownText.color = Color.green;
                    break;
            }
            countdownTime--;
        }
        else if (isCountingDown)
        {
            countdownText.text = "GO!";
            countdownText.color = Color.green;
            isCountingDown = false;
            CancelInvoke("UpdateCountdown");
            StartRace();
            Invoke("HideCountdown", 1f);
        }
    }

    private void StartRace()
    {
        if (carController != null)
        {
            carController.enabled = true; 
        }
        racingUIContainer.SetActive(true);
        ResetTimer();
    }

    private void HideCountdown()
    {
        countdownCanvas.SetActive(false);
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