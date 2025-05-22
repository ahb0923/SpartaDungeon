using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public enum CAMERA_STATE
{
    MAIN,
    SUB
}
public class CameraSwitch : MonoBehaviour
{
    [Header("Camera Setting")]
    [SerializeField]    // 실제 카메라
    private Transform mainCamera;

    [SerializeField]    // 3인칭
    private Transform mainCameraPos;

    [SerializeField]    // 1인칭
    private Transform subCameraPos;

    [SerializeField] 
    private float lerpSpeed = 5f;

    [SerializeField]
    private CAMERA_STATE state; 
    public CAMERA_STATE State { get => state; set { state = value; } }


    private bool isMainCam = true;
    public bool isLerping = false;
    private Transform target;

    private void Awake()
    {
        State = CAMERA_STATE.MAIN;
    }
    private void Start()
    {
        target = mainCameraPos;
        mainCamera.position = target.position;
        mainCamera.rotation = target.rotation;  
    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        //Debug.Log("카메라 전환 호출");
        if (context.phase == InputActionPhase.Performed)
        {
            isMainCam = !isMainCam;
            target = isMainCam ? mainCameraPos : subCameraPos;
            State = isMainCam ? CAMERA_STATE.MAIN : CAMERA_STATE.SUB;
            isLerping = true;
        }
    }
    private void Update()
    {
        if (isLerping)
        {
            mainCamera.position = Vector3.Lerp(mainCamera.position, target.position, Time.deltaTime * lerpSpeed);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, target.rotation, Time.deltaTime * lerpSpeed);

            float distance = Vector3.Distance(mainCamera.position, target.position);
            float angle = Quaternion.Angle(mainCamera.rotation, target.rotation);

            if (distance < 0.01f && angle < 0.5f)
            {
                mainCamera.position = target.position;
                mainCamera.rotation = target.rotation;
                isLerping = false;
            }
        }
    }
}
