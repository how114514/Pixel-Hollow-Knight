using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header ("µÿ√Ê")]
    public Vector2 bottomOffset;
    public float cheakRaduis;
    public LayerMask groundMask;

    [HideInInspector]public bool isGround;

    [Header("«Ω√Ê")]
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float wallRadius;

    [HideInInspector] public bool touchLeftWall;
    [HideInInspector] public bool touchRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        Cheak();
    }

    public void Cheak()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, cheakRaduis, groundMask);
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, wallRadius, groundMask);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, wallRadius, groundMask);
    }

    //ªÊª≠∑∂Œß
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, cheakRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, wallRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, wallRadius);
    }
}
