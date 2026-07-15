using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/BoundsEventSO")]
public class BoundsEventSO : ScriptableObject
{
    public Action<Collider2D> OnEventRaised;

    public void RaiseEvent(Collider2D bounds)
    {
        OnEventRaised?.Invoke(bounds);
    }
}