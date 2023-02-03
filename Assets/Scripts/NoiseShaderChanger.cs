using System;
using SuperMaxim.Messaging;
using UnityEngine;

public class NoiseShaderChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material[] _materials;

    private void Reset()
    {
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        if (_materials == null)
        {
            _materials = _meshRenderer.materials;
        }
    }

    private void Awake()
    {
        Reset();
    }

    public void SetNoise(float seed)
    {
        foreach (var material in _materials)
        {
            material.SetFloat("_Seed", seed);
        }
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<TickEvent>(OnTick);
    }

    private float noise = 0;
    private void OnTick(TickEvent obj)
    {
        SetNoise(noise++);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<TickEvent>(OnTick);
    }
}