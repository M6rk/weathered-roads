using UnityEngine;
using TMPro;

public class RacingHandler : MonoBehaviour
{

    private int laps;
    [SerializeField] private TextMeshProUGUI lapCounter;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
        laps = TrackCheckpoints.laps;
        lapCounter.SetText($"{laps}/3");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Started Lap " + laps);
            if(laps > 3){
                Debug.Log("Player detected - finishing race");
                // Find the RacingUI and call FinishRace
                // TODO: Store time for leaderboard
                RacingUI racingUI = FindAnyObjectByType<RacingUI>();
                if (racingUI != null)
                {
                    racingUI.FinishRace();
                }
            }
        }
    }
}