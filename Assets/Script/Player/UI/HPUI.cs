using UnityEngine;

public class HPUI : MonoBehaviour
{
	public PlayerStats stats;

	public Animator animator;

	private void OnEnable()
	{
		if (stats != null)
		{
			stats.OnHPChanged += OnHPChanged;
		}
	}

	private void OnDisable()
	{
		if (stats != null)
		{
			stats.OnHPChanged -= OnHPChanged;
		}
	}

	private void OnHPChanged(int hp)
	{
		string state = $"hp{Mathf.Clamp(hp, 0, stats.maxHP)}";
		if (animator.HasState(0, Animator.StringToHash(state)))
		{
			animator.Play(state);
		}
	}
}
