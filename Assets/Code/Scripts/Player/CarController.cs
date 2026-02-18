using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
      [Header("Car parameters")]
    public float maxSteerAngle;
    public float steeringForce;
    public float steeringPower;
    public float engineForce;
    public float enginePower;

    [Header("Knuckle joints")]
    [SerializeField] private HingeJoint _frontLeftKnuckleJoint;
    [SerializeField] private HingeJoint _frontRightKnuckleJoint;
    [SerializeField] private HingeJoint _rearLeftKnuckleJoint;
    [SerializeField] private HingeJoint _rearRightKnuckleJoint;

    [Header("Wheel joints")]
    [SerializeField] private HingeJoint _frontLeftWheelJoint;
    [SerializeField] private HingeJoint _frontRightWheelJoint;
    [SerializeField] private HingeJoint _rearLeftWheelJoint;
    [SerializeField] private HingeJoint _rearRightWheelJoint;

    [Header("Input System")]
    [SerializeField] private InputActionAsset _myInputSystem;
    private float _moveInput;
    private float _steerInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<float>();
        Debug.Log("OnMove called: " + _moveInput);
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        _steerInput = context.ReadValue<float>();
        Debug.Log("OnSteer called: " + _moveInput);
    }

    void FixedUpdate()
    {
        ApplyMovingHingeMotor(_moveInput, _frontLeftWheelJoint);
        ApplyMovingHingeMotor(_moveInput, _frontRightWheelJoint);
        ApplyMovingHingeMotor(_moveInput, _rearLeftWheelJoint);
        ApplyMovingHingeMotor(_moveInput, _rearRightWheelJoint);

        ApplySteeringHingeMotor(_steerInput, _frontLeftKnuckleJoint);
        ApplySteeringHingeMotor(_steerInput, _frontRightKnuckleJoint);
        ApplySteeringHingeMotor(-_steerInput, _rearLeftKnuckleJoint);
        ApplySteeringHingeMotor(-_steerInput, _rearRightKnuckleJoint);
    }

    private void ApplyMovingHingeMotor(float accelerationInput, HingeJoint hingeJoint)
    {
        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = accelerationInput * engineForce;
        motor.force = Mathf.Abs(accelerationInput) * enginePower;

        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }

    private void ApplySteeringHingeMotor(float steeringInput, HingeJoint hingeJoint)
    {
        if (Mathf.Abs(steeringInput) < 0.01f)
        {
            hingeJoint.useMotor = false;
            return;
        }

        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = steeringInput * steeringForce;
        motor.force = Mathf.Abs(steeringInput) * steeringPower;

        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }
}
