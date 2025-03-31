using UnityEngine;
using TMPro;

public class MapTimeHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldTimeText;
    [SerializeField] private TextMeshProUGUI silverTimeText;
    [SerializeField] private TextMeshProUGUI bronzeTimeText;

    private float timeModifier;
    [SerializeField] private float trackTimeOffset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeModifier = VariableManager.instance.timeModifier;
        // trackTimeOffset = VariableManager.instance.tracKTimeOffset;

        SetTimes();
    }

    private void SetTimes(){
        // Set the times for each medal based on the selected weather condition (time modifier) and track time offset
        float goldTime = (180f - trackTimeOffset) * timeModifier;
        float silverTime = (210f - trackTimeOffset) * timeModifier;
        float bronzeTime = (240f - trackTimeOffset) * timeModifier;

        // update the time so its written in MM:SS format
        float goldTimeMinues = Mathf.FloorToInt(goldTime / 60);
        float goldTimeSeconds = Mathf.Floor(goldTime % 60);

        float silverTimeMinues = Mathf.FloorToInt(silverTime / 60);
        float silverTimeSeconds = Mathf.Floor(silverTime % 60);

        float bronzeTimeMinues = Mathf.FloorToInt(bronzeTime / 60);
        float bronzeTimeSeconds = Mathf.Floor(bronzeTime % 60);

        // ensuring two decimal places
        string goldTimeSecondsString = goldTimeSeconds.ToString("00");
        string silverTimeSecondsString = silverTimeSeconds.ToString("00");
        string bronzeTimeSecondsString = bronzeTimeSeconds.ToString("00");

        // Set the text for each medal
        goldTimeText.SetText($"{goldTimeMinues}:{goldTimeSecondsString}");
        silverTimeText.SetText($"{silverTimeMinues}:{silverTimeSecondsString}");
        bronzeTimeText.SetText($"{bronzeTimeMinues}:{bronzeTimeSecondsString}");
    }
}
