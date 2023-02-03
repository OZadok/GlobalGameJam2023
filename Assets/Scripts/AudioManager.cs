using System;
using SuperMaxim.Messaging;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEvent _shovelHitAudioEvent;
    private float _lastTimeShovelHit;

    [SerializeField] private AudioEvent _stepAudioEvent;
    [SerializeField] private AudioEvent _whooshAudioEvent;

    public static AudioManager Instance; 
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<YamHitEvent>(OnYamHit);
        Messenger.Default.Subscribe<ShovelWhooshEvent>(OnShovelWhoosh);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<YamHitEvent>(OnYamHit);
        Messenger.Default.Subscribe<ShovelWhooshEvent>(OnShovelWhoosh);
    }

    private void OnShovelWhoosh(ShovelWhooshEvent obj)
    {
        Invoke(nameof(WhooshPlay), 0.2f);
    }

    private void WhooshPlay()
    {
        _whooshAudioEvent.Play();
    }

    private void OnYamHit(YamHitEvent obj)
    {
        if (_lastTimeShovelHit >= Time.time)
        {
            return;
        }

        _lastTimeShovelHit = Time.time;
        _shovelHitAudioEvent.Play();
    }

    public void OnStep()
    {
        _stepAudioEvent.Play();
    }
}