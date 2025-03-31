using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject icyRoadParent;
    [SerializeField] private GameObject rainyRoadParent;

    void Start()
    {
        SetEnvironment();
    }
    public void SetEnvironment(){
        if(VariableManager.instance.weather == "Sunny"){
            icyRoadParent.SetActive(false);
            rainyRoadParent.SetActive(false);
        }   
        if(VariableManager.instance.weather == "Snowy"){
            icyRoadParent.SetActive(true);
            rainyRoadParent.SetActive(false);
        }
        if(VariableManager.instance.weather == "Rainy"){
            icyRoadParent.SetActive(false);
            rainyRoadParent.SetActive(true);
        }
    }
}
