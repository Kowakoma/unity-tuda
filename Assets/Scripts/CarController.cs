using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Engine")]
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _enginePower;
    [SerializeField] private Rigidbody _rbAxleRear;

    [Header("Controllability")]
    [SerializeField] private float _wheelRotationSpeed;

    [Header("Input System")]
    [SerializeField] private InputActionAsset _myInputSystem;
    [SerializeField] private InputAction _moveAction;

    private HingeJoint _hingeJoint;

    private void Awake()
    {
        _moveAction = _myInputSystem.FindAction("Player/Move");
        _hingeJoint = _rbAxleRear.GetComponent<HingeJoint>();
        _moveAction.Enable();
    }

    void Start()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        ApplyHingeJointMotor(input.y);
    }

    private void ApplyHingeJointMotor(float accelerationInput)
    {
        JointMotor motor = _hingeJoint.motor;
        motor.targetVelocity = accelerationInput * _rotationForce;
        motor.force = Mathf.Abs(accelerationInput) * _enginePower;

        _hingeJoint.motor = motor;
        _hingeJoint.useMotor = true;
    }
}
