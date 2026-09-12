using UnityEngine;

public class EnemyMoveLoopState : EnemyState
{
	private enum Phase
	{
		Move,
		SlashWindup,
		Slash,
		UpperSlashWindup,
		UpperSlash,
		Fall
	}

	private Phase phase;

	private float moveDir;

	public EnemyMoveLoopState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		EnterMove();
	}

	public override void Update()
	{
		switch (phase)
		{
		case Phase.Move:
			TickMove();
			break;
		case Phase.SlashWindup:
			TickSlashWindup();
			break;
		case Phase.Slash:
			TickSlash();
			break;
		case Phase.UpperSlashWindup:
			TickUpperSlashWindup();
			break;
		case Phase.UpperSlash:
			TickUpperSlash();
			break;
		case Phase.Fall:
			TickFall();
			break;
		}
	}

	public override void FixedUpdate()
	{
		if (phase == Phase.Move)
		{
			base.enemy.movement.MoveX(moveDir * base.enemy.movement.moveSpeed);
		}
	}

	private void EnterMove()
	{
		phase = Phase.Move;
		base.enemy.animation.Play("move");
	}

	private void TickMove()
	{
		float num = Mathf.Abs(base.enemy.player.position.x - base.enemy.transform.position.x);
		moveDir = base.enemy.movement.FaceTarget(base.enemy.player.position);
		if (num <= base.enemy.movement.stopDistance)
		{
			EnterSlashWindup();
		}
	}

	private void EnterSlashWindup()
	{
		phase = Phase.SlashWindup;
		base.enemy.movement.MoveX(0f);
		moveDir = base.enemy.movement.FaceTarget(base.enemy.player.position);
		base.enemy.animation.Play("slashWindup");
	}

	private void TickSlashWindup()
	{
		if (base.enemy.animation.IsDone)
		{
			EnterSlash();
		}
	}

	private void EnterSlash()
	{
		phase = Phase.Slash;
		base.enemy.movement.velocity = new Vector2(moveDir * base.enemy.movement.slashSpeed, 0f);
		base.enemy.audio.PlaySlash();
		base.enemy.animation.Play("slash");
	}

	private void TickSlash()
	{
		if (base.enemy.animation.IsDone)
		{
			EnterUpperSlashWindup();
		}
	}

	private void EnterUpperSlashWindup()
	{
		phase = Phase.UpperSlashWindup;
		base.enemy.movement.MoveX(0f);
		base.enemy.animation.Play("upperSlashWindup");
	}

	private void TickUpperSlashWindup()
	{
		if (base.enemy.animation.IsDone)
		{
			EnterUpperSlash();
		}
	}

	private void EnterUpperSlash()
	{
		phase = Phase.UpperSlash;
		base.enemy.movement.velocity = new Vector2(moveDir * base.enemy.movement.upperSlashSpeedX, base.enemy.movement.upperSlashSpeedY);
		base.enemy.audio.PlaySlash();
		base.enemy.animation.Play("upperSlash");
	}

	private void TickUpperSlash()
	{
		if (base.enemy.animation.IsDone)
		{
			EnterFall();
		}
	}

	private void EnterFall()
	{
		phase = Phase.Fall;
		base.enemy.movement.MoveX(0f);
		base.enemy.animation.Play("fall");
	}

	private void TickFall()
	{
		if (base.enemy.physicsCheck.isGround)
		{
			base.sm.ChangeState(base.enemy.decisionState);
		}
	}

	public override void Exit()
	{
	}
}
