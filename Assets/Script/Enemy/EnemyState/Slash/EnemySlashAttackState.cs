using System;
using UnityEngine;
using UnityEngine.InputSystem;

//��ɨ����
public class EnemySlashAttackState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemySlashAttackState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.rb.linearVelocity = new Vector2(enemy.slashSpeed * -enemy.facingDir, 0);
        enemy.anim.Play("slashAttack");
        enemy.PlaySFX(enemy.SlashClip);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}