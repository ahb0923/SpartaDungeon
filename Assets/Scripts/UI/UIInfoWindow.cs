using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoWindow : MonoBehaviour
{
    [Header("[ UIÁ¤º¸ ]")]
    [SerializeField] 
    TextMeshProUGUI mainTitle;
    [SerializeField] 
    TextMeshProUGUI subTitle;
    [SerializeField] 
    TextMeshProUGUI context;
    [SerializeField] 
    Image icon;
    // Start is called before the first frame update

    private void Awake()
    {
        UIManager.Instance.InfoWindow = this;
    }

    public void SettingWindow(ItemData data)
    {
        mainTitle.text = data.itemName;
        subTitle.text = data.type.ToString();
        context.text = data.context;
        icon.sprite = data.icon;

        transform.gameObject.SetActive(true);
    }
}
