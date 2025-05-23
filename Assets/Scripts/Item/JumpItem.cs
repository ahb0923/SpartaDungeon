using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class JumpItem : MonoBehaviour
{
    public ItemData data;
    public float jumpBoostRate = 2f;

    // �̺κе� data.duration ���� ��� ���� ���� ����
    public float duration = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.moveController�� ����ص� ��, �浹ü ���� Ȯ���ϵ��� ��������
            var moveController = other.GetComponent<PlayerMoveController>();
            if (moveController != null)
            {
                moveController.ApplyJumpBoost(jumpBoostRate, duration);
            }
            Sprite icon = data.icon;
            float buffDuration = data.durationTime;

            UIManager.Instance.BuffWindow.ActivateBuff(data.icon, data.durationTime);
            StartCoroutine(DestroyAfterFrame());
        }
    }

    private IEnumerator DestroyAfterFrame()
    {
        yield return null;
        Destroy(gameObject);
    }
}
