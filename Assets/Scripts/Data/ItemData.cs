using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ITEM_TYPE
{
    EQUIPABLE,
    CONSUMABLE
}

public enum CONSUMABLE_TYPE
{
    HEALTH,
    HUNGER,
    STAMINA
}

[System.Serializable]
public class ItemDataConsumable
{
    public CONSUMABLE_TYPE type;
    public float value;
}

[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("[ ������ ���� ]")]
    public int code;
    public string itemName;
    public string context;
    public ITEM_TYPE type;
    public Sprite icon;
    public GameObject prefab;

    [Header("[ ���� ���� ]")]
    public bool isStacking;
    public int maxStackAmount;

    [Header("[ �Ҹ�ǰ ���� ]")]
    public ItemDataConsumable[] consumables;

}
