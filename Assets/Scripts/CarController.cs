using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private LayerMask drivable;
   
    [Header("Suspension Settings")]
    [SerializeField] private float springStiffness;
    [SerializeField] private float restLength;
    [SerializeField] private float springTravel;
    [SerializeField] private float wheelRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Suspension();
    }
    private void Suspension(){
        foreach(Transform rayPoint in rayPoints){
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if(Physics.Raycast(rayPoint.position, -rayPoint.up, out hit, maxLength + wheelRadius, drivable)){
                float currentSpringLength = hit.distance - wheelRadius;
                float springCompression = restLength - currentSpringLength / springTravel;
                
                float springForce = springStiffness * springCompression;

                carRB.AddForceAtPosition(springForce * rayPoint.up, rayPoint.position);

                Debug.DrawLine(rayPoint.position, hit.point, Color.red);
            }
            else {
                Debug.DrawLine(rayPoint.position, rayPoint.position + (wheelRadius + maxLength) * -rayPoint.up, Color.green);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
