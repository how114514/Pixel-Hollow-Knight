using UnityEngine;

public class PlayerActionHurtState : PlayerState
{
	public PlayerActionHurtState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.StartInvincibility();
		base.player.movementLocked = true;
		CameraShakeManager.Instance?.Shake(0.3f);
		base.player.audio.PlayHurt();
		base.player.animation.Play("hurt");
		float num = ((base.player.transform.position.x - base.player.lastDamageSourcePos.x >= 0f) ? 1 : (-1));
		base.player.movement.velocity = new Vector2(num * base.player.hurtRecoilForce, base.player.rb.linearVelocity.y);
		int num2 = ((base.player.lastDamageSourcePos.x > base.player.transform.position.x) ? 1 : (-1));
		base.player.transform.localScale = new Vector3(num2, 1f, 1f);
	}

	public override void Update()
	{
		if (base.player.animation.IsDone)
		{
			base.sm.ChangeState(base.player.noneState);
		}
	}

	public override void Exit()
	{
		base.player.movementLocked = false;
	}
}
