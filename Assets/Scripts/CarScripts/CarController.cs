using System;
using UnityEngine;
using UnityEngine.Events;

public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private LayerMask drivable;
    [SerializeField] private Transform accelerationPoint;
    [SerializeField] private GameObject[] tires = new GameObject[4];
    [SerializeField] private GameObject[] frontTireParents = new GameObject[2];
    [SerializeField] private TrailRenderer[] skidMarks = new TrailRenderer[2];
    [SerializeField] private ParticleSystem[] skidSmokes = new ParticleSystem[2];
    [SerializeField] private AudioSource engineSound, skidSound;
    [SerializeField] private GameObject car;
   
    [Header("Suspension Settings")]
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;
    [SerializeField] private float restLength;
    [SerializeField] private float springTravel;
    [SerializeField] private float wheelRadius;

    private int[] wheelIsGrounded = new int[4];
    private bool isGrounded = false;

    [Header("Suspension Settings")]
    private float moveInput = 0;
    private float steerInput = 0;

    [Header("Car Settings")]
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float steerStrength = 15f;
    [SerializeField] private AnimationCurve turningCurve;
    [SerializeField] private float dragCoefficient = 1f;
    [SerializeField] private float breakingDeceleration = 100f;
    [SerializeField] private float breakingDragCoefficient = 0.5f;

    private Vector3 currentCarLocalVelocity = Vector3.zero;
    private float carVelocityRatio = 0;

    [Header("Visuals")]
    [SerializeField] private float tireRotSpeed = 3000f;
    [SerializeField] private float maxSteeringAngle = 30f;
    [SerializeField] private float minSideSkidVelocity = 10f;

    [Header("Audio")]
    [SerializeField]
    [Range(0, 1)] private float minPitch = 1f;
    [SerializeField]
    [Range(1, 5)] private float maxPitch = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carRB = GetComponent<Rigidbody>();

        // sets different speeds of the car depending on the CC selected.
        float selectedCC = VariableManager.instance.selectedCC;
        maxSpeed = maxSpeed * selectedCC;
        acceleration = acceleration * selectedCC;
        Debug.Log("Max speed: " + maxSpeed);
        Debug.Log("Acceleration: " + acceleration);
    }

    private void FixedUpdate()
    {
        Suspension();
        GroundCheck();
        CalculateCarVelocity();
        Movement();
        Visuals();
        EngineSound();
    }

    void Update()
    {
        GetPlayerInput();
    }

    public void ResetCar(){
        carRB.isKinematic = true;
        car.transform.position = TrackCheckpoints.respawnPoint;
        carRB.linearVelocity = Vector3.zero;
        carRB.angularVelocity = Vector3.zero;
        // carRB.MoveRotation(TrackCheckpoints.checkpointRotation);
        car.transform.rotation = TrackCheckpoints.checkpointRotation;
        carRB.isKinematic = false;
    }

    #region Movement

    private void Movement(){
        if(isGrounded){
            Acceleration();
            Deceleration();
            Turn();
            SidewaysDrag();
        }
    }
    private void Acceleration(){
        if(currentCarLocalVelocity.z < maxSpeed){
            carRB.AddForceAtPosition(acceleration * moveInput * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
        }
    }

    private void Deceleration(){
        carRB.AddForceAtPosition((Input.GetKey(KeyCode.Space) ? breakingDeceleration : deceleration) * carVelocityRatio * -transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    private void Turn(){
        carRB.AddTorque(steerStrength * steerInput * turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * Mathf.Sign(carVelocityRatio) * transform.up, ForceMode.Acceleration);
    }

    private void SidewaysDrag(){
        float currentSidewaysSpeed = currentCarLocalVelocity.x;

        float dragMagnitude = -currentSidewaysSpeed * (Input.GetKey(KeyCode.Space) ? breakingDragCoefficient : dragCoefficient);

        Vector3 dragForce = transform.right * dragMagnitude;

        carRB.AddForceAtPosition(dragForce, carRB.worldCenterOfMass, ForceMode.Acceleration);
    }
    #endregion

    #region Car status check

    private void GroundCheck(){
        int tempGroundedWheels = 0;
        for(int i = 0; i < wheelIsGrounded.Length; i++){
            tempGroundedWheels += wheelIsGrounded[i];
        }

        if(tempGroundedWheels > 1){
            isGrounded = true;
            // Debug.Log("Is grounded: " + isGrounded);
        } else {
            isGrounded = false;
            // Debug.Log("Is grounded: " + isGrounded);
        }
    }

    private void CalculateCarVelocity(){
        currentCarLocalVelocity = transform.InverseTransformDirection(carRB.linearVelocity);
        carVelocityRatio = currentCarLocalVelocity.z / maxSpeed;

    }
    #endregion

    #region Visuals
    private void Visuals(){
        TireVisuals();
        VFX();
    }

    private void SetTirePosition(GameObject tire, Vector3 targetPosition){
        tire.transform.position = targetPosition;
    }

    private void TireVisuals(){
        float steeringAngle = maxSteeringAngle * steerInput;
        for(int i = 0; i < tires.Length; i++){
            if (i < 2){
                tires[i].transform.Rotate(Vector3.right, tireRotSpeed * carVelocityRatio * Time.deltaTime, Space.Self);

                frontTireParents[i].transform.localEulerAngles = new Vector3(frontTireParents[i].transform.localEulerAngles.x, steeringAngle, frontTireParents[i].transform.localEulerAngles.z);
            } else {
                tires[i].transform.Rotate(Vector3.right, tireRotSpeed * moveInput * Time.deltaTime, Space.Self);
            }
        }
    }

    private void VFX(){
        if (isGrounded && Math.Abs(currentCarLocalVelocity.x) > minSideSkidVelocity && carVelocityRatio > 0){
            ToggleSkidMarks(true);
            ToggleSkidSmokes(true);
            ToggleSkidSound(true);
        } else {
            ToggleSkidMarks(false);
            ToggleSkidSmokes(false);
            ToggleSkidSound(false);
        }
    }

    private void ToggleSkidMarks(bool toggle){
        foreach(var skidMark in skidMarks){
            skidMark.emitting = toggle;
        }
    }

    private void ToggleSkidSmokes(bool toggle){
        foreach(var smoke in skidSmokes){
            if(toggle){
                smoke.Play();
            } else {
                smoke.Stop();
            }
        }
    }
        
    #endregion

    #region Audio
        
    private void EngineSound(){
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(carVelocityRatio));
    }

    private void ToggleSkidSound(bool toggle){
        skidSound.mute = !toggle;
    }

    #endregion

    #region Input Handling
        private void GetPlayerInput(){
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    #endregion

    #region Suspension Functions
    private void Suspension(){
        for(int i = 0; i < rayPoints.Length;i++){
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if(Physics.Raycast(rayPoints[i].position, -rayPoints[i].up, out hit, maxLength + wheelRadius, drivable)){

                wheelIsGrounded[i] = 1;
                float currentSpringLength = hit.distance - wheelRadius;
                float springCompression = restLength - currentSpringLength / springTravel;

                float springVelocity = Vector3.Dot(carRB.GetPointVelocity(rayPoints[i].position), rayPoints[i].up);
                float dampForce = damperStiffness * springVelocity;
                
                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                carRB.AddForceAtPosition(netForce * rayPoints[i].up, rayPoints[i].position);

                SetTirePosition(tires[i], hit.point + rayPoints[i].up * wheelRadius);

                Debug.DrawLine(rayPoints[i].position, hit.point, Color.red);
            }
            else {

                wheelIsGrounded[i] = 0;

                SetTirePosition(tires[i], rayPoints[i].position - rayPoints[i].up * maxLength);

                Debug.DrawLine(rayPoints[i].position, rayPoints[i].position + (wheelRadius + maxLength) * -rayPoints[i].up, Color.green);
            }
        }

    }
    #endregion

// below getters and setters have been rendered relatively useless, but still keeping them in case we need them
    #region Getters and Setters
    // getters for acceleration and speed as they are the only ones we need to get and set
    public float GetMaxSpeed(){
        return maxSpeed;
    }

    public void SetMaxSpeed(float speedModifier){
        maxSpeed = maxSpeed * speedModifier;
    }
    public float GetAcceleration(){
        return acceleration;
    }

    public void SetAcceleration(float accelerationModifier){
        acceleration = acceleration * accelerationModifier;
    }
    #endregion
}
