using UnityEngine;

public class SurfaceWheelDetector : MonoBehaviour
{
    [Header("Joints")]
    public HingeJoint wheelJoint;
    public HingeJoint knuckleJoint;

    [Header("Raycast - Surface detector settings")]
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask surfaceLayer;

    [Header("Current State")]
    [SerializeField] private SurfaceType _currentSurface;
    [SerializeField] private bool isGrounded = false;

    // Event for subscription for other scripts
    public System.Action<SurfaceType> OnSurfaceChanged;

    private void CheckSurfaceUnderWheel()
    {
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = -transform.up;

        bool wasGrounded = isGrounded;
        SurfaceType previousSurface = _currentSurface;

        RaycastHit hit;
        isGrounded = Physics.Raycast(
            rayStart,
            rayDirection,
            out hit,
            _raycastDistance,
            surfaceLayer
        );

        if (isGrounded)
        {
            Surface surfaceComponent = hit.collider.GetComponent<Surface>();
            _currentSurface = surfaceComponent?.surfaceType;
        }
        else
        {
            _currentSurface = null;
        }

        if (isGrounded != wasGrounded || _currentSurface != previousSurface)
        {
            OnSurfaceChanged?.Invoke(_currentSurface);
        }
    }

    public SurfaceType GetCurrentSurface() => _currentSurface;
    public bool IsGrounded() => isGrounded;

}
