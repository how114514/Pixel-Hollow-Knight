using UnityEngine;

public class PlayerActionUpRoarState : PlayerState
{
	public PlayerActionUpRoarState(PlayerStateMachine sm)
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
		base.player.audio.PlayUpRoar();
		CameraShakeManager.Instance?.StartContinuous(0.15f);
		base.player.animation.Play("upRoar");
	}

	public override void Update()
	{
		if (!TryEnterPriorityState() && base.player.animation.IsDone)
		{
			base.player.ability.Execute(AbilityType.UpRoar);
			base.sm.ChangeState(base.player.noneState);
		}
	}

	public override void Exit()
	{
		CameraShakeManager.Instance?.StopContinuous();
		base.player.movementLocked = false;
		base.player.movement.gravityLocked = false;
		base.player.movement.gravityScale = 2f;
	}
}
