using UnityEngine;

public class RoarVFX : MonoBehaviour
{
	public float lifetime = 1f;

	private void Start()
	{
		Object.Destroy(base.gameObject, lifetime);
	}
}
