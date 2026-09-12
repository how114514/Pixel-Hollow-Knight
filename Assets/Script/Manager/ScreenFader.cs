using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
	public Image fadeImage;

	public float fadeDuration = 1f;

	public static ScreenFader Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		fadeImage.gameObject.SetActive(value: false);
	}

	public void FadeOutAndRestart()
	{
		fadeImage.gameObject.SetActive(value: true);
		fadeImage.color = new Color(0f, 0f, 0f, 0f);
		fadeImage.DOFade(1f, fadeDuration).OnComplete(delegate
		{
			GameEndManager.Instance?.RestartScene();
		});
	}
}
