using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource audioSource;

    public VoidEventSO startEvent;
    public VoidEventSO bossDeadEvent;

    public Animator anim;

    public AudioClip open;
    public AudioClip close;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        startEvent.OnEventRaised += CloseDoor;
        bossDeadEvent.OnEventRaised += OpenDoor;
    }

    private void OnDisable()
    {
        startEvent.OnEventRaised -= CloseDoor;
        bossDeadEvent.OnEventRaised -= OpenDoor;
    }

    private void OpenDoor()
    {
        anim.Play("open");
        audioSource.PlayOneShot(open);

    }

    void CloseDoor()
    {
        anim.Play("close");
        audioSource.PlayOneShot(close);
    }
}
