using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    private List<CheckpointSingle> checkpointSingleList;
    private int nextCheckpointSingleIndex;

    public static int laps;
    private int numOfCheckpoints;
    private int tempLapCounter;
    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("CheckPoints");
        checkpointSingleList = new List<CheckpointSingle>();
        foreach(Transform checkpointSingleTransform in checkpointsTransform){
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);

            checkpointSingleList.Add(checkpointSingle);
        }
        numOfCheckpoints = checkpointSingleList.Count;
        nextCheckpointSingleIndex = 0;
        laps = 1;
        tempLapCounter = laps;
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle){
        if(checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex){
            //correct checkpoint
            Debug.Log("Correct");
            Debug.Log(laps);
            // introduce logic for increasing the lap counter before the check of if its the right checkpoint.
            // Reason for this is that want to update lap on index 0, but have the check be for the max index, so have it update lap counter on the next run through
            if(tempLapCounter != laps){
                tempLapCounter = laps;
                Debug.Log("increase lap ui here");
            }

            if((checkpointSingleList.IndexOf(checkpointSingle) + 1) == numOfCheckpoints){
                Debug.Log("increase");
                IncreaseLaps();
            }
            
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
        } else {
            Debug.Log("wrong");
            // incorrect checkpoing
        }
    }

    public void IncreaseLaps(){
        laps++;
    }

    public int GetLapNumber(){
        return laps;
    }
}
