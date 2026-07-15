using System;
using UnityEngine;
using UnityEngine.InputSystem;

//��������
public class EnemyUppercutAttackState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyUppercutAttackState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.rb.linearVelocity = new Vector2(enemy.uppercutSpeedX * -enemy.facingDir, enemy.uppercutSpeedY);
        enemy.anim.Play("uppercutAttack");
        enemy.PlaySFX(enemy.SlashClip);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        enemy.fallByAttack = true;
    }
}