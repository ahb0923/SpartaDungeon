using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    private const float  subDistance = 11.35f;
    private const float mainDistance = 5f;
    public LayerMask layerMask;

    public GameObject currentObject;
    private IInteractable currentInteractable;

    public TextMeshProUGUI promptText;

    [SerializeField]
    public Transform startRay; 
    private Camera camera;

    private void Awake()
    {
        promptText.gameObject.SetActive(false);
       // startRay = 
    }

    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            //Ray ray = new Ray(startRay.position, startRay.forward);
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            maxCheckDistance = GameManager.Instance.player.moveController.cameraSwtich.State == CAMERA_STATE.SUB ? subDistance : mainDistance;

            Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.blue, checkRate);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                Debug.Log($"오브젝트 충돌함!! [{hit.collider.gameObject}]");

                if(hit.collider.gameObject != currentObject)
                {
                    currentObject = hit.collider.gameObject;
                    currentInteractable = hit.collider.GetComponent<IInteractable>();
                    UIManager.Instance.InfoWindow.SettingWindow(currentObject.GetComponent<ItemObject>().data);
                    promptText.gameObject.SetActive(true);
                }
            }
            else
            {
                currentInteractable = null;
                currentObject = null;
                UIManager.Instance.InfoWindow.gameObject.SetActive(false);
                promptText.gameObject.SetActive(false);
            }
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && currentInteractable != null)
        {
            currentInteractable.OnInteract();
            currentObject = null;
            currentInteractable = null;
            UIManager.Instance.InfoWindow.gameObject.SetActive(false);
            promptText.gameObject.SetActive(false);
        }
    }
}
