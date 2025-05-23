using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIBuffWindow : MonoBehaviour
{
    public BuffSlots[] buffIcons; // 미리 만든 아이콘들

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
            Debug.LogWarning("버프 슬롯이 부족합니다.");
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
        TMP_Text timeText = slot.timeText;

        elapsed += Time.deltaTime;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fillImage.fillAmount = 0 + (elapsed / duration);

            if (timeText != null)
            {
                timeText.text = Mathf.CeilToInt(duration - elapsed).ToString();
            }

            yield return null;
        }

        fillImage.fillAmount = 1;
        slot.gameObject.SetActive(false);
    }
}
