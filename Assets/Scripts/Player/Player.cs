using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public PLAYER_STATE state;
    // Start is called before the first frame update

    private void Awake()
    {
        state = PLAYER_STATE.IDLE;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
