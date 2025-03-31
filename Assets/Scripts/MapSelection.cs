using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyTransition;

public class MapSelection : MonoBehaviour
{
    [Header("Maps")]
    [SerializeField] private Button map1Button;
    [SerializeField] private Button map2Button;
    [SerializeField] private Button map3Button;
    [SerializeField] private Color mapNormalColor = Color.white;
    [SerializeField] private Color mapSelectedColor = Color.gray;

    [Header("Navigation")]
    [SerializeField] private Button nextButton;
//    [SerializeField] private GameObject nextButtonParent; // Parent object containing RectTransition

    [Header("Button Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.gray;

    [Header("Transition")]
    public TransitionSettings transition;
    public float startDelay;

    private Button selectedMapButton;
    private string targetScene;

    // General button colors
    // [SerializeField] private Color normalColor = Color.white;
    // [SerializeField] private Color selectedColor = Color.gray;


    void Start()
    {
        // Hide next button by default
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(LoadSelectedTrack);

        // Assign each map button to the associated map scene
        map1Button.onClick.AddListener(() => SelectMapButton(map1Button, "TrackOne"));
        map2Button.onClick.AddListener(() => SelectMapButton(map2Button, "TrackTwo"));
        map3Button.onClick.AddListener(() => SelectMapButton(map3Button, "TrackThree"));
    }

    // Map button selection (same logic as Mode selection)
    private void SelectMapButton(Button button, string trackScene)
    {
        if (selectedMapButton != null)
        {
            SetButtonNormal(selectedMapButton);
        }
        selectedMapButton = button;
        targetScene = trackScene;
        if(targetScene == "TrackTwo"){
            VariableManager.instance.SetTrackTimeOffset(60);
        }
        SetButtonSelected(button);
        CheckShowNextButton();
    }

    // Reset button style
    private void SetButtonNormal(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = mapNormalColor;
        colors.highlightedColor = mapNormalColor;
        button.colors = colors;
    }

    // Set button style
    private void SetButtonSelected(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = mapSelectedColor;
        colors.highlightedColor = mapSelectedColor;
        button.colors = colors;
    }

    // Next button visibility
    private void CheckShowNextButton()
    {
        if (nextButton != null && selectedMapButton != null)
        {
            nextButton.gameObject.SetActive(true);
        }
    }

    // Navigate to tracks scene
    public void LoadSelectedTrack()
    {
        if (selectedMapButton != null && !string.IsNullOrEmpty(targetScene))
        {
            // Load the selected track scene
            SceneManager.LoadScene(targetScene);

            // Load the selected weather scene additively
            string selectedWeather = PlayerPrefs.GetString("SelectedWeatherScene");
            if (!string.IsNullOrEmpty(selectedWeather))
            {
                SceneManager.LoadScene(selectedWeather, LoadSceneMode.Additive);
            }
        }
    }
}