using UnityEngine;

public class DamageDealer : MonoBehaviour
{
	public int damage = 1;

	public LayerMask targetLayers;

	public LayerMask clashLayers;

	public LayerMask blockLayers;

	public bool useTrigger = true;

	public PlayerCombat combat;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (useTrigger)
		{
			DealDamage(other);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!useTrigger)
		{
			DealDamage(collision.collider);
		}
	}

	private void DealDamage(Collider2D other)
	{
		IDamageable component;
		if ((clashLayers.value & (1 << other.gameObject.layer)) != 0)
		{
			combat?.Clash(other);
		}
		else if ((targetLayers.value & (1 << other.gameObject.layer)) != 0 && !GetComponent<Collider2D>().IsTouchingLayers(blockLayers) && other.TryGetComponent<IDamageable>(out component))
		{
			component.TakeDamage(damage, base.gameObject);
			combat?.OnHit(other);
		}
	}
}
