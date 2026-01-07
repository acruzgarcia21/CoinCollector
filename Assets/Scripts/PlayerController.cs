using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private Rigidbody _playerRigidBody;

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
        float playerSpeed = 5f;
        
        _playerRigidBody.AddForce(new Vector3(playerInput.x, 0, playerInput.y) * playerSpeed);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump " + context.phase);
        _playerRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
