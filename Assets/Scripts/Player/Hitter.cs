using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hitter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int Hit = Animator.StringToHash("Hit");

    [SerializeField] private float _timeFromPressToHit = 0f;

    [SerializeField] private Transform _hitCenter;
    [SerializeField] private float _hitRadius = 1f;
    [SerializeField] private LayerMask _yamsLayerMask;

    private ShovelWhooshEvent _shovelWhooshEvent = new ShovelWhooshEvent();

    private float _lastWhackTime;

    private void Reset()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Awake()
    {
        Reset();
    }

    private void OnWhack(InputValue value)
    {
        if (value.isPressed && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && Time.time >= _lastWhackTime + _timeFromPressToHit + 0.2f)
        {
            _lastWhackTime = Time.time;
            Messenger.Default.Publish(_shovelWhooshEvent);
            _animator.SetTrigger(Hit);
            Invoke(nameof(CalculateHit), _timeFromPressToHit);
        }
    }

    private void CalculateHit()
    {
        var colliders = Physics.OverlapSphere(_hitCenter.position, _hitRadius, _yamsLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            Messenger.Default.Publish(new YamHitEvent(colliders[i]));
        }
    }
}