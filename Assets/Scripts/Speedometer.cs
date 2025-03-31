using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private CarController car;
    private float maxSpeed = 180;
    [SerializeField] private float minSpeedArrowAngle;
    [SerializeField] private float maxSpeedArrowAngle;

    [Header("UI")]
    public TextMeshProUGUI speedLabel;
    public RectTransform arrow;

    private float speed = 0.0f;

    // private void Start()
    // {
    //     maxSpeed = car.GetMaxSpeed();
    // }
    private void Update()
    {
        speed = carRB.linearVelocity.magnitude * 2f;

        if (speedLabel != null){
            speedLabel.SetText($"{(int)speed}");
        }

        if(arrow != null){
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
        }
    }
}
