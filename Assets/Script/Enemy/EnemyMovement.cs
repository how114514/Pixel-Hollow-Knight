using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[Header("移动")]
	public float moveSpeed = 3f;

	public float stopDistance = 2f;

	public float jumpDistance = 5f;

	[Header("跳跃")]
	public float jumpArcHeight = 3f;

	[Header("Slash 冲刺")]
	public float slashSpeed = 6f;

	[Header("UpperSlash 冲刺")]
	public float upperSlashSpeedX = 5f;

	public float upperSlashSpeedY = 8f;

	[Header("Dive")]
	public float diveForce = 15f;

	[Header("Charge 冲刺")]
	public float chargeSpeed = 10f;

	public float chargeHeight = 2f;

	[Header("重力")]
	public float normalGravity = 2f;

	public float fallGravity = 4f;

	public float fallThreshold = -0.5f;

	[HideInInspector]
	public bool gravityLocked;

	private Rigidbody2D rb;

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

	public float FaceTarget(Vector3 targetPos)
	{
		float num = ((targetPos.x - base.transform.position.x > 0f) ? 1 : (-1));
		base.transform.localScale = new Vector3(0f - num, 1f, 1f);
		return num;
	}

	public void JumpToward(Vector3 targetPos)
	{
		float num = targetPos.x - base.transform.position.x;
		float num2 = Mathf.Abs(Physics2D.gravity.y) * normalGravity;
		float num3 = Mathf.Sqrt(2f * num2 * jumpArcHeight);
		float x = num * num2 / (2f * num3);
		float num4 = ((num > 0f) ? 1 : (-1));
		base.transform.localScale = new Vector3(0f - num4, 1f, 1f);
		velocity = new Vector2(x, num3);
	}

	public void JumpAbove(Vector3 targetPos)
	{
		float num = targetPos.x - base.transform.position.x;
		float num2 = Mathf.Abs(Physics2D.gravity.y) * normalGravity;
		float num3 = Mathf.Sqrt(2f * num2 * jumpArcHeight);
		float x = num * num2 / num3;
		float num4 = ((num > 0f) ? 1 : (-1));
		base.transform.localScale = new Vector3(0f - num4, 1f, 1f);
		velocity = new Vector2(x, num3);
	}
}
