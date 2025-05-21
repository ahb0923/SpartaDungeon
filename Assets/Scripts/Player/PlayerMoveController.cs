using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;


public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody rigidBody;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;

    private Vector2 currentMovementInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXlook;
    public float maxXlook;
    public float lookSensitivity;
    private float cameraCurrentXRotation;
    private Vector2 mouseDelta;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }
    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
    }

    private void Move()
    {
        Vector3 dir = (transform.forward * currentMovementInput.y) + (transform.right * currentMovementInput.x);
        dir *= moveSpeed;
        dir.y = rigidBody.velocity.y;

        rigidBody.velocity = dir;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentMovementInput = Vector2.zero;
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }

    }
    private bool IsGrounded()
    {
        return Physics.SphereCast(transform.position - Vector3.up * 0.5f, 0.41f, Vector3.down, out _, 0.2f, 1 << LayerMask.NameToLayer("Ground"));
    }
}
