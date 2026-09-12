using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
	[Header("暗黑冲刺")]
	public float blackDashCooldown = 3f;

	[Header("受伤")]
	public float hurtRecoilForce = 5f;

	public float invincibleDuration = 1.5f;

	[HideInInspector]
	public PlayerInput input;

	[HideInInspector]
	public PlayerMovement movement;

	[HideInInspector]
	public new PlayerAnimation animation;

	[HideInInspector]
	public PlayerStats stats;

	[HideInInspector]
	public PlayerCombat combat;

	[HideInInspector]
	public PlayerAbility ability;

	[HideInInspector]
	public PhysicsCheck physicsCheck;

	[HideInInspector]
	public Rigidbody2D rb;

	[HideInInspector]
	public Collider2D col;

	[HideInInspector]
	public PlayerHitFlash hitFlash;

	[HideInInspector]
	public new PlayerAudio audio;

	[HideInInspector]
	public Animator vfxAnimator;

	[HideInInspector]
	public bool movementLocked;

	[HideInInspector]
	public bool isRecoiling;

	[HideInInspector]
	public float recoilTimer;

	[HideInInspector]
	public bool canDashFlag = true;

	[HideInInspector]
	public bool canDoubleJump = true;

	[HideInInspector]
	public bool canBlackDash;

	[HideInInspector]
	public int attackIndex;

	[HideInInspector]
	public float blackDashTimer;

	[HideInInspector]
	public bool blackDashCharging;

	[HideInInspector]
	public bool isInvincible;

	private float invincibleTimer;

	[HideInInspector]
	public Vector2 lastDamageSourcePos;

	[HideInInspector]
	public bool pendingHurt;

	[HideInInspector]
	public bool pendingDead;

	[HideInInspector]
	public bool stunnedActive;

	public bool IsActing => actionSM.currentState != noneState;

	public bool IsDashing => movementSM.currentState == dashState;

	public bool IsHurt => actionSM.currentState == hurtState;

	public bool CanAttack
	{
		get
		{
			if (!IsDashing && !IsActing)
			{
				return !stats.IsDead;
			}
			return false;
		}
	}

	public bool CanCast
	{
		get
		{
			if (!IsActing && stats.currentEnergy >= 3)
			{
				return !stats.IsDead;
			}
			return false;
		}
	}

	public bool CanDash
	{
		get
		{
			if (canDashFlag && !IsActing)
			{
				return !stats.IsDead;
			}
			return false;
		}
	}

	public PlayerStateMachine movementSM { get; private set; }

	public PlayerStateMachine actionSM { get; private set; }

	public PlayerIdleState idleState { get; private set; }

	public PlayerMoveState moveState { get; private set; }

	public PlayerJumpState jumpState { get; private set; }

	public PlayerFallState fallState { get; private set; }

	public PlayerDashState dashState { get; private set; }

	public PlayerActionNoneState noneState { get; private set; }

	public PlayerActionAttackState attackState { get; private set; }

	public PlayerActionHealState healState { get; private set; }

	public PlayerActionShockwaveState shockwaveState { get; private set; }

	public PlayerActionUpRoarState upRoarState { get; private set; }

	public PlayerActionDiveState diveState { get; private set; }

	public PlayerActionHurtState hurtState { get; private set; }

	public PlayerActionStunnedState stunnedState { get; private set; }

	public PlayerActionDeadState deadState { get; private set; }

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
		movement = GetComponent<PlayerMovement>();
		animation = GetComponent<PlayerAnimation>();
		stats = GetComponent<PlayerStats>();
		combat = GetComponent<PlayerCombat>();
		ability = GetComponent<PlayerAbility>();
		physicsCheck = GetComponent<PhysicsCheck>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		hitFlash = GetComponent<PlayerHitFlash>();
		audio = GetComponent<PlayerAudio>();
		Animator[] componentsInChildren = GetComponentsInChildren<Animator>();
		vfxAnimator = ((componentsInChildren.Length > 1) ? componentsInChildren[1] : null);
		movementSM = new PlayerStateMachine(this);
		actionSM = new PlayerStateMachine(this);
		idleState = new PlayerIdleState(movementSM);
		moveState = new PlayerMoveState(movementSM);
		jumpState = new PlayerJumpState(movementSM);
		fallState = new PlayerFallState(movementSM);
		dashState = new PlayerDashState(movementSM);
		noneState = new PlayerActionNoneState(actionSM);
		attackState = new PlayerActionAttackState(actionSM);
		healState = new PlayerActionHealState(actionSM);
		shockwaveState = new PlayerActionShockwaveState(actionSM);
		upRoarState = new PlayerActionUpRoarState(actionSM);
		diveState = new PlayerActionDiveState(actionSM);
		hurtState = new PlayerActionHurtState(actionSM);
		stunnedState = new PlayerActionStunnedState(actionSM);
		deadState = new PlayerActionDeadState(actionSM);
	}

	private void Start()
	{
		movementSM.Initialize(idleState);
		actionSM.Initialize(noneState);
	}

	private void Update()
	{
		TickBlackDashCooldown();
		TickInvincibility();
		movementSM.Update();
		actionSM.Update();
	}

	private void FixedUpdate()
	{
		physicsCheck.Tick();
		movementSM.FixedUpdate();
		actionSM.FixedUpdate();
	}

	public void TakeDamage(int damage, GameObject source)
	{
		if (!isInvincible && !stats.IsDead)
		{
			if (source != null)
			{
				lastDamageSourcePos = source.transform.position;
			}
			stats.TakeDamage(damage);
			if (stats.IsDead)
			{
				pendingDead = true;
			}
			else
			{
				pendingHurt = true;
			}
		}
	}

	public void StartInvincibility()
	{
		isInvincible = true;
		invincibleTimer = invincibleDuration;
		hitFlash.StartFlash();
		SetEnemyCollision(ignore: true);
	}

	private void TickInvincibility()
	{
		if (isInvincible)
		{
			invincibleTimer -= Time.deltaTime;
			hitFlash.Tick();
			if (invincibleTimer <= 0f)
			{
				isInvincible = false;
				hitFlash.StopFlash();
				SetEnemyCollision(ignore: false);
			}
		}
	}

	private void SetEnemyCollision(bool ignore)
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), ignore);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), ignore);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), ignore);
	}

	public void RefreshMovementAnimation()
	{
		if (!IsActing)
		{
			State<Player> currentState = movementSM.currentState;
			if (currentState == idleState)
			{
				animation.Play("idle");
			}
			else if (currentState == moveState)
			{
				animation.Play("move");
			}
			else if (currentState == jumpState)
			{
				animation.Play("jump");
			}
			else if (currentState == fallState)
			{
				animation.Play("fall");
			}
		}
	}

	private void TickBlackDashCooldown()
	{
		if (blackDashCharging)
		{
			blackDashTimer += Time.deltaTime;
			if (blackDashTimer >= blackDashCooldown)
			{
				blackDashCharging = false;
				vfxAnimator?.Play("dashCharge", 0, 0f);
			}
		}
	}
}
