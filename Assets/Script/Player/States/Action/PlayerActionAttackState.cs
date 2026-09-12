using UnityEngine;

public class PlayerActionAttackState : PlayerState
{
	private const float DeadZone = 0.3f;

	private float timer;

	public PlayerActionAttackState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		timer = 0.3f;
		float y = base.player.input.MoveInput.y;
		if (y > 0.3f)
		{
			base.player.combat.BeginAttack(AttackType.Up);
			base.player.audio.PlayAttackUp();
			base.player.animation.Play("attackUp");
		}
		else if (y < -0.3f && !base.player.physicsCheck.isGround)
		{
			base.player.combat.BeginAttack(AttackType.Down);
			base.player.audio.PlayAttackDown();
			base.player.animation.Play("attackDown");
		}
		else
		{
			base.player.combat.BeginAttack(AttackType.Horizontal);
			base.player.audio.PlayAttackHorizontal();
			base.player.animation.Play((base.player.attackIndex == 0) ? "attack1" : "attack2");
			base.player.attackIndex = ((base.player.attackIndex == 0) ? 1 : 0);
		}
	}

	public override void Update()
	{
		if (!TryEnterPriorityState())
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				base.sm.ChangeState(base.player.noneState);
			}
		}
	}

	public override void Exit()
	{
		base.player.combat.EndAttack();
	}
}
