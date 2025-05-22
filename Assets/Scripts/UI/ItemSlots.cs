using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
    [SerializeField]
    public int index;
    [SerializeField]
    public Transform background_h;
    [SerializeField]
    public Transform icon_h;
    [SerializeField]
    public Transform ea;
    [SerializeField]
    public Transform pivot;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.ItemSlots.SelectedSlotIndex == index)
        {
            OnHighlights();
        }
        else
        {
            OffHighlights();
        }
    }
    public void OnHighlights()
    {
        background_h.gameObject.SetActive(true);
        icon_h.gameObject.SetActive(true);
    }
    public void OffHighlights()
    {
        background_h.gameObject.SetActive(false);
        icon_h.gameObject.SetActive(false);
    }
    public void OnEa()
    {
        ea.gameObject.SetActive(true);
    }
    public void OffEa()
    {
        ea.gameObject.SetActive(false);
    }
}

