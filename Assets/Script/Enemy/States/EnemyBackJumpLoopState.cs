using UnityEngine;

public class EnemyBackJumpLoopState : EnemyState
{
	private enum Phase
	{
		Jump,
		Fall,
		ChargeWindup,
		Charge,
		ChargeRecover,
		CastShockwave
	}

	private const float ShockwaveChance = 0.4f;

	private Phase phase;

	private int wallDir;

	private bool isShockwaveBranch;

	public EnemyBackJumpLoopState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		float num = Mathf.Abs(base.enemy.player.position.x - base.enemy.leftWallX);
		float num2 = Mathf.Abs(base.enemy.player.position.x - base.enemy.rightWallX);
		wallDir = ((!(num > num2)) ? 1 : (-1));
		EnterJump();
	}

	public override void Update()
	{
		switch (phase)
		{
		case Phase.Jump:
			TickJump();
			break;
		case Phase.Fall:
			TickFall();
			break;
		case Phase.ChargeWindup:
			TickChargeWindup();
			break;
		case Phase.Charge:
			TickCharge();
			break;
		case Phase.ChargeRecover:
			TickChargeRecover();
			break;
		case Phase.CastShockwave:
			TickCastShockwave();
			break;
		}
	}

	public override void FixedUpdate()
	{
		if (phase == Phase.Charge)
		{
			base.enemy.movement.velocity = new Vector2((float)(-wallDir) * base.enemy.movement.chargeSpeed, base.enemy.rb.linearVelocity.y);
		}
	}

	private void EnterJump()
	{
		phase = Phase.Jump;
		float x = ((wallDir == -1) ? base.enemy.leftWallX : base.enemy.rightWallX);
		Vector3 targetPos = new Vector3(x, base.enemy.transform.position.y, 0f);
		base.enemy.movement.JumpAbove(targetPos);
		base.enemy.audio.PlayJump();
		base.enemy.animation.Play("jump");
	}

	private void TickJump()
	{
		base.enemy.movement.FaceTarget(base.enemy.player.position);
		if ((wallDir == -1) ? base.enemy.physicsCheck.touchLeftWall : base.enemy.physicsCheck.touchRightWall)
		{
			EnterFall();
		}
	}

	private void EnterFall()
	{
		phase = Phase.Fall;
		isShockwaveBranch = Random.value < 0.4f;
		base.enemy.movement.velocity = Vector2.zero;
		base.enemy.animation.Play("fall");
	}

	private void TickFall()
	{
		if (base.enemy.physicsCheck.isGround)
		{
			base.enemy.movement.MoveX(0f);
			base.enemy.audio.PlayLand();
			if (isShockwaveBranch)
			{
				EnterCastShockwave();
			}
			else
			{
				EnterChargeWindup();
			}
		}
	}

	private void EnterChargeWindup()
	{
		phase = Phase.ChargeWindup;
		base.enemy.movement.MoveX(0f);
		base.enemy.movement.FaceTarget(base.enemy.player.position);
		base.enemy.animation.Play("chargeWindup");
	}

	private void TickChargeWindup()
	{
		if (base.enemy.animation.IsDone)
		{
			EnterCharge();
		}
	}

	private void EnterCharge()
	{
		phase = Phase.Charge;
		Vector3 position = base.enemy.transform.position;
		position.y += base.enemy.movement.chargeHeight;
		base.enemy.transform.position = position;
		base.enemy.movement.gravityScale = 0f;
		base.enemy.movement.gravityLocked = true;
		base.enemy.audio.PlayCharge();
		CameraShakeManager.Instance?.StartContinuous(0.15f);
		base.enemy.animation.Play("charge");
	}

	private void TickCharge()
	{
		if ((wallDir == -1) ? base.enemy.physicsCheck.touchRightWall : base.enemy.physicsCheck.touchLeftWall)
		{
			base.enemy.movement.velocity = Vector2.zero;
			base.enemy.movement.gravityLocked = false;
			base.enemy.movement.gravityScale = base.enemy.movement.normalGravity;
			EnterChargeRecover();
		}
	}

	private void EnterChargeRecover()
	{
		phase = Phase.ChargeRecover;
		CameraShakeManager.Instance?.StopContinuous();
		base.enemy.movement.MoveX(0f);
		base.enemy.animation.Play("chargeRecover");
	}

	private void TickChargeRecover()
	{
		if (base.enemy.animation.IsDone)
		{
			base.sm.ChangeState(base.enemy.decisionState);
		}
	}

	private void EnterCastShockwave()
	{
		phase = Phase.CastShockwave;
		base.enemy.shockwaveDir = ((wallDir == -1) ? 1 : (-1));
		base.enemy.audio.PlayCastShockwave();
		base.enemy.animation.Play("castShockwave");
	}

	private void TickCastShockwave()
	{
		if (base.enemy.animation.IsDone)
		{
			base.sm.ChangeState(base.enemy.decisionState);
		}
	}

	public override void Exit()
	{
	}
}
