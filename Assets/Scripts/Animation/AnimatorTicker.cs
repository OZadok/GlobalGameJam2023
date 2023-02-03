using System;
using System.Collections;
using System.Collections.Generic;
using SuperMaxim.Messaging;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorTicker : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        _animator.enabled = false;
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<TickEvent>(OnTick);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<TickEvent>(OnTick);
    }
    
    private void OnTick(TickEvent tickEvent)
    {
        _animator.Update(tickEvent.DeltaTime);
    }
}
