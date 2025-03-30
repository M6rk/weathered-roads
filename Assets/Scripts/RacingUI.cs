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
    [SerializeField] private AudioSource[] carAudioSources;
    [SerializeField] public AudioSource countdownBeepSource;
    [SerializeField] public AudioSource backgroundMusicSource;
    [SerializeField] public AudioClip countdownBeep;
    [SerializeField] public AudioClip goBeep;
    private float raceTime = 0f;
    private bool isRacing = false;
    private float countdownTime = 3f; 
    private bool isCountingDown = false;

    void Start()
    {
        if (carController != null)
        {
            carController.enabled = false; 
        }
        if (carAudioSources != null)
        {
            foreach(AudioSource source in carAudioSources)
            {
                if(source != null)
                {
                    source.enabled = false;
                }
            }
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
            countdownBeepSource.PlayOneShot(countdownBeep, 2.5f);
            switch (countdownTime)
            {
                case 3:
                    countdownText.color = new Color(255f / 255f, 36f / 255f, 36f / 255f);
                    break;
                case 2:
                    countdownText.color = new Color(255f / 255f, 229f / 255f, 36f / 255f);
                    break;
                case 1:
                    countdownText.color = new Color(65f / 255f, 255f / 255f, 36f / 255f);
                    break;
            }
            countdownTime--;
        }
        else if (isCountingDown)
        {
            countdownText.text = "GO!";
            countdownBeepSource.PlayOneShot(goBeep, 2.5f);
            countdownText.color = new Color(65f / 255f, 255f / 255f, 36f / 255f);
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
        if (carAudioSources != null)
        {
            foreach(AudioSource source in carAudioSources)
            {
                if(source != null)
                {
                    source.enabled = true;
                }
            }
        }
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.time = 0f; // Reset to beginning
            backgroundMusicSource.Play();
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