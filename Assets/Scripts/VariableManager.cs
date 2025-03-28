using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static VariableManager instance;
    public float selectedCC = 1;

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
}
