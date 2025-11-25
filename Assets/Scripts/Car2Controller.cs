using UnityEngine;
using UnityEngine.InputSystem;

public class Car2Controller : MonoBehaviour
{
    [Header("Physics")]
    public float maxSteerAngle;
    public float rotationForce;
    public float enginePower;

    [Header("Wheel Hinge Joint components")]
    [SerializeField] private HingeJoint _frontLeftWheelHingeJoint;
    [SerializeField] private HingeJoint _frontRightWheelHingeJoint;
    [SerializeField] private HingeJoint _rearLeftWheelHingeJoint;
    [SerializeField] private HingeJoint _rearRightWheelHingeJoint;

    [Header("Wheel Rigidbody components")]
    [SerializeField] private Rigidbody _frontLeftWheelRigidbody;
    [SerializeField] private Rigidbody _frontRightWheelRigidbody;
    [SerializeField] private Rigidbody _rearLeftWheelRigidbody;
    [SerializeField] private Rigidbody _rearRightWheelRigidbody;

    [Header("Input System")]
    [SerializeField] private InputActionAsset _myInputSystem;
    private Vector2 _moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log("OnMove called: " + _moveInput);
    }

    private void FixedUpdate()
    {
        ApplyEnginePower(_moveInput.y);
        ApplySteeringDirection(_moveInput.x);
    }

    private void ApplyEnginePower(float accelerationInput)
    {
        ApplyWheelMotor(accelerationInput, _frontLeftWheelHingeJoint);
        ApplyWheelMotor(accelerationInput, _frontRightWheelHingeJoint);
        ApplyWheelMotor(accelerationInput, _rearLeftWheelHingeJoint);
        ApplyWheelMotor(accelerationInput, _rearRightWheelHingeJoint);
    }

    private void ApplySteeringDirection(float steeringInput)
    {
        Vector3 steeringFrontDirection = new Vector3(0, steeringInput * 100, 0);
        Vector3 steeringRearDirection = new Vector3(0, steeringInput * -100, 0);

        _frontLeftWheelRigidbody.AddTorque(steeringFrontDirection, ForceMode.Force);
        _frontRightWheelRigidbody.AddTorque(steeringFrontDirection, ForceMode.Force);
        _frontLeftWheelRigidbody.AddTorque(steeringRearDirection, ForceMode.Force);
        _frontRightWheelRigidbody.AddTorque(steeringRearDirection, ForceMode.Force);
    }

    private void ApplyWheelMotor(float accelerationInput, HingeJoint hingeJoint)
    {
        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = accelerationInput * rotationForce;
        motor.force = Mathf.Abs(accelerationInput) * enginePower;

        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }
}
