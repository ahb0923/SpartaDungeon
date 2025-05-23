using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemSlots[] slots;
    private ItemSlots selectedItem;


    private PlayerMoveController moveController;
    private PlayerStatHandler statHandler;
    private Transform dropPosition;



    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.Instance.player.addItem += AddItem;

        moveController = GameManager.Instance.player.moveController;
        statHandler = GameManager.Instance.player.statHandler;
        dropPosition = GameManager.Instance.player.dropPosition;

        var uiSlots = UIManager.Instance.ItemSlots.slots;
        slots = new ItemSlots[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = uiSlots[i].GetComponent<ItemSlots>();
        }

    }

    private void Update()
    {
        selectedItem = slots[UIManager.Instance.ItemSlots.SelectedSlotIndex];
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUseButton();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            OnDropButton();
        }

    }

    public void AddItem()
    {
        ItemData data = GameManager.Instance.player.itemData;

        if (data.isStacking)
        {
            ItemSlots slot = GetItemStack(data);
            if(slot != null)
            {
                slot.quantity++;
                UpdateUI();
                GameManager.Instance.player.itemData = null;
                return;
            }
        }
        ItemSlots emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            GameManager.Instance.player.itemData = null;
            return;
        }

        ThrowItem(data);
        GameManager.Instance.player.itemData = null;
    }


    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlots GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.code == data.code && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }
    ItemSlots GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data.prefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    //presse "mouse[Right Click]"
    public void OnUseButton()
    {
        if(selectedItem == null)
        { 
            Debug.Log("빈 아이템 공간입니다."); 
        }
        if (selectedItem.item.type == ITEM_TYPE.CONSUMABLE)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                switch (selectedItem.item.consumables[i].type)
                {
                    case CONSUMABLE_TYPE.HEALTH:
                        statHandler.Heal(selectedItem.item.consumables[i].value); 
                        break;
                    case CONSUMABLE_TYPE.HUNGER:
                        statHandler.Eat(selectedItem.item.consumables[i].value); 
                        break;
                    case CONSUMABLE_TYPE.STAMINA:
                        statHandler.RecoverStamina(selectedItem.item.consumables[i].value);
                        break;
                }
            }
            RemoveSelctedItem();
        }
    }
    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelctedItem();
    }


    private void RemoveSelctedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            selectedItem.item = null;
        }

        UpdateUI();
    }
}