using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCryVFX : MonoBehaviour
{
    public float destroyTime = 1f;

    private void Update()
    {
        destroyTime -= Time.deltaTime;

        if ( destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
