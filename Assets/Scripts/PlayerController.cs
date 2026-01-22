using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private Rigidbody _playerRigidBody;
    private SphereCollider _sphereCollider;

    [Header("Movement Fields")] 
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float braking = 20f;
    [SerializeField] private float jumpingForce = 5f;

    [Header("Camera")] 
    [SerializeField] private Transform cameraTransform;

    [Header("Ground Check")] 
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundSkin = 0.05f;
    [SerializeField] private float jumpBuffer = 0.12f;
    [SerializeField] private float coyoteTime = 0.12f;
    
    private bool _jumpRequested;
    private bool _isGrounded;
    private float _jumpBufferTimer;
    private float _coyoteTimer;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerRigidBody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= Jump;
        _playerInputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        UpdateGrounded();
        
        // Jump buffer + coyote time
        _jumpBufferTimer -= Time.fixedDeltaTime;
        _coyoteTimer = _isGrounded ? coyoteTime : _coyoteTimer - Time.fixedDeltaTime;

        if ((_jumpRequested || _jumpBufferTimer > 0f) && _coyoteTimer > 0f)
        {
            _jumpRequested = false;
            _jumpBufferTimer = 0f;
            _coyoteTimer = 0f;
            
            // Make jump consistent 
            Vector3 linearVelocity = _playerRigidBody.linearVelocity;
            if (linearVelocity.y < 0f)
            {
                linearVelocity.y = 0f;
            }

            _playerRigidBody.linearVelocity = linearVelocity;
        
            _playerRigidBody.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
        }
        else
        {
            // Don't let a single input event "stick" forever
            _jumpRequested = false;
        }
        
        Vector2 playerInput = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        // Camera direction
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();
        
        // Convert WASD into camera-relative world direction
        var directionInput = camRight * playerInput.x + camForward * playerInput.y;
        
        // This is a slight optimization that allows for controllers to not normalize when 
        // analog stick is barley pushed.
        if (directionInput.sqrMagnitude > 1f) directionInput.Normalize();

        Vector3 velocity = _playerRigidBody.linearVelocity;
        Vector3 horizVelocity = new Vector3(velocity.x, 0f, velocity.z);

        if (playerInput.sqrMagnitude > 0f)
        {
            // Move toward desired speed
            Vector3 desiredVelocity = directionInput * maxSpeed;
            Vector3 neededAcceleration = desiredVelocity - horizVelocity;

            Vector3 accelStep = 
                Vector3.ClampMagnitude(neededAcceleration, acceleration * Time.fixedDeltaTime);
            
            _playerRigidBody.AddForce(accelStep / Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            // Brake when player stops moving
            _playerRigidBody.AddForce(-horizVelocity * braking, ForceMode.Acceleration);
        }
        
        // Hard speed clamp (Backup)
        Vector3 newVelocity = _playerRigidBody.linearVelocity;
        Vector3 newHorizVel = new Vector3(newVelocity.x, 0f, newVelocity.z);

        if (newHorizVel.magnitude > maxSpeed)
        {
            Vector3 clamped = newHorizVel.normalized * maxSpeed;
            _playerRigidBody.linearVelocity = new Vector3(clamped.x, newVelocity.y, clamped.z);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _jumpRequested = true;
        _jumpBufferTimer = jumpBuffer;
    }
    
    private void UpdateGrounded()
    {
        // World-space center of the sphere collider
        Vector3 centerWorld = transform.TransformPoint(_sphereCollider.center);
        
        // Approx radius in world units (assumes mostly uniform scale)
        float radiusWorld = _sphereCollider.radius * transform.lossyScale.x;
        
        // Point near the bottom of the sphere
        Vector3 feet = centerWorld + Vector3.down * (radiusWorld - groundSkin);
        
        // Check a small sphere at the "feet" for stable grounded detection
        _isGrounded = Physics.CheckSphere(
            feet,
            groundSkin * 2f,
            groundMask,
            QueryTriggerInteraction.Ignore
            );
    }
}
