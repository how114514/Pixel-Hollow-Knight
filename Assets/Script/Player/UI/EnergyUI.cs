using UnityEngine;

public class EnergyUI : MonoBehaviour
{
	public PlayerStats stats;

	public Animator animator;

	private void OnEnable()
	{
		if (stats != null)
		{
			stats.OnEnergyChanged += OnEnergyChanged;
		}
	}

	private void OnDisable()
	{
		if (stats != null)
		{
			stats.OnEnergyChanged -= OnEnergyChanged;
		}
	}

	private void OnEnergyChanged(int energy)
	{
		string state = $"energy{Mathf.Clamp(energy, 0, stats.maxEnergy)}";
		if (animator.HasState(0, Animator.StringToHash(state)))
		{
			animator.Play(state);
		}
	}
}
