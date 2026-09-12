using UnityEngine;

public class PlayerActionHealState : PlayerState
{
	private enum Phase
	{
		Charge,
		Healed
	}

	private Phase phase;

	public PlayerActionHealState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.movementSM.ChangeState(base.player.idleState);
		base.player.movementLocked = true;
		if (base.player.rb.linearVelocity.y > 0f)
		{
			base.player.movement.velocity = new Vector2(0f, 0f);
		}
		EnterCharge();
	}

	public override void Update()
	{
		if (!TryEnterPriorityState())
		{
			switch (phase)
			{
			case Phase.Charge:
				TickCharge();
				break;
			case Phase.Healed:
				TickHealed();
				break;
			}
		}
	}

	public override void Exit()
	{
		base.player.movementLocked = false;
	}

	private void EnterCharge()
	{
		phase = Phase.Charge;
		base.player.audio.PlayHealCharge();
		base.player.animation.Play("heal");
	}

	private void TickCharge()
	{
		if (!base.player.input.HealHeld)
		{
			base.sm.ChangeState(base.player.noneState);
		}
		else if (base.player.animation.IsDone)
		{
			EnterHealed();
		}
	}

	private void EnterHealed()
	{
		phase = Phase.Healed;
		base.player.ability.Execute(AbilityType.Heal);
		base.player.audio.PlayHeal();
		base.player.animation.Play("healed");
	}

	private void TickHealed()
	{
		if (base.player.animation.IsDone)
		{
			base.sm.ChangeState(base.player.noneState);
		}
	}
}
