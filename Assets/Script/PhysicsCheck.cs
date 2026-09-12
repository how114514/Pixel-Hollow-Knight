using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
	[Header("地面")]
	public float checkRadius = 0.2f;

	public Vector2 bottomOffset;

	public LayerMask groundMask;

	[Header("墙壁")]
	public float wallRadius = 0.2f;

	public Vector2 leftOffset;

	public Vector2 rightOffset;

	[Header("材质")]
	public PhysicsMaterial2D normalMaterial;

	public PhysicsMaterial2D wallMaterial;

	[HideInInspector]
	public bool isGround;

	[HideInInspector]
	public bool touchLeftWall;

	[HideInInspector]
	public bool touchRightWall;

	private CapsuleCollider2D col;

	private void Awake()
	{
		col = GetComponent<CapsuleCollider2D>();
	}

	public void Tick()
	{
		Vector2 vector = base.transform.position;
		isGround = Physics2D.OverlapCircle(vector + bottomOffset, checkRadius, groundMask);
		touchLeftWall = Physics2D.OverlapCircle(vector + leftOffset, wallRadius, groundMask);
		touchRightWall = Physics2D.OverlapCircle(vector + rightOffset, wallRadius, groundMask);
		col.sharedMaterial = (isGround ? normalMaterial : wallMaterial);
	}

	private void OnDrawGizmosSelected()
	{
		Vector2 vector = base.transform.position;
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(vector + bottomOffset, checkRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(vector + leftOffset, wallRadius);
		Gizmos.DrawWireSphere(vector + rightOffset, wallRadius);
	}
}
