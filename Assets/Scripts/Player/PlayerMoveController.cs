using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMoveController : MonoBehaviour
{
    private Player player;
    public CameraSwitch cameraSwtich;
    private Rigidbody rigidBody;

    [Header("Movement")]
    public float moveSpeed;
    public float baseSpeed;
    public float runRate;
    public float jumpPower;
    public bool isRunning;
    public bool canRun;

    public Vector2 currentMovementInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;
    public Image aim;
    private float cameraCurrentXRotation;
    private Vector2 mouseDelta;

    [Header("Jump")]
    [SerializeField] 
    private LayerMask jumpAbleMask;

    private Coroutine jumpBuffCoroutine;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        isRunning = false;
        canRun = true;
    }

    void Start()
    {
        player = GetComponent<Player>();
        cameraSwtich = GetComponentInChildren<CameraSwitch>();
    }
    private void FixedUpdate()
    {
        IsOnJumpBoard();
        Move();
    }
    void Update()
    {
    }
    private void LateUpdate()
    {
        CameraLook();
    }

    private void Move()
    {
        Vector3 dir = (transform.forward * currentMovementInput.y) + (transform.right * currentMovementInput.x);
        if(player.statHandler.Stamina.CurrValue > 0f)
        {
            moveSpeed = isRunning ? baseSpeed * runRate : baseSpeed;
        }
        else
        {
            moveSpeed = baseSpeed;
        }

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

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && canRun)
        {
            isRunning = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRunning = false;
        }
    }

    private void CameraLook()
    {
        if (cameraSwtich.State == CAMERA_STATE.MAIN)
            Cursor.lockState = CursorLockMode.Locked;

        cameraCurrentXRotation += mouseDelta.y * lookSensitivity;
        cameraCurrentXRotation = Mathf.Clamp(cameraCurrentXRotation, minXLook, maxXLook);

        float currentYRotation = transform.eulerAngles.y;
        currentYRotation += mouseDelta.x * lookSensitivity;

        cameraContainer.localRotation = Quaternion.Euler(-cameraCurrentXRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        /*
        cameraContainer.localEulerAngles = new Vector3(-cameraCurrentXRotation, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);*/

    }
    public void OnLook(InputAction.CallbackContext context)
    {

        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if(player.statHandler.Stamina.CurrValue >= 5f)
            {
                player.statHandler.UseStamina(5);
                rigidBody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("스태미너 부족함!");
            }
        }

    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position + Vector3.down * 0.6f, 0.5f, jumpAbleMask);
    }

    public void IsOnJumpBoard()
    {
        if( Physics.CheckSphere(transform.position + Vector3.down * 0.6f, 0.5f, 1 << LayerMask.NameToLayer("Jumpboard")))
        {
            Debug.Log("Jumpboard 감지됨!");
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            rigidBody.AddForce(Vector2.up * 15, ForceMode.Impulse);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * 0.6f, 0.5f);
    }

    public void RunPermission(bool value)
    {
        if (isRunning)
        {
            isRunning = false;
        }
        canRun = value;
    }


    public void ApplyJumpBoost(float multiplier, float duration)
    {
        if (jumpBuffCoroutine != null)
            StopCoroutine(jumpBuffCoroutine);

        jumpBuffCoroutine = StartCoroutine(JumpBoostRoutine(multiplier, duration));
    }

    private IEnumerator JumpBoostRoutine(float multiplier, float duration)
    {
        jumpPower *= multiplier;
        yield return new WaitForSeconds(duration);
        jumpPower /= multiplier;
    }

}
