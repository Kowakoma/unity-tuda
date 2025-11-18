using UnityEngine;
using UnityEngine.InputSystem;

public class Car3Controller : MonoBehaviour
{
    [Header("Physics")]
    public float motorTorque;
    public float maxSteerAngle;

    [Header("Input System")]
    [SerializeField] private InputActionAsset _myInputSystem;
    [SerializeField] private InputAction _moveAction;
    private Vector2 _moveInput;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider;
    [SerializeField] private WheelCollider _rearRightWheelCollider;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log("OnMove called: " + _moveInput);
    }

    private void FixedUpdate()
    {
        HandleEnigine();
        HandleSteering();
    }

    private void HandleEnigine()
    {
        float torque = _moveInput.y * motorTorque;
        _rearLeftWheelCollider.motorTorque = torque;
        _rearRightWheelCollider.motorTorque = torque;
    }
    
    private void HandleSteering()
    {
        float steerAngle = _moveInput.x * maxSteerAngle;
        _frontLeftWheelCollider.steerAngle = steerAngle;
        _frontRightWheelCollider.steerAngle = steerAngle;
    }
}
