using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
public enum PLAYER_STATE
{
    IDLE,
    MOVE,
    RUN,
    JUMP,
    DIE
}
public class Player : MonoBehaviour
{
    [SerializeField]
    public PLAYER_STATE state;
    [SerializeField]
    public PlayerStatHandler statHandler;
    [SerializeField]
    public PlayerMoveController moveController;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        state = PLAYER_STATE.IDLE;
        GameManager.Instance.player = this;
        statHandler = GetComponent<PlayerStatHandler>();
        moveController = GetComponent<PlayerMoveController>();
    }
    private void Update()
    {
        UpdateState();
    }
    public void SetState(PLAYER_STATE newState)
    {
        state = newState;
    }

    private void UpdateState()
    {
        // ���� ������� ���� ��
        if (!moveController.IsGrounded())
        {
            SetState(PLAYER_STATE.JUMP);
        }
        // �޸����� + �̵��Է����� + ���¹̳� 0�̻�
        else if (moveController.isRunning && moveController.currentMovementInput != Vector2.zero && statHandler.Stamina.CurrValue > 0f)
        {
            SetState(PLAYER_STATE.RUN);
        }
        // �̵��Է� ���� ��
        else if (moveController.currentMovementInput != Vector2.zero)
        {
            SetState(PLAYER_STATE.MOVE);
        }
        else
        {
            SetState(PLAYER_STATE.IDLE);
        }
    }
}
