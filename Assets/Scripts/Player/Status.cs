using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status : MonoBehaviour
{
    [Header("[ Á¤º¸ ]")]
    [SerializeField]
    private float currValue;
    public float CurrValue 
    { 
        get => currValue; 
        set => currValue = value; 
    }

    [SerializeField]
    private float maxValue;
    public float MaxValue 
    {
        get => maxValue;
        set => maxValue = value;
    }

    [SerializeField]
    private float changeValue;
    public float ChangeValue
    {
        get => changeValue;
        set => changeValue = value;
    }

    [SerializeField]
    public Image bar;


    private void Update()
    {
        bar.fillAmount = currValue / maxValue;
    }
    public void Increase(float value)
    {
        currValue = Mathf.Min(currValue + value, maxValue);
    }
    public void Decrease(float value)
    {
        currValue = Mathf.Max(currValue - value, 0);
    }
}
