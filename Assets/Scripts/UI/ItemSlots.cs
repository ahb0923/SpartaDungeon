using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlots : MonoBehaviour
{
    public ItemData item;

    [SerializeField]
    public int index;

    public bool equiped;
    public int quantity;
    public TextMeshProUGUI eaText;
    public Image iconImg;

    public Color highlightColor;

    [SerializeField]
    public Transform background_h;
    [SerializeField]
    public Transform icon;
    [SerializeField]
    public Transform ea;
    [SerializeField]
    public Transform pivot;



    private void Awake()
    {
        eaText = ea.gameObject.GetComponent<TextMeshProUGUI>();
        iconImg = icon.gameObject.GetComponent<Image>();

        eaText.text = string.Empty;
        icon.gameObject.SetActive(false);
       
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Set()
    {
        icon.gameObject.SetActive(true);
        iconImg.sprite = item.icon;
        eaText.text = quantity > 1 ? quantity.ToString() : string.Empty;
        OffHighlights();
    }
    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        eaText.text = string.Empty;
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

        if (quantity > 1)
        {
            OnEa();
        }
        else OffEa();
    }
    public void OnHighlights()
    {
        background_h.gameObject.SetActive(true);
        iconImg.color = highlightColor;
    }
    public void OffHighlights()
    {
        background_h.gameObject.SetActive(false);
        iconImg.color = Color.white;
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

