using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
	[HideInInspector]
	public Player player;

	[Header("Shockwave")]
	public GameObject shockwavePrefab;

	public Transform castPoint;

	[Header("UpRoar")]
	public GameObject sonicwavePrefab;

	public Transform sonicwavePoint;

	[Header("Dive")]
	public float diveRiseForce = 10f;

	public float diveForce = 15f;

	public GameObject groundwavePrefab;

	public Transform groundwavePoint;

	private void Awake()
	{
		player = GetComponent<Player>();
	}

	public void Execute(AbilityType type)
	{
		if (type != AbilityType.Heal)
		{
			_ = type - 1;
			_ = 1;
		}
		else
		{
			Heal();
		}
	}

	public void SpawnSonicwave()
	{
		Object.Instantiate(sonicwavePrefab, sonicwavePoint.position, Quaternion.identity);
	}

	public void SpawnShockwave()
	{
		Shockwave component = Object.Instantiate(shockwavePrefab, castPoint.position, Quaternion.identity).GetComponent<Shockwave>();
		float x = player.transform.localScale.x;
		component.Launch(x);
	}

	public void SpawnGroundwave()
	{
		Object.Instantiate(groundwavePrefab, groundwavePoint.position, Quaternion.identity);
	}

	private void Heal()
	{
		if (player.stats.TryConsumeEnergy(3))
		{
			player.stats.Heal(1);
		}
	}
}

public enum AbilityType
{
	Heal,
	Shockwave,
	UpRoar
}
