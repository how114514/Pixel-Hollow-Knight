using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundwave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<EnemyHitFlash>()?.Flash();

        other.GetComponent<EnemyController>()?.TakeDamage(50);
    }

    public void DestroyGroundwave()
    {
        Destroy(gameObject);
    }
}
