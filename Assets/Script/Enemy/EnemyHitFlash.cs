using UnityEngine;
using System.Collections;

public class EnemyHitFlash : MonoBehaviour
{
    SpriteRenderer sprite;
    Color originalColor;
    public AudioSource audioSource;

    public GameObject bloodVFXPrefab;
    public Transform bloodPoint;

    public AudioClip hitClip;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        originalColor = sprite.color;
    }

    public void Flash()
    {
        Instantiate(bloodVFXPrefab, bloodPoint.position, Quaternion.identity);
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        sprite.color = Color.red;
        audioSource.PlayOneShot(hitClip);
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;

    }
}