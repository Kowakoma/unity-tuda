using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
      [Header("Car parameters")]
    public float steeringSpeed = 500f;
    public float steeringForce = 250f;
    public float movingSpeed = 500f;
    public float movingForce = 400f;

    [SerializeField] private HingeJoint[] _wheelJoints;
    [SerializeField] private HingeJoint[] _knuckleJoints;

    [Header("Input System")]
    [SerializeField] private InputActionAsset _myInputSystem;
    [SerializeField] private float _steeringDeadZone = 0.01f;
    private float _moveInput;
    private float _steerInput;

    void Awake()
    {
        ValidateReferences();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<float>();
        Debug.Log("OnMove called: " + _moveInput);
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        _steerInput = context.ReadValue<float>();
        Debug.Log("OnSteer called: " + _steerInput);
    }

    void FixedUpdate()
    {
        foreach (var wheels in _wheelJoints)
            ApplyMovingHingeMotor(_moveInput, wheels);

        for (int i = 0; i < _knuckleJoints.Length; i++)
        {
            // Front wheels (first two) steer with input, rear wheels (last two) steer opposite
            float steeringFactor = (i < 2) ? _steerInput : -_steerInput;
            ApplySteeringHingeMotor(steeringFactor, _knuckleJoints[i]);
        }
    }

    private void ApplyMovingHingeMotor(float accelerationInput, HingeJoint hingeJoint)
    {
        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = accelerationInput * movingSpeed;
        motor.force = Mathf.Abs(accelerationInput) * movingForce;

        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }

    private void ApplySteeringHingeMotor(float steeringInput, HingeJoint hingeJoint)
    {
        if (Mathf.Abs(steeringInput) < _steeringDeadZone)
        {
            hingeJoint.useMotor = false;
            return;
        }

        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = steeringInput * steeringSpeed;
        motor.force = Mathf.Abs(steeringInput) * steeringForce;

        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }

    private void ValidateReferences()
    {
        if (_wheelJoints == null || _wheelJoints.Length == 0)
            Debug.LogError("Wheel joints not assigned!");

        if (_knuckleJoints == null || _knuckleJoints.Length == 0)
            Debug.LogError("Knuckle joints not assigned!");
    }
}
