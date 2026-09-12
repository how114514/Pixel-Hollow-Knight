using UnityEngine;

public class EnemyHitVFX : MonoBehaviour
{
	public GameObject bloodPrefab;

	public void Play(Vector3 position)
	{
		if (!(bloodPrefab == null))
		{
			Object.Instantiate(bloodPrefab, position, Quaternion.identity);
		}
	}
}
