using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[HideInInspector]
	public Player player;

	[HideInInspector]
	public AttackType currentAttackType;

	[Header("Pogo")]
	public float pogoForce = 12f;

	[Header("后坐力")]
	public float recoilSpeedX = 5f;

	public float recoilDuration = 0.1f;

	[Header("拼刀")]
	public GameObject clashVFX;

	private void Awake()
	{
		player = GetComponent<Player>();
	}

	private void Update()
	{
		if (player.isRecoiling)
		{
			player.recoilTimer -= Time.deltaTime;
			if (player.recoilTimer <= 0f)
			{
				player.isRecoiling = false;
			}
		}
	}

	public void BeginAttack(AttackType type)
	{
		currentAttackType = type;
	}

	public void EndAttack()
	{
		currentAttackType = AttackType.Horizontal;
	}

	public void Clash(Collider2D enemyAttack)
	{
		player.audio.PlayClash();
		if (clashVFX != null)
		{
			Vector3 position = (base.transform.position + enemyAttack.transform.position) / 2f;
			Object.Instantiate(clashVFX, position, Quaternion.identity);
		}
		EnemyMovement componentInParent = enemyAttack.GetComponentInParent<EnemyMovement>();
		if (componentInParent != null)
		{
			componentInParent.velocity = Vector2.zero;
		}
	}

	public void OnHit(Collider2D target)
	{
		switch (currentAttackType)
		{
		case AttackType.Horizontal:
			player.stats.GainEnergy(1);
			Recoil();
			break;
		case AttackType.Up:
			player.stats.GainEnergy(1);
			break;
		case AttackType.Down:
			player.stats.GainEnergy(1);
			Pogo();
			break;
		}
	}

	private void Recoil()
	{
		float x = player.transform.localScale.x;
		player.movement.velocity = new Vector2((0f - x) * recoilSpeedX, player.rb.linearVelocity.y);
		player.isRecoiling = true;
		player.recoilTimer = recoilDuration;
	}

	private void Pogo()
	{
		player.movement.velocity = new Vector2(player.rb.linearVelocity.x, pogoForce);
		player.canDashFlag = true;
		player.canDoubleJump = true;
	}
}

public enum AttackType
{
	Horizontal,
	Up,
	Down
}
