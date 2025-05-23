using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    private Player player;

    public Status Health { get; set; }
    public Status Hunger { get; set; }
    public Status Stamina { get; set; }

    [SerializeField]
    private float starveDamagae;


    void Start()
    {
        player = GetComponent<Player>();
        Health = UIManager.Instance.Guages.health.GetComponent<Status>();
        Hunger = UIManager.Instance.Guages.hunger.GetComponent<Status>();
        Stamina = UIManager.Instance.Guages.stamina.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Health!=null)
        Health.Increase(Health.ChangeValue * Time.deltaTime);
        Hunger.Decrease(Hunger.ChangeValue * Time.deltaTime);

        if (player.state == PLAYER_STATE.IDLE)
        {
            Stamina.Increase(Stamina.ChangeValue * Time.deltaTime);
        }
        else if (player.state == PLAYER_STATE.MOVE)
        {
            Stamina.Increase(Stamina.ChangeValue * 0.5f * Time.deltaTime);
        }
        else if (player.state == PLAYER_STATE.RUN)
        {
            Stamina.Decrease(Stamina.ChangeValue * Time.deltaTime);
        }

        if(Hunger.CurrValue <= 0f)
        {
            Health.Decrease(starveDamagae * Time.deltaTime);
        }

        if(Health.CurrValue <= 0f)
        {
            Die();
        }

        if (Stamina.CurrValue <= 0f)
        {
            player.moveController.RunPermission(false);
        }
        if (!player.moveController.canRun && Stamina.CurrValue >= 10f)
        {
            player.moveController.RunPermission(true);
        }

    }

    public void Heal(float value)
    {
        Health.Increase(value);
    }

    public void Eat(float value)
    {
        Hunger.Increase(value);
    }

    public void RecoverStamina(float value)
    {
        Stamina.Increase(value);
    }

    public void Die()
    {
        player.state = PLAYER_STATE.DIE;
    }

    public bool UseStamina(float value)
    {
        if(Stamina.CurrValue - value < 0)
        {
            return false;
        }
        Stamina.Decrease(value);
        return true;
    }
}
