using UnityEngine;

public class Shockwave : MonoBehaviour
{
	public float speed = 8f;

	public float lifetime = 1.5f;

	public bool flip;

	private float dir;

	public void Launch(float direction)
	{
		dir = direction;
		base.transform.localScale = new Vector3(flip ? (0f - dir) : dir, 1f, 1f);
		Object.Destroy(base.gameObject, lifetime);
		CameraShakeManager.Instance?.StartContinuous(0.2f);
	}

	private void OnDestroy()
	{
		CameraShakeManager.Instance?.StopContinuous();
	}

	private void Update()
	{
		base.transform.Translate(Vector3.right * dir * speed * Time.deltaTime);
	}
}
