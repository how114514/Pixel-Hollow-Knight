using UnityEngine;

public class PlayerSonicwave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHitFlash>()?.Flash();

        other.GetComponent<EnemyController>()?.TakeDamage(100);
    }

    public void DestroySonicwave()
    {
        Destroy(gameObject);
    }
}