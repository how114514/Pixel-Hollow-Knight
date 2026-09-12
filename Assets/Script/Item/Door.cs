using UnityEngine;

public class Door : MonoBehaviour
{
	public AudioClip openClip;

	public AudioClip closeClip;

	private Animator animator;

	private AudioSource source;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}

	public void Open()
	{
		source.PlayOneShot(openClip);
		animator.Play("open");
	}

	public void Close()
	{
		source.PlayOneShot(closeClip);
		animator.Play("close");
	}
}
