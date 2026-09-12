using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	public int maxHP = 30;

	public int currentHP;

	public bool IsDead => currentHP <= 0;

	public event Action<int> OnHPChanged;

	private void Awake()
	{
		currentHP = maxHP;
	}

	public void TakeDamage(int amount)
	{
		currentHP -= amount;
		currentHP = Mathf.Clamp(currentHP, 0, maxHP);
		this.OnHPChanged?.Invoke(currentHP);
	}
}
