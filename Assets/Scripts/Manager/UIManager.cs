using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIGuages Guages { get; set; }


    void Start()
    {
        Guages = FindAnyObjectByType<UIGuages>();
        if (Guages == null)
            Debug.Log("Guages is Null");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
