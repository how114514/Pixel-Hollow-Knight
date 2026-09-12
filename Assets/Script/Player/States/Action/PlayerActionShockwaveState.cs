using UnityEngine;

public class PlayerActionShockwaveState : PlayerState
{
	public PlayerActionShockwaveState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.movementSM.ChangeState(base.player.idleState);
		base.player.movementLocked = true;
		if (!base.player.stats.TryConsumeEnergy(3))
		{
			base.sm.ChangeState(base.player.noneState);
			return;
		}
		base.player.movement.gravityScale = 0f;
		base.player.movement.gravityLocked = true;
		base.player.movement.velocity = Vector2.zero;
		base.player.audio.PlayShockwave();
		base.player.animation.Play("castShockwave");
	}

	public override void Update()
	{
		if (!TryEnterPriorityState() && base.player.animation.IsDone)
		{
			base.player.ability.Execute(AbilityType.Shockwave);
			base.sm.ChangeState(base.player.noneState);
		}
	}

	public override void Exit()
	{
		base.player.movementLocked = false;
		base.player.movement.gravityLocked = false;
		base.player.movement.gravityScale = 2f;
	}
}
