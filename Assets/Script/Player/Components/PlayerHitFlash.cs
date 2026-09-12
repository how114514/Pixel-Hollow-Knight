using UnityEngine;

public class PlayerHitFlash : MonoBehaviour
{
	public float flashInterval = 0.1f;

	public float flashAlpha = 0.3f;

	private SpriteRenderer sr;

	private float flashTimer;

	private bool active;

	private Color originalColor;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		originalColor = sr.color;
	}

	public void StartFlash()
	{
		active = true;
		flashTimer = flashInterval;
	}

	public void StopFlash()
	{
		active = false;
		sr.color = originalColor;
	}

	public void Tick()
	{
		if (active)
		{
			flashTimer -= Time.deltaTime;
			if (flashTimer <= 0f)
			{
				Color color = sr.color;
				color.a = (Mathf.Approximately(color.a, originalColor.a) ? flashAlpha : originalColor.a);
				sr.color = color;
				flashTimer = flashInterval;
			}
		}
	}
}
