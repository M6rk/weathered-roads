using UnityEngine;

public class RacingHandler : MonoBehaviour
{

    private int laps;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
        laps = TrackCheckpoints.laps;
        
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