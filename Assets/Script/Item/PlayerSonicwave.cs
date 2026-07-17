using UnityEngine;

/// <summary>
/// 上吼声波。播放动画，播完自动销毁。
/// </summary>
public class PlayerSonicwave : MonoBehaviour
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
            Destroy(gameObject);
        }
    }
}
