using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [Header("Map 1 Times")]
    [SerializeField] private TMP_Text[] map1TimeTexts = new TMP_Text[5];

    [Header("Map 2 Times")]
    [SerializeField] private TMP_Text[] map2TimeTexts = new TMP_Text[5];

    [Header("Map 3 Times")]
    [SerializeField] private TMP_Text[] map3TimeTexts = new TMP_Text[5];

    [Header("Map Names")]
    [SerializeField] private string map1Name = "Map1";
    [SerializeField] private string map2Name = "Map2";
    [SerializeField] private string map3Name = "Map3";


    void Start()
    {
        // check for new race time 
        float lastRaceTime = PlayerPrefs.GetFloat("LastRaceTime", 0f);
        string lastRaceMap = PlayerPrefs.GetString("LastRaceMap", "");

        if (lastRaceTime > 0f && !string.IsNullOrEmpty(lastRaceMap))
        {
            // insert time into associated map column in scene
            AddTimeToLeaderboard(lastRaceMap, lastRaceTime);

            // clear PlayerRef to avoid duplicate times
            PlayerPrefs.DeleteKey("LastRaceTime");
            PlayerPrefs.DeleteKey("LastRaceMap");
            PlayerPrefs.Save();
        }
        // load leaderboard textmeshpro times
        DisplayMapTimes(map1Name, map1TimeTexts);
        DisplayMapTimes(map2Name, map2TimeTexts);
        DisplayMapTimes(map3Name, map3TimeTexts);
    }
    private void DisplayMapTimes(string mapName, TMP_Text[] timeTexts)
    {
        List<float> times = GetSavedTimes(mapName);

        // Debug - print the loaded times
        Debug.Log($"Loaded times for {mapName}: {string.Join(", ", times)}");

        // Sort times (best/lowest first)
        times.Sort();

        // Display up to 5 best times
        for (int i = 0; i < timeTexts.Length; i++)
        {
            if (i < times.Count)
            {
                string formattedTime = FormatTime(times[i]);
                timeTexts[i].text = formattedTime;
            }
            else
            {
                // No time for this position
                timeTexts[i].text = "--:--";
            }
        }
    }
    private List<float> GetSavedTimes(string mapName)
    {
        List<float> times = new List<float>();

        string timesString = PlayerPrefs.GetString(mapName + "_Times", "");
        if (!string.IsNullOrEmpty(timesString))
        {
            string[] timeStrings = timesString.Split(',');
            foreach (string timeStr in timeStrings)
            {
                if (float.TryParse(timeStr, out float time))
                {
                    times.Add(time);
                }
            }
        }

        return times;
    }

    private void AddTimeToLeaderboard(string mapName, float raceTime)
    {
        List<float> times = GetSavedTimes(mapName);
        if (times.Count >= 5)
        {
            // sort times from low to high
            times.Sort();
            // if the new time is worse than other times dont add
            if (raceTime > times[times.Count - 1])
            {
                return;
            }
        }
        times.Add(raceTime);
        times.Sort();
        // limit list size to 5
        if (times.Count > 5)
        {
            times = times.Take(5).ToList();
        }
        // save back to PlayerPrefs
        string newTimesString = string.Join(",", times);
        PlayerPrefs.SetString(mapName + "_Times", newTimesString);
    }

    private string FormatTime(float raceTime)
    {
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        int milliseconds = Mathf.FloorToInt((raceTime % 1) * 100);
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}