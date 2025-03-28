using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent OnResetPressed = new UnityEvent();
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            OnResetPressed?.Invoke();
        }
    }
}
