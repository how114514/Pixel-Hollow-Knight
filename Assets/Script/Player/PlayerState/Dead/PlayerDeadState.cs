using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    float deadTime = 5f;

    public PlayerDeadState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }
    public void OnEnter()
    {
        player.rb.bodyType = RigidbodyType2D.Static;
        player.GetComponent<Collider2D>().enabled = false;

        player.anim.Play("dead");
    }

    public void OnUpdate()
    {
        deadTime -= Time.deltaTime;
        if (deadTime < 0)
        {
            player.ReStart();
        }
    }

    public void OnExit()
    {

    }
}
