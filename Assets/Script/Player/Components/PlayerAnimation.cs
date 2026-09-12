using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private Animator animator;

	public bool IsDone => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void Play(string stateName)
	{
		animator.Play(stateName);
	}
}
