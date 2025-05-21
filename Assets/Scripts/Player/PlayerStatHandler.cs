using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    private Player player;

    [Header("[ Ω∫≈» ]")]
    [SerializeField]
    private Status health;
    public Status Health { get; set; }

    [SerializeField]
    private Status hunger;
    public Status Hunger { get; set; }

    [SerializeField]
    private Status stamina;
    public Status Stamina { get; set; }

    [SerializeField]
    private float starveDamagae;


    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Health!=null)
        Health.Increase(Health.ChangeValue * Time.deltaTime);
        if (player.state == PLAYER_STATE.IDLE)
        {
            Stamina.Increase(stamina.ChangeValue * Time.deltaTime);
        }
        if(hunger.CurrValue <= 0f)
        {
            Health.Decrease(starveDamagae * Time.deltaTime);
        }
        if(health.CurrValue <= 0f)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        health.Increase(value);
    }

    public void Eat(float value)
    {
        hunger.Increase(value);
    }

    public void Die()
    {
        player.state = PLAYER_STATE.DIE;
    }

    public bool UseStamina(float value)
    {
        if(stamina.CurrValue - value < 0)
        {
            return false;
        }
        stamina.Decrease(value);
        return true;
    }
}
