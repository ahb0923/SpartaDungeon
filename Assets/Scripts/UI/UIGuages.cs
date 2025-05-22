using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuages : MonoBehaviour
{
    [SerializeField]
    public Transform health;
    [SerializeField]
    public Transform hunger;
    [SerializeField]
    public Transform stamina;

    private void Awake()
    {
        UIManager.Instance.Guages = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (health == null)
            Debug.Log("Health is Null");
        if (hunger == null)
            Debug.Log("Hunger is Null");
        if (stamina == null)
            Debug.Log("Stamina is Null");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
