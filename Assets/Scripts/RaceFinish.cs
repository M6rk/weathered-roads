using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private TMP_Text finalTimeText;

    [Header("Medal Images")]
    [SerializeField] private GameObject goldMedal;
    [SerializeField] private GameObject silverMedal;
    [SerializeField] private GameObject bronzeMedal;

    void Start()
    {
        // hide medals unless player achieves an equiavalent time
        HideAllMedals();

        // Get race time
        float raceTime = PlayerPrefs.GetFloat("LastRaceTime", 0f);
        DisplayFinalTime(raceTime);

        // Display the appropriate medal
        DisplayMedal(raceTime);
    }

    private void DisplayFinalTime(float raceTime)
    {
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int milliseconds = Mathf.FloorToInt((raceTime % 1) * 100);
        finalTimeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    private void HideAllMedals()
    {
        if (goldMedal) goldMedal.SetActive(false);
        if (silverMedal) silverMedal.SetActive(false);
        if (bronzeMedal) bronzeMedal.SetActive(false);
    }
    private void DisplayMedal(float raceTime)
    {
        if (raceTime < 60f) // Less than 1 minute
        {
            if (goldMedal) goldMedal.SetActive(true);
        }
        else if (raceTime < 120f) // Less than 2 minutes
        {
            if (silverMedal) silverMedal.SetActive(true);
        }
        else if (raceTime < 180f) // Less than 3 minutes
        {
            if (bronzeMedal) bronzeMedal.SetActive(true);
        }
        // No medal for times >= 3 minutes
    }
}