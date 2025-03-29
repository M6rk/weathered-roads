using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WeatherPostProcessing : MonoBehaviour
{
    [SerializeField] private PostProcessProfile rainyProfile;
    [SerializeField] private PostProcessProfile sunnyProfile;
    [SerializeField] private PostProcessProfile snowyProfile;

    private PostProcessVolume postProcessVolume;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        if (postProcessVolume == null)
        {
            Debug.LogError("WeatherPostProcessing script needs to be attached to a PostProcessVolume.");
            return;
        }

        string selectedWeather = PlayerPrefs.GetString("SelectedWeatherScene");
        if (!string.IsNullOrEmpty(selectedWeather))
        {
            if (selectedWeather == "RainyScene" && rainyProfile != null)
            {
                postProcessVolume.profile = rainyProfile;
            }
            else if (selectedWeather == "SunnyScene" && sunnyProfile != null)
            {
                postProcessVolume.profile = sunnyProfile;
            }
            else if (selectedWeather == "SnowyScene" && snowyProfile != null)
            {
                postProcessVolume.profile = snowyProfile;
            }
            else
            {
                Debug.LogWarning("No matching Post Processing Profile found for the selected weather: " + selectedWeather);
            }
        }
        else
        {
            Debug.LogWarning("No weather scene selected.");
        }
    }
}