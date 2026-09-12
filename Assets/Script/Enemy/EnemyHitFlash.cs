using System.Collections;
using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{
	public float flashDuration = 0.1f;

	public Color flashColor = Color.red;

	private SpriteRenderer sr;

	private Color originalColor;

	private Coroutine flashRoutine;

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		originalColor = sr.color;
	}

	public void Flash()
	{
		if (flashRoutine != null)
		{
			StopCoroutine(flashRoutine);
		}
		flashRoutine = StartCoroutine(FlashRoutine());
	}

	private IEnumerator FlashRoutine()
	{
		sr.color = flashColor;
		yield return new WaitForSeconds(flashDuration);
		sr.color = originalColor;
	}
}
