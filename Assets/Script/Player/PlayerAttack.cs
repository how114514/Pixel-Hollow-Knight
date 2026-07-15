using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        other.GetComponent<EnemyHitFlash>()?.Flash();

        other.GetComponent<EnemyController>()?.TakeDamage(10);

        if (player.stateMachine.currentState is PlayerAttackState attackState)
        {
            attackState.OnHitEnemy();
        }

    }
}
