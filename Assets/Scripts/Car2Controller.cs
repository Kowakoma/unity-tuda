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
    }

    private void ApplyEnginePower(float accelerationInput)
    {
        ApplyWheelMotor(accelerationInput, _frontLeftWheelHingeJoint);
        ApplyWheelMotor(accelerationInput, _frontRightWheelHingeJoint);
        ApplyWheelMotor(accelerationInput, _rearLeftWheelHingeJoint);
        ApplyWheelMotor(accelerationInput, _rearRightWheelHingeJoint);
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
