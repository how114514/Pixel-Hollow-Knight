using UnityEngine;

/// <summary>
/// 冲击波弹体。由 PlayerAbility 实例化。
/// </summary>
public class PlayerShockwave : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 1.5f;
    private float dir;

    public void Launch(float direction)
    {
        dir = direction;
        transform.localScale = new Vector3(dir, 1, 1);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * dir * speed * Time.deltaTime);
    }
}
