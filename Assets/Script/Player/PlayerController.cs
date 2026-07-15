using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;

    [Header("���")]
    public PhysicsCheck physicsCheck;
    public Animator anim;
    public Rigidbody2D rb;
    public CapsuleCollider2D coll;
    public GameObject vfxObject;
    private Animator vfxAnim;
    public SpriteRenderer sprite;
    public CameraManager cameraManager;
    public AudioSource audioSource;

    [Header("����")]
    public VoidEventSO playerDefenedEvent;

    [Header("����")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;

    [Header("��Ч")]
    public AudioClip attack1Clip;
    public AudioClip attack2Clip;
    public AudioClip attackUpClip;
    public AudioClip attackDownClip;
    public AudioClip clash1Clip;
    public AudioClip clash2Clip;
    public AudioClip upRoarClip;
    public AudioClip downSmashClip;
    public AudioClip downSmashLandClip;
    public AudioClip doubleJumpClip;
    public AudioClip dashClip;
    public AudioClip healedClip;
    public AudioClip healClip;
    public AudioClip hurtClip;
    public AudioClip blackDashClip;
    public AudioClip blackDashCDClip;
    public AudioClip castShockwaveClip;

    [Header("�ƶ�����")]
    public float moveSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float doubleJumpSpeed;

    [Header("�ڳ��ʱ")]
    public float blackDashCD;
    public float blackDashCDTimer;
    public bool blackDashCDEnd;
    public bool canBlackDash;

    [Header("������")]
    public float recoilSpeedX;
    public float recoilSpeedY;

    [Header("�޵�")]
    public float invincibleTime;
    public float invincibleTimer;
    public bool isInvincible;
    public float flashInterval;
    private float flashTimer;

    [Header("���˻���")]
    public float hurtRecoilX;
    public float hurtRecoilY;
    public bool isHurt;

    [Header("״̬")]
    public bool canDash;
    public bool canDoubleJump;
    public int attackIndex;
    public bool isAttacking;
    public bool isDashing;
    public bool isDead;
    public bool isSkill;

    [Header("Ѫ��")]
    public int maxHP;
    public int currentHp;
    public HPUI hpUI;
    public bool isPressedHeal;

    [Header("����")]
    public int maxEnergy;
    public int currentEnergy;
    public EnergyUI energyUI;

    [Header("�����")]
    public GameObject shockwavePrefab;
    public Transform castPoint;

    [Header("����")]
    public GameObject sonicwavePrefab;
    public Transform sonicwavePoint;

    [Header("����")]
    public GameObject groundwavePrefab;
    public Transform groundwavePoint;

    [Header("ƴ��")]
    public GameObject clashEffectPrefab;

    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    public PlayerDashState dashState;
    public PlayerDoubleJumpState doubleJumpState;
    public PlayerAttackState attackState;
    public PlayerHurtState hurtState;
    public PlayerBlackDashState blackDashState;
    public PlayerHealState healState;
    public PlayerHealedState healedState;
    public PlayerDeadStartState deadStartState;
    public PlayerDeadState deadState;
    public PlayerCastShockwaveState castShockwaveState;
    public PlayerUpRoarState upRoarState;
    public PlayerDownSmashStartState downSmashStartState;
    public PlayerDownSmashState downSmashState;
    public PlayerDownSmashEndState downSmashEndState;
    public PlayerDeafenedState deafenedState;


    private void Awake()
    {
        inputControl = new PlayerInputControl();

        physicsCheck = GetComponent<PhysicsCheck>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        vfxAnim = vfxObject.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine);
        moveState = new PlayerMoveState(this, stateMachine);
        jumpState = new PlayerJumpState(this, stateMachine);
        fallState = new PlayerFallState(this, stateMachine);
        dashState = new PlayerDashState(this, stateMachine);
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine);
        attackState = new PlayerAttackState(this, stateMachine);
        hurtState = new PlayerHurtState(this, stateMachine);
        blackDashState = new PlayerBlackDashState(this, stateMachine);
        healState = new PlayerHealState(this, stateMachine);
        healedState = new PlayerHealedState(this, stateMachine);
        deadStartState = new PlayerDeadStartState(this, stateMachine);
        deadState = new PlayerDeadState(this, stateMachine);
        castShockwaveState = new PlayerCastShockwaveState(this, stateMachine);
        upRoarState = new PlayerUpRoarState(this, stateMachine);
        downSmashStartState = new PlayerDownSmashStartState(this, stateMachine);
        downSmashState = new PlayerDownSmashState(this, stateMachine);
        downSmashEndState = new PlayerDownSmashEndState(this, stateMachine);
        deafenedState = new PlayerDeafenedState(this, stateMachine);
    }

    private void OnEnable()
    {
        inputControl.Enable();
        playerDefenedEvent.OnEventRaised += ChangeToDeafened;
        inputControl.GamePlay.ReStart.started += ReStartLever;
    }

    private void OnDisable()
    {
        inputControl.Disable();
        playerDefenedEvent.OnEventRaised -= ChangeToDeafened;
        inputControl.GamePlay.ReStart.started += ReStartLever;
    }

    private void Start()
    {
        currentHp = maxHP;
        hpUI.SetHP(currentHp);
        currentEnergy = 0;
        energyUI.SetEnergy(currentEnergy);

        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.Update();

        RecoveryAbilityOnGround();

        CheakState();

        BlackDashCD();

        Invincible();

        FallFaster();

        ChangeToDeadStart();
    }

    private void ReStartLever(InputAction.CallbackContext context)
    {
        ReStart();
    }

    private void ChangeToDeafened()
    {
        stateMachine.ChangeState(deafenedState);
    }

    public void AttackEnd()
    {
        if (stateMachine.currentState is PlayerAttackState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void BlackDashEnd()
    {
        if (stateMachine.currentState is PlayerBlackDashState)
        {
            stateMachine.ChangeState(fallState);
        }
    }

    public void DashEnd()
    {
        if (stateMachine.currentState is PlayerDashState)
        {
            stateMachine.ChangeState(fallState);
        }
    }

    public void HurtEnd()
    {
        if (stateMachine.currentState is PlayerHurtState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void HealEnd()
    {
        if (stateMachine.currentState is PlayerHealState)
        {
            if (currentEnergy >= 3)
            {
                currentEnergy -= 3;
                currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
                energyUI.SetEnergy(currentEnergy);

                currentHp++;
                currentHp = Mathf.Clamp(currentHp, 0, maxHP);
                hpUI.SetHP(currentHp);

                stateMachine.ChangeState(healedState);

            }
            else
            {
                stateMachine.ChangeState(idleState);
            }
        }
    }

    public void HealedEnd()
    {
        if (stateMachine.currentState is PlayerHealedState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void CastShockwaveEnd()
    {
        if (stateMachine.currentState is PlayerCastShockwaveState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void DownSmashStartEnd()
    {
        if (stateMachine.currentState is PlayerDownSmashStartState)
        {
            stateMachine.ChangeState(downSmashState);
        }
    }

    public void DownSmashEndEnd()
    {
        if (stateMachine.currentState is PlayerDownSmashEndState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void SpawnGroundwave()
    {
        Vector3 spawnPos = groundwavePoint.position + new Vector3(0, 0, 0);

        GameObject groundwave = Instantiate(groundwavePrefab, spawnPos, Quaternion.identity);

        PlayerGroundwave GroundwaveScript = groundwave.GetComponent<PlayerGroundwave>();

        cameraManager.Shake();
    }

    public void SpawnSonicwave()
    {
        currentEnergy -= 3;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energyUI.SetEnergy(currentEnergy);

        Vector3 spawnPos = sonicwavePoint.position + new Vector3(-0.1f, 2f, 0f);

        GameObject sonicwave = Instantiate(sonicwavePrefab, spawnPos, Quaternion.identity);

        PlayerSonicwave sonicwaveScript = sonicwave.GetComponent<PlayerSonicwave>();

        cameraManager.Shake();
    }

    public void CastShockwave()
    {
        currentEnergy -= 3;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energyUI.SetEnergy(currentEnergy);

        GameObject shockwave = Instantiate(shockwavePrefab, castPoint.position, Quaternion.identity);

        PlayerShockwave shockwaveScript = shockwave.GetComponent<PlayerShockwave>();

        shockwaveScript.SetDirection(Mathf.Sign(transform.localScale.x));

        cameraManager.Shake();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBody") && !isHurt && !isDead)
        {
            currentHp--;
            currentHp = Mathf.Clamp(currentHp, 0, maxHP);
            hpUI.SetHP(currentHp);

            stateMachine.ChangeState(hurtState);
            Knockback(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            if (collision.IsTouchingLayers(1 << LayerMask.NameToLayer("PlayerAttack")))
            {
                Vector3 clashPos = (transform.position + collision.transform.position) / 2f;
                Instantiate(clashEffectPrefab, clashPos, Quaternion.identity);
                cameraManager.Shake();

                Rigidbody2D enemyRb = collision.GetComponentInParent<Rigidbody2D>();
                enemyRb.linearVelocity = Vector2.zero;

                float r = UnityEngine.Random.value;

                if (r < 0.5f)
                {
                    PlaySFX(clash1Clip);
                }
                else
                {
                    PlaySFX(clash2Clip);
                }

                return;
            }
        }

        if ((collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") || (collision.gameObject.layer == LayerMask.NameToLayer("EnemySkill"))) && !isHurt && !isDead)
        {
            if (collision.IsTouchingLayers(1 << LayerMask.NameToLayer("PlayerBody")))
            {
                currentHp--;
                currentHp = Mathf.Clamp(currentHp, 0, maxHP);
                hpUI.SetHP(currentHp);

                stateMachine.ChangeState(hurtState);
                Knockback(collision.transform);
                return;
            }
        }
    }

    private void ChangeToDeadStart()
    {
        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            stateMachine.ChangeState(deadStartState);
        }
    }

    public void UpRoarEnd()
    {
        if (stateMachine.currentState is PlayerUpRoarState)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    public void DeadStartEnd()
    {
        if (stateMachine.currentState is PlayerDeadStartState)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    private void RecoveryAbilityOnGround()
    {
        if (physicsCheck.isGround)
        {
            canDash = true;
            canDoubleJump = true;
        }
    }

    public void RecoveryAbility()
    {
        canDash = true;
        canDoubleJump = true;
    }

    private void CheakState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }

    private void BlackDashCD()
    {
        if (blackDashCDEnd)
            return;

        blackDashCDTimer += Time.deltaTime;

        if (blackDashCDTimer >= blackDashCD)
        {
            blackDashCDEnd = true;
            vfxAnim.Play("dashCharge");
        }
    }

    public void BlackDashCDFinished()
    {
        canBlackDash = true;
        PlaySFX(blackDashCDClip);
    }

    public void Invincible()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0)
            {
                sprite.enabled = !sprite.enabled;
                flashTimer = flashInterval;
            }

            if (invincibleTimer <= 0)
            {
                isInvincible = false;

                sprite.enabled = true;

                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), false);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), false);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), false);
            }
        }
    }

    public void Knockback(Transform enemy)
    {
        float dir = transform.position.x - enemy.position.x;

        rb.linearVelocity = new Vector2(Mathf.Sign(dir) * hurtRecoilX, hurtRecoilY);
    }

    private void FallFaster()
    {
        if (!isDashing)
        {
            if (rb.linearVelocity.y < 0)
            {
                rb.gravityScale = 4;
            }
            else
            {
                rb.gravityScale = 2;
            }
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}