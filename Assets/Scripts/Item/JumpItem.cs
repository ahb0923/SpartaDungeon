using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class JumpItem : MonoBehaviour
{
    public ItemData data;
    public float jumpBoostRate = 2f;

    // 이부분도 data.duration 으로 사용 가능 추후 수정
    public float duration = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.moveController를 사용해도 됨, 충돌체 정보 확인하도록 만들어봤음
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
