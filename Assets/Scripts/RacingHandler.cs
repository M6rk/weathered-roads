using UnityEngine;

public class RacingHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
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