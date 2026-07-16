using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyController : MonoBehaviour
{
    [Header("���")]
    public Animator anim;
    public Rigidbody2D rb;
    public Transform player;
    //public PhysicsCheck physicsCheck;
    public Collider2D col;
    public CameraManager cameraManager;
    public Collider2D endBounds;
    public AudioSource audioSource;

    [Header("�¼�����")]
    public VoidEventSO startEvent;

    [Header("�㲥")]
    public VoidEventSO playerDeafenedEvent;
    public VoidEventSO bossDeadEvent;
    public BoundsEventSO bossDeadBoundsEvent;

    [Header("��Ч")]
    public AudioClip staggerClip;
    public AudioClip deadExplosionStartClip;
    public AudioClip diveClip;
    public AudioClip chargeClip;
    public AudioClip battleRoarClip;
    public AudioClip SlashClip;
    public AudioClip deadExplosionClip;
    public AudioClip landClip;
    public AudioClip jumpClip;
    public AudioClip deadClip;
    public AudioClip shockwaveClip;

    [Header("����")]
    public float slashSpeed;
    public float uppercutSpeedX;
    public float uppercutSpeedY;
    public float moveSpeed;
    public float jumpSpeed;
    public float attackDistance;
    public float diveSpeed;
    public float minX;
    public float maxX;
    public float jumpBackSpeedX;
    public float jumpBackSpeedY;

    [Header("״̬")]
    public float facingDir;
    public bool fallByAttack;
    public bool isDiveAttackEnd;
    public bool isDiveStart;
    public bool isStartGame;

    [Header("�ش�")]
    public GameObject groundSpikePrefab;
    public Transform spikeSpawnPoint;
    public int spikeCount;
    public float spacing;
    public float spikeWaitTime;

    [Header("���")]
    public float chargeSpeed;
    public float chargeHeight;
    public bool isCharge;

    [Header("�����")]
    public GameObject shockwavePrefab;
    public Transform castPoint;

    [Header("Ѫ��")]
    public int maxHP;
    public int currentHP;
    public bool isDead;

    [Header("��ֱ")]
    public int[] staggerPoints;
    public int staggerIndex;
    public float staggerTimer;
    public float staggerTime;
    public bool wasInStagger;
    public float staggerRecoilX;
    public float staggerRecoilY;
    public Transform playerPos;

    [Header("ս��")]
    public GameObject battleCryVFXPrefab;
    public Transform battleCryVFXPoint;

    public EnemyStateMachine stateMachine;

    public EnemySlashStartState slashStartState;
    public EnemySlashAttackState slashAttackState;
    public EnemyUppercutStartState uppercutStartState;
    public EnemyUppercutAttackState uppercutAttackState;
    public EnemyMoveState moveState;
    public EnemyJumpState jumpState;
    public EnemyFallState fallState;
    public EnemyDiveStartState diveStartState;
    public EnemyDiveAttackState diveAttackState;
    public EnemyDiveEndState diveEndState;
    public EnemyBackJumpState backJumpState;
    public EnemyBackFallState backFallState;
    public EnemyCastShockwaveState castShockwaveState;
    public EnemyChargeStartState chargeStartState;
    public EnemyChargeState chargeState;
    public EnemyChargeEndState chargeEndState;
    public EnemyDeadStartState deadStartState;
    public EnemyDeadState deadState;
    public EnemyStaggerState staggerState;
    public EnemyIdleState idleState;
    public EnemyBattleCryStartState battleCryStartState;
    public EnemyBattleCryState battleCryState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //physicsCheck = GetComponent<PhysicsCheck>();
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        facingDir = Mathf.Sign(transform.localScale.x);

        stateMachine = new EnemyStateMachine();

        slashStartState = new EnemySlashStartState(this, stateMachine);
        slashAttackState = new EnemySlashAttackState(this, stateMachine);
        uppercutStartState = new EnemyUppercutStartState(this, stateMachine);
        uppercutAttackState = new EnemyUppercutAttackState(this, stateMachine);
        moveState = new EnemyMoveState(this, stateMachine);
        jumpState = new EnemyJumpState(this, stateMachine);
        fallState = new EnemyFallState(this, stateMachine);
        diveStartState = new EnemyDiveStartState(this, stateMachine);
        diveAttackState = new EnemyDiveAttackState(this, stateMachine);
        diveEndState = new EnemyDiveEndState(this, stateMachine);
        backJumpState = new EnemyBackJumpState(this, stateMachine);
        backFallState = new EnemyBackFallState(this, stateMachine);
        castShockwaveState = new EnemyCastShockwaveState(this, stateMachine);
        chargeStartState = new EnemyChargeStartState(this, stateMachine);
        chargeState = new EnemyChargeState(this, stateMachine);
        chargeEndState = new EnemyChargeEndState(this, stateMachine);
        deadStartState = new EnemyDeadStartState(this, stateMachine);
        deadState = new EnemyDeadState(this, stateMachine);
        staggerState = new EnemyStaggerState(this, stateMachine);
        idleState = new EnemyIdleState(this, stateMachine);
        battleCryStartState = new EnemyBattleCryStartState(this, stateMachine);
        battleCryState = new EnemyBattleCryState(this, stateMachine);
    }

    private void OnEnable()
    {
        startEvent.OnEventRaised += ChangeToBattleCryStart;
    }

    private void OnDisable()
    {
        startEvent.OnEventRaised -= ChangeToBattleCryStart;
    }

    private void Start()
    {
        currentHP = maxHP;

        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.Update();

        ChangeToDiveEnd();

        FallFaster();
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void FacePlayer()
    {
        float dir = player.position.x - transform.position.x;

        if (dir > 0 && facingDir == 1)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingDir *= -1;
        }
        else if (dir < 0 && facingDir == -1)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingDir *= -1;
        }
    }

    public IEnumerator SpawnSpikeRoutine()
    {
        for (int i = 1; i <= spikeCount; i++)
        {
            float rightX = spikeSpawnPoint.position.x + i * spacing;
            float leftX = spikeSpawnPoint.position.x - i * spacing;

            if (rightX >= minX && rightX <= maxX)
            {
                Vector2 rightPos = new Vector2(rightX, spikeSpawnPoint.position.y + 3);
                Instantiate(groundSpikePrefab, rightPos, Quaternion.identity);
            }

            if (leftX >= minX && leftX <= maxX)
            {
                Vector2 leftPos = new Vector2(leftX, spikeSpawnPoint.position.y + 3);
                Instantiate(groundSpikePrefab, leftPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(spikeWaitTime);
        }
    }

    public void CastShockwave()
    {
        GameObject shockwave = Instantiate(shockwavePrefab, castPoint.position, Quaternion.identity);

        EnemyShockwave shockwaveScript = shockwave.GetComponent<EnemyShockwave>();

        shockwaveScript.SetDirection(facingDir);

        cameraManager.Shake();
    }

    public void SpawnBattleCryVFX()
    {
        GameObject battleCryVFX = Instantiate(battleCryVFXPrefab, battleCryVFXPoint.position, Quaternion.identity);
    }

    private void FallFaster()
    {
        if ((!isCharge) || (!isDiveStart))
        {
            if (rb.linearVelocity.y < -0.1)
            {
                rb.gravityScale = 4;
            }
            else
            {
                rb.gravityScale = 2;
            }
        }
    }

    public void CameraShake()
    {
        cameraManager.Shake();
    }

    public void TakeDamage(int damage)
    {
        wasInStagger = stateMachine.currentState is EnemyStaggerState;
        int oldHP = currentHP;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (staggerIndex < staggerPoints.Length && oldHP > staggerPoints[staggerIndex] && currentHP <= staggerPoints[staggerIndex])
        {
            stateMachine.ChangeState(staggerState);
            staggerIndex++;
        }

        if (wasInStagger)
        {
            stateMachine.ChangeState(moveState);
            return;
        }

        if (currentHP <= 0 && !isDead)
        {
            stateMachine.ChangeState(deadStartState);
        }
    }

    public void BossDead()
    {
        bossDeadEvent.RaiseEvent();
        bossDeadBoundsEvent.RaiseEvent(endBounds);
    }

    public void PlayerDeafened()
    {
        playerDeafenedEvent.RaiseEvent();
    }

    private void ChangeToBattleCryStart()
    {
        stateMachine.ChangeState(battleCryStartState);
    }

    public void SlashStartEnd()
    {
        if (stateMachine.currentState is EnemySlashStartState)
        {
            stateMachine.ChangeState(slashAttackState);
        }
    }

    public void SlashAttackEnd()
    {
        if (stateMachine.currentState is EnemySlashAttackState)
        {
            stateMachine.ChangeState(uppercutStartState);
        }
    }

    public void UppercutStartEnd()
    {
        if (stateMachine.currentState is EnemyUppercutStartState)
        {
            stateMachine.ChangeState(uppercutAttackState);
        }
    }

    public void UppercutAttackEnd()
    {
        if (stateMachine.currentState is EnemyUppercutAttackState)
        {
            stateMachine.ChangeState(fallState);
        }
    }

    public void DiveStartEnd()
    {
        if (stateMachine.currentState is EnemyDiveStartState)
        {
            stateMachine.ChangeState(diveAttackState);
        }
    }

    public void DiveAttackEnd()
    {
        if (stateMachine.currentState is EnemyDiveAttackState)
        {
            isDiveAttackEnd = true;
        }
    }

    public void DiveEndEnd()
    {
        if (stateMachine.currentState is EnemyDiveEndState)
        {
            stateMachine.ChangeState(slashStartState);
        }
    }

    public void CastShockwaveEnd()
    {
        if (stateMachine.currentState is EnemyCastShockwaveState)
        {
            stateMachine.ChangeState(jumpState);
        }
    }

    public void ChargeStartEnd()
    {
        if (stateMachine.currentState is EnemyChargeStartState)
        {
            stateMachine.ChangeState(chargeState);
        }
    }

    public void ChargeEndEnd()
    {
        if (stateMachine.currentState is EnemyChargeEndState)
        {
            stateMachine.ChangeState(moveState);
        }
    }

    public void DeadStartEnd()
    {
        if (stateMachine.currentState is EnemyDeadStartState)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    private void ChangeToDiveEnd()
    {
        /*if ((isDiveAttackEnd) && (physicsCheck.isGround) && (!isDead))
        {
            stateMachine.ChangeState(diveEndState);
        }*/
    }

    public void BattleCryEnd()
    {
        if (stateMachine.currentState is EnemyBattleCryState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void IdleEnd()
    {
        if (stateMachine.currentState is EnemyIdleState)
        {
            stateMachine.ChangeState(moveState);
        }
    }

    public void HurtShake()
    {

        PlaySFX(deadExplosionClip);
        cameraManager.Shake();
        StartCoroutine(HitStopRoutine());
    }

    private IEnumerator HitStopRoutine()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
    }

    public void Knockback(Transform player)
    {
        float dir = transform.position.x - player.position.x;

        rb.linearVelocity = new Vector2(Mathf.Sign(dir) * staggerRecoilX, staggerRecoilY);
    }
}
