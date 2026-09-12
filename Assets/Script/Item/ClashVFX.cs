using UnityEngine;

public class ClashVFX : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
