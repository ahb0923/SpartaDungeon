using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIGuages Guages { get; set; }
    public UIInfoWindow InfoWindow { get; set; }

    public UIItemSlots ItemSlots { get; set; }

    public UIBuffWindow BuffWindow { get; set; }


    void Start()
    {
        if(Guages == null)
        {
            Debug.Log("Guages is Null");
            Guages = FindAnyObjectByType<UIGuages>();
        }
        if (InfoWindow == null)
        {
            Debug.Log("InfoWindow is Null");
            InfoWindow  = FindAnyObjectByType<UIInfoWindow>();
        }
        if (ItemSlots == null)
        {
            Debug.Log("ItemSlots is Null");
            ItemSlots = FindAnyObjectByType<UIItemSlots>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
