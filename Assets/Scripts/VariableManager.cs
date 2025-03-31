using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static VariableManager instance;
    public float selectedCC = 1;
    public float timeModifier = 1;
    public float tracKTimeOffset = 0;

    private void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void SetCCValue(float cc){
        selectedCC = cc;
    }

    public void SetTimeModifier(float modifier){
        timeModifier = modifier;
        Debug.Log(timeModifier);
    }

    public void SetTrackTimeOffset(float offset){
        tracKTimeOffset = offset;
    }
}
