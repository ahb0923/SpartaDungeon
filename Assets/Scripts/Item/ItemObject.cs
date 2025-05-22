using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract();
}
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public void OnInteract()
    {
        GameManager.Instance.player.itemData = data;
        GameManager.Instance.player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
