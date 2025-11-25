using UnityEngine;
using UnityEngine.InputSystem;

public class Car4Controller : MonoBehaviour
{
    [Header("Physics")]
    public float maxSteerAngle;
    public float rotationForce;
    public float enginePower;

    [Header("Wheel Hinge Joint components")]
    [SerializeField] private ConfigurableJoint _frontLeftWheelHingeJoint;
    [SerializeField] private ConfigurableJoint _frontRightWheelHingeJoint;
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
        ApplyHingeMotor(_moveInput.y, _rearLeftWheelHingeJoint);
        ApplyHingeMotor(_moveInput.y, _rearRightWheelHingeJoint);
    }

    private void ApplyHingeMotor(float accelerationInput, HingeJoint hingeJoint)
    {
        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = accelerationInput * rotationForce;
        motor.force = Mathf.Abs(accelerationInput) * enginePower;

        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }

    private void ApplyConfMotor()
    {
        
    }
}
