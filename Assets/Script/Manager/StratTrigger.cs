using UnityEngine;
using UnityEngine.Events;

public class StartTrigger : MonoBehaviour
{
    [Header("¹ã²¥")]
    public VoidEventSO startEvent;
    public BoundsEventSO setBoundsEvent;

    [Header("×é¼þ")]
    public Collider2D roomBounds;

    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            setBoundsEvent.RaiseEvent(roomBounds);
            startEvent.RaiseEvent();
        }
    }

    private void OnEnable()
    {
        hasTriggered = false;
    }
}