using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private Rigidbody _playerRigidBody;

    [Header("Movement Fields")] [SerializeField]
    private float maxSpeed = 8f;
    
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float braking = 20f;
    [SerializeField] private float jumpingForce = 5f;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerRigidBody = GetComponent<Rigidbody>();
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
        Vector2 playerInput = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 directionInput = new Vector3(playerInput.x, 0, playerInput.y);
        
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
        Debug.Log("Jump " + context.phase);
        _playerRigidBody.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
    }
}
