using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
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
        if(context.phase == InputActionPhase.Performed)
        {
            currentMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            currentMovementInput = Vector2.zero;
        }
    }

}
