using UnityEngine;

public class BloodVFX : MonoBehaviour
{
	public float lifetime = 0.5f;

	private void Start()
	{
		Object.Destroy(base.gameObject, lifetime);
	}
}
