using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBuffWindow : MonoBehaviour
{
    public BuffSlots[] buffIcons; // �̸� ���� �����ܵ�

    private void Awake()
    {
        UIManager.Instance.BuffWindow = this;
        foreach(var icon in buffIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }

    public void ActivateBuff(Sprite icon, float duration)
    {
        BuffSlots slot = GetAvailableSlot();
        if (slot == null)
        {
            Debug.LogWarning("���� ������ �����մϴ�.");
            return;
        }

        slot.iconImage.sprite = icon;
        slot.gameObject.SetActive(true);
        StartCoroutine(HandleBuffDuration(slot, duration));
    }

    private BuffSlots GetAvailableSlot()
    {
        foreach (var slot in buffIcons)
        {
            if (!slot.gameObject.activeSelf)
                return slot;
        }
        return null;
    }

    private IEnumerator HandleBuffDuration(BuffSlots slot, float duration)
    {
        float elapsed = 0f;
        Image fillImage = slot.cooldownImage;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fillImage.fillAmount = 1 - (elapsed / duration);
            yield return null;
        }

        fillImage.fillAmount = 0;
        slot.gameObject.SetActive(false);
    }
}
