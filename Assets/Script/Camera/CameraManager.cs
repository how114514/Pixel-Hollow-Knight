using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("")]
    public BoundsEventSO SetBoundsEvent;
    public BoundsEventSO endBoundsEvent;

    [Header("")]
    public CinemachineVirtualCamera vcam;
    public CinemachineConfiner2D confiner; 
    public CinemachineImpulseSource impulseSource;

    public void Shake()
    {
        impulseSource.GenerateImpulse();
    }

    void OnEnable()
    {
        SetBoundsEvent.OnEventRaised += SetBounds;
        endBoundsEvent.OnEventRaised += SetBounds;
    }

    void OnDisable()
    {
        SetBoundsEvent.OnEventRaised -= SetBounds;
        endBoundsEvent.OnEventRaised -= SetBounds;
    }

    public void SetBounds(Collider2D newBounds)
    {
        confiner.m_BoundingShape2D = newBounds;

        confiner.InvalidateCache();
    }
}
