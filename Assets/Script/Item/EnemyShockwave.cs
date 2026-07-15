using UnityEngine;

public class EnemyShockwave : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;

    float direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5f);
    }

    public void SetDirection(float dir)
    {
        direction = dir;

        if (dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Update()
    {
        
        rb.linearVelocity = new Vector2(speed * -direction, 0);
    }
}