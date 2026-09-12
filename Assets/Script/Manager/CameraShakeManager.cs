using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
	private CinemachineImpulseSource impulse;

	private Coroutine shakeRoutine;

	private int shakeCount;

	public static CameraShakeManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		impulse = GetComponent<CinemachineImpulseSource>();
	}

	public void Shake(float intensity)
	{
		impulse.GenerateImpulse(intensity);
	}

	public void StartContinuous(float intensity)
	{
		shakeCount++;
		if (shakeRoutine == null)
		{
			shakeRoutine = StartCoroutine(ContinuousShake(intensity));
		}
	}

	public void StopContinuous()
	{
		shakeCount--;
		if (shakeCount <= 0 && shakeRoutine != null)
		{
			StopCoroutine(shakeRoutine);
			shakeRoutine = null;
		}
	}

	private IEnumerator ContinuousShake(float intensity)
	{
		while (true)
		{
			impulse.GenerateImpulse(intensity);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
