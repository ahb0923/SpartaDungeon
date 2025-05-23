using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UIItemSlots : MonoBehaviour
{
    [SerializeField]
    private int selectedSlotIndex;
    public int SelectedSlotIndex { get => selectedSlotIndex; set => selectedSlotIndex = value; }

    [SerializeField] 
    public GameObject[] slots;

    private void Awake()
    {
        UIManager.Instance.ItemSlots = this;
        int count = 0;
        foreach(var slot in slots)
        {
            slot.transform.GetComponent<ItemSlots>().index = count++;
            slot.transform.GetComponent<ItemSlots>().OffHighlights();
            slot.transform.GetComponent<ItemSlots>().OffEa();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0)
        {
            selectedSlotIndex--;
        }
        else if (scroll < 0)
        {
            selectedSlotIndex++;
        }

        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex, 0, slots.Length - 1);
    }
}
