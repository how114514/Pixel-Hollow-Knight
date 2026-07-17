using UnityEngine;

/// <summary>
/// 下砸地面波。播动画，播完自动销毁。
/// </summary>
public class PlayerGroundwave : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            Destroy(gameObject);
    }
}
