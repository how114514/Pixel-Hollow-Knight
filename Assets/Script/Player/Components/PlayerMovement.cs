using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("移动")]
	public float moveSpeed = 5f;

	[Header("跳跃")]
	public float jumpForce = 10f;

	public float doubleJumpForce = 8f;

	[Header("冲刺")]
	public float dashSpeed = 12f;

	[Header("重力")]
	public float normalGravity = 2f;

	public float fallGravity = 4f;

	public float fallThreshold = -0.5f;

	[HideInInspector]
	public bool gravityLocked;

	private Rigidbody2D rb;

	public Vector2 velocity
	{
		get
		{
			return rb.linearVelocity;
		}
		set
		{
			rb.linearVelocity = value;
		}
	}

	public float gravityScale
	{
		get
		{
			return rb.gravityScale;
		}
		set
		{
			rb.gravityScale = value;
		}
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (!gravityLocked)
		{
			rb.gravityScale = ((rb.linearVelocity.y < fallThreshold) ? fallGravity : normalGravity);
		}
	}

	public void MoveX(float speed)
	{
		rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
	}

	public void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
	{
		rb.AddForce(force, mode);
	}
}
