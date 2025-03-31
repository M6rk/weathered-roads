using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private TMP_Text finalTimeText;

    [Header("Medal Images")]
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject bronzeMedal;
    [SerializeField] private float timeModifier;
    [SerializeField] private float trackTimeOffset = 0;

    void Start()
    {
        // hide medals unless player achieves an equiavalent time
        HideAllMedals();

        // Get race time
        float raceTime = PlayerPrefs.GetFloat("LastRaceTime", 0f);
        DisplayFinalTime(raceTime);

        // Display the appropriate medal
        DisplayMedal(raceTime);
        timeModifier = VariableManager.instance.timeModifier;
        trackTimeOffset = VariableManager.instance.tracKTimeOffset;
    }
    private void DisplayFinalTime(float raceTime)
    {
        if (raceTime == 0f)
        {
            finalTimeText.text = "--:--";
            return;
        }

        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int milliseconds = Mathf.FloorToInt((raceTime % 1) * 100);
        finalTimeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
    public void RestartRace()
    {
        string lastRaceMap = PlayerPrefs.GetString("LastRaceMap", "Map1");

        switch (lastRaceMap)
        {
            case "Map1":
                SceneManager.LoadScene("TrackOne");
                break;
            case "Map2":
                SceneManager.LoadScene("TrackTwo");
                break;
            case "Map3":
                SceneManager.LoadScene("TrackThree");
                break;
            default:
                // Fallback to first track 
                SceneManager.LoadScene("TrackOne");
                break;
        }
    }
    private void HideAllMedals()
    {
        if (goldMedal) goldMedal.SetActive(false);
        if (silverMedal) silverMedal.SetActive(false);
        if (bronzeMedal) bronzeMedal.SetActive(false);
    }
    private void DisplayMedal(float raceTime)
    {
        if (raceTime < (180f * timeModifier) - trackTimeOffset) // Less than 3  minutes
        {
            if (goldMedal) goldMedal.SetActive(true);
        }
        else if (raceTime < (210f * timeModifier) - trackTimeOffset) // Less than 3.5 minutes
        {
            if (silverMedal) silverMedal.SetActive(true);
        }
        else if (raceTime < (240f * timeModifier) - trackTimeOffset) // Less than 4 minutes
        {
            if (bronzeMedal) bronzeMedal.SetActive(true);
        }
        // No medal for times >= 4 minutes
    }
}