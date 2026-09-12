using UnityEngine;

public class EnemyJumpLoopState : EnemyState
{
	private enum Phase
	{
		Jump,
		Fall,
		SlashWindup,
		Slash,
		UpperSlashWindup,
		UpperSlash,
		DiveWindup,
		Diving,
		DiveRecover
	}

	private const float DiveChance = 0.4f;

	private Phase phase;

	private float moveDir;

	private bool isDiveBranch;

	public EnemyJumpLoopState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
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
		case Phase.DiveWindup:
			TickDiveWindup();
			break;
		case Phase.Diving:
			TickDiving();
			break;
		case Phase.DiveRecover:
			TickDiveRecover();
			break;
		}
	}

	private void EnterJump()
	{
		phase = Phase.Jump;
		isDiveBranch = Random.value < 0.4f;
		if (isDiveBranch)
		{
			base.enemy.movement.JumpAbove(base.enemy.player.position);
		}
		else
		{
			base.enemy.movement.JumpToward(base.enemy.player.position);
		}
		base.enemy.audio.PlayJump();
		base.enemy.animation.Play("jump");
	}

	private void TickJump()
	{
		if (base.enemy.rb.linearVelocity.y <= 0f)
		{
			if (isDiveBranch)
			{
				EnterDiveWindup();
			}
			else
			{
				EnterFall();
			}
		}
	}

	private void EnterFall()
	{
		phase = Phase.Fall;
		base.enemy.animation.Play("fall");
	}

	private void TickFall()
	{
		if (base.enemy.physicsCheck.isGround)
		{
			base.enemy.movement.MoveX(0f);
			base.enemy.audio.PlayLand();
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
			base.sm.ChangeState(base.enemy.decisionState);
		}
	}

	private void EnterDiveWindup()
	{
		phase = Phase.DiveWindup;
		base.enemy.movement.velocity = Vector2.zero;
		base.enemy.movement.gravityLocked = true;
		base.enemy.movement.gravityScale = 0f;
		base.enemy.animation.Play("diveWindup");
	}

	private void TickDiveWindup()
	{
		if (base.enemy.animation.IsDone)
		{
			EnterDiving();
		}
	}

	private void EnterDiving()
	{
		phase = Phase.Diving;
		base.enemy.movement.gravityLocked = false;
		base.enemy.movement.gravityScale = base.enemy.movement.normalGravity;
		base.enemy.movement.velocity = new Vector2(0f, 0f - base.enemy.movement.diveForce);
		base.enemy.animation.Play("diving");
	}

	private void TickDiving()
	{
		base.enemy.movement.velocity = new Vector2(0f, 0f - base.enemy.movement.diveForce);
		if (base.enemy.physicsCheck.isGround)
		{
			EnterDiveRecover();
		}
	}

	private void EnterDiveRecover()
	{
		phase = Phase.DiveRecover;
		base.enemy.movement.MoveX(0f);
		base.enemy.audio.PlayDiveLand();
		CameraShakeManager.Instance?.Shake(0.5f);
		if (base.enemy.groundSpikePrefab != null)
		{
			SpawnSpikes();
		}
		base.enemy.animation.Play("diveRecover");
	}

	private void SpawnSpikes()
	{
		float x = base.enemy.transform.position.x;
		for (int i = 1; i <= base.enemy.spikeCount; i++)
		{
			TrySpawnSpike(x - base.enemy.spikeSpacing * (float)i);
			TrySpawnSpike(x + base.enemy.spikeSpacing * (float)i);
		}
	}

	private void TrySpawnSpike(float x)
	{
		if (!(x < base.enemy.leftWallX) && !(x > base.enemy.rightWallX))
		{
			Vector3 position = new Vector3(x, (base.enemy.groundSpikePoint != null) ? base.enemy.groundSpikePoint.position.y : base.enemy.transform.position.y, 0f);
			Object.Instantiate(base.enemy.groundSpikePrefab, position, Quaternion.identity);
		}
	}

	private void TickDiveRecover()
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
