using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CarController car;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager.OnResetPressed.AddListener(HandleReset);
    }

    private void HandleReset() {
        car.ResetCar();
    }
}
