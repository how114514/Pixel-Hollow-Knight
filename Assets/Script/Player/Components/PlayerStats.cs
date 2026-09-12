using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[Header("HP")]
	public int maxHP = 10;

	public int currentHP;

	[Header("Energy")]
	public int maxEnergy = 9;

	public int currentEnergy;

	public bool IsDead => currentHP <= 0;

	public event Action<int> OnHPChanged;

	public event Action<int> OnEnergyChanged;

	private void Start()
	{
		currentHP = maxHP;
	}

	public void TakeDamage(int damage)
	{
		if (!IsDead)
		{
			currentHP -= damage;
			currentHP = Mathf.Clamp(currentHP, 0, maxHP);
			this.OnHPChanged?.Invoke(currentHP);
		}
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);
		this.OnHPChanged?.Invoke(currentHP);
	}

	public void GainEnergy(int amount)
	{
		currentEnergy += amount;
		currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
		this.OnEnergyChanged?.Invoke(currentEnergy);
	}

	public bool TryConsumeEnergy(int amount)
	{
		if (currentEnergy < amount)
		{
			return false;
		}
		currentEnergy -= amount;
		this.OnEnergyChanged?.Invoke(currentEnergy);
		return true;
	}
}
