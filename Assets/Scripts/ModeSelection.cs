using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    [Header("CC Selection")]
    [SerializeField] private Button cc50Button;
    [SerializeField] private Button cc100Button;
    [SerializeField] private Button cc150Button;

    [Header("Weather Selection")]
    [SerializeField] private Button rainyButton;
    [SerializeField] private Button sunnyButton;
    [SerializeField] private Button snowyButton;

    [Header("Navigation")]
    [SerializeField] private Button nextButton;

    private Button selectedCCButton;
    private Button selectedWeatherButton;

    private int selectedCC;
    private string selectedWeatherSceneName; // Changed to store scene name

    // Color button states
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.gray;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(LoadMapSelection);
        // button listeners for CC selection
        cc50Button.onClick.AddListener(() => SelectCCButton(cc50Button, 50));
        cc100Button.onClick.AddListener(() => SelectCCButton(cc100Button, 100));
        cc150Button.onClick.AddListener(() => SelectCCButton(cc150Button, 150));

        // button listeners for weather selection
        rainyButton.onClick.AddListener(() => SelectWeatherButton(rainyButton, "RainyScene")); // Assuming your rainy scene is named "RainyScene"
        sunnyButton.onClick.AddListener(() => SelectWeatherButton(sunnyButton, "SunnyScene")); // Assuming your sunny scene is named "SunnyScene"
        snowyButton.onClick.AddListener(() => SelectWeatherButton(snowyButton, "SnowyScene")); // Assuming your snowy scene is named "SnowyScene"
    }

    // handle CC button selection
    private void SelectCCButton(Button button, int ccValue)
    {
        if (selectedCCButton != null)
        {
            SetButtonNormal(selectedCCButton);
        }
        selectedCCButton = button;
        selectedCC = ccValue;
        SetButtonSelected(button);
        Debug.Log($"Selected CC: {selectedCC}");
        CheckShowNextButton();
    }

    // handle weather button selection
    private void SelectWeatherButton(Button button, string weatherSceneName)
    {
        if (selectedWeatherButton != null)
        {
            SetButtonNormal(selectedWeatherButton);
        }
        selectedWeatherButton = button;
        selectedWeatherSceneName = weatherSceneName;
        SetButtonSelected(button);
        Debug.Log($"Selected Weather Scene: {selectedWeatherSceneName}");
        CheckShowNextButton();
    }
    // reset button method
    private void SetButtonNormal(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = normalColor;
        button.colors = colors;
    }
    // update button method via colors
    private void SetButtonSelected(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = selectedColor;
        button.colors = colors;
    }
    // determine if 'Next' button is visible
    private void CheckShowNextButton()
    {
        if (nextButton != null && selectedCCButton != null && selectedWeatherButton != null)
        {
            nextButton.gameObject.SetActive(true);
        }
    }
    // navigation for next button
    private void LoadMapSelection()
    {
        if (selectedCCButton != null && selectedWeatherButton != null)
        {
            // Store the selected weather scene name for the next scene to access
            PlayerPrefs.SetString("SelectedWeatherScene", selectedWeatherSceneName);
            SceneManager.LoadScene("MapSelectMenu");
        }
        else
        {
            Debug.LogWarning("Both CC and Weather must be selected.");
        }
    }
    // get the current CC selection
    public int GetSelectedCC()
    {
        return selectedCC;
    }

    // get the current weather selection (returns scene name now)
    public string GetSelectedWeatherScene()
    {
        return selectedWeatherSceneName;
    }
}