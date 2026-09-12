using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
	public Transform player;

	[Header("VFX")]
	public GameObject roarVFX;

	[Header("Shockwave")]
	public GameObject shockwavePrefab;

	public Transform castPoint;

	[Header("地刺")]
	public GameObject groundSpikePrefab;

	public Transform groundSpikePoint;

	public int spikeCount = 3;

	public float spikeSpacing = 2f;

	[Header("墙壁")]
	public float leftWallX = -33f;

	public float rightWallX = -15f;

	[HideInInspector]
	public float shockwaveDir;

	[HideInInspector]
	public Vector2 lastDamageSourcePos;

	[HideInInspector]
	public new EnemyAnimation animation;

	[HideInInspector]
	public EnemyMovement movement;

	[HideInInspector]
	public EnemyStats stats;

	[HideInInspector]
	public EnemyHitFlash hitFlash;

	[HideInInspector]
	public EnemyHitVFX hitVFX;

	[HideInInspector]
	public new EnemyAudio audio;

	[HideInInspector]
	public PhysicsCheck physicsCheck;

	[HideInInspector]
	public bool pendingStaggerExit;

	[HideInInspector]
	public Rigidbody2D rb;

	[HideInInspector]
	public Collider2D col;

	private int lastStaggerThreshold;

	public EnemyStateMachine sm { get; private set; }

	public EnemyIntroState introState { get; private set; }

	public EnemyBattleDecisionState decisionState { get; private set; }

	public EnemyMoveLoopState moveLoopState { get; private set; }

	public EnemyJumpLoopState jumpLoopState { get; private set; }

	public EnemyBackJumpLoopState backJumpLoopState { get; private set; }

	public EnemyStaggerState staggerState { get; private set; }

	public EnemyDeadState deadState { get; private set; }

	private void Awake()
	{
		animation = GetComponent<EnemyAnimation>();
		movement = GetComponent<EnemyMovement>();
		stats = GetComponent<EnemyStats>();
		hitFlash = GetComponent<EnemyHitFlash>();
		hitVFX = GetComponent<EnemyHitVFX>();
		audio = GetComponent<EnemyAudio>();
		physicsCheck = GetComponent<PhysicsCheck>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		sm = new EnemyStateMachine(this);
		introState = new EnemyIntroState(sm);
		decisionState = new EnemyBattleDecisionState(sm);
		moveLoopState = new EnemyMoveLoopState(sm);
		jumpLoopState = new EnemyJumpLoopState(sm);
		backJumpLoopState = new EnemyBackJumpLoopState(sm);
		staggerState = new EnemyStaggerState(sm);
		deadState = new EnemyDeadState(sm);
	}

	private void Start()
	{
		sm.Initialize(introState);
		lastStaggerThreshold = GetHPThreshold();
	}

	public void TakeDamage(int damage, GameObject source)
	{
		if (stats.IsDead)
		{
			return;
		}
		if (source != null)
		{
			lastDamageSourcePos = source.transform.position;
		}
		if (sm.currentState == staggerState)
		{
			pendingStaggerExit = true;
		}
		stats.TakeDamage(damage);
		hitFlash?.Flash();
		hitVFX?.Play(base.transform.position);
		audio?.PlayHit();
		if (stats.IsDead && sm.currentState != deadState)
		{
			sm.ChangeState(deadState);
		}
		else if (sm.currentState != staggerState)
		{
			int hPThreshold = GetHPThreshold();
			if (hPThreshold < lastStaggerThreshold)
			{
				lastStaggerThreshold = hPThreshold;
				sm.ChangeState(staggerState);
			}
		}
	}

	private int GetHPThreshold()
	{
		float num = (float)stats.currentHP / (float)stats.maxHP;
		if (num > 0.75f)
		{
			return 3;
		}
		if (num > 0.5f)
		{
			return 2;
		}
		if (num > 0.25f)
		{
			return 1;
		}
		return 0;
	}

	private void Update()
	{
		sm.Update();
	}

	public void PlayDeadSound()
	{
		audio?.PlayDead();
	}

	public void SpawnRoarVFX()
	{
		if (roarVFX != null)
		{
			Object.Instantiate(roarVFX, base.transform.position, Quaternion.identity);
		}
	}

	public void SpawnShockwave()
	{
		if (!(shockwavePrefab == null) && !(castPoint == null))
		{
			Object.Instantiate(shockwavePrefab, castPoint.position, Quaternion.identity).GetComponent<Shockwave>()?.Launch(shockwaveDir);
		}
	}

	private void FixedUpdate()
	{
		physicsCheck.Tick();
		sm.FixedUpdate();
	}
}
