using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
	public Door door;

	public CinemachineConfiner2D confiner;

	public Collider2D endBounds;

	private float timer;

	private float delay;

	private bool triggered;

	public static GameEndManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	public void TriggerRestart(float delaySeconds)
	{
		if (!triggered)
		{
			triggered = true;
			delay = delaySeconds;
			door?.Open();
			if (confiner != null && endBounds != null)
			{
				confiner.m_BoundingShape2D = endBounds;
			}
		}
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void Update()
	{
		if (triggered)
		{
			timer += Time.deltaTime;
			if (timer >= delay)
			{
				base.enabled = false;
				ScreenFader.Instance?.FadeOutAndRestart();
			}
		}
	}
}
