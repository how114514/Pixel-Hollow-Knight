using System;
using UnityEngine;

public class PlayerAttackState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerAttackState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    private enum AttackRecoilType
    {
        Horizontal,
        Up,
        Down
    }

    private AttackRecoilType currentRecoilType;
    private bool hasHitEnemy;
    private float recoilTimer;
    private bool isRecoiling;

    public void OnEnter()
    {
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, 0);

        player.isAttacking = true;
        hasHitEnemy = false;
        isRecoiling = false;
        recoilTimer = 0f;

        Vector2 inputDirection = player.inputControl.GamePlay.Move.ReadValue<Vector2>();

        if (inputDirection.y > 0.1f)
        {
            currentRecoilType = AttackRecoilType.Up;
            player.anim.Play("attackUp");
            player.PlaySFX(player.attackUpClip);
        }
        else if ((inputDirection.y < -0.1f) && (!player.physicsCheck.isGround))
        {
            currentRecoilType = AttackRecoilType.Down;
            player.anim.Play("attackDown");
            player.PlaySFX(player.attackDownClip);
        }
        else
        {
            currentRecoilType = AttackRecoilType.Horizontal;

            if (player.attackIndex == 0)
            {
                player.anim.Play("attack1");
                player.PlaySFX(player.attack1Clip);
            }
            else
            {
                player.anim.Play("attack2");
                player.PlaySFX(player.attack2Clip);
            }
        }
    }

    public void OnUpdate()
    {
        if (isRecoiling)
        {
            recoilTimer -= Time.deltaTime;

            if (recoilTimer <= 0f)
            {
                isRecoiling = false;
            }

            return;
        }

        Move();
    }

    public void OnExit()
    {
        if (player.attackIndex == 0)
        {
            player.attackIndex = 1;
        }
        else
        {
            player.attackIndex = 0;
        }

        player.isAttacking = false;
    }

    private void Move()
    {
        Vector2 inputDirection = player.inputControl.GamePlay.Move.ReadValue<Vector2>();

        player.rb.linearVelocity = new Vector2(inputDirection.x * player.moveSpeed, player.rb.linearVelocity.y);
    }

    public void OnHitEnemy()
    {
        if (hasHitEnemy) return;

        hasHitEnemy = true;

        player.currentEnergy++;
        player.currentEnergy = Mathf.Clamp(player.currentEnergy, 0, player.maxEnergy);
        player.energyUI.SetEnergy(player.currentEnergy);

        switch (currentRecoilType)
        {
            case AttackRecoilType.Horizontal:
                float faceDir = player.transform.localScale.x;
                player.rb.linearVelocity = new Vector2(-faceDir * player.recoilSpeedX, player.rb.linearVelocity.y);

                isRecoiling = true;
                recoilTimer = 0.1f;
                break;

            case AttackRecoilType.Down:
                player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.recoilSpeedY);
                player.RecoveryAbility();
                break;

            case AttackRecoilType.Up:
                break;
        }
    }
}