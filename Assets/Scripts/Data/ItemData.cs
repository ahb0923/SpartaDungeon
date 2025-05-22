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
    [Header("[ 아이템 정보 ]")]
    public int code;
    public string itemName;
    public string context;
    public ITEM_TYPE type;
    public Sprite icon;
    public GameObject prefab;

    [Header("[ 스택 정보 ]")]
    public bool isStacking;
    public int maxStackAmount;

    [Header("[ 소모품 정보 ]")]
    public ItemDataConsumable[] consumables;

}
