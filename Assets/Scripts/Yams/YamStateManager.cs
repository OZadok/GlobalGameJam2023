using System.Collections.Generic;
using Animation;
using UnityEngine;
using Yams.States;

namespace Yams
{
    public class YamStateManager : MonoBehaviour
    {
        private Dictionary<YamState.YamStateName, YamState> _states;
        
        [SerializeField] private Sprouting.SproutingStateSettings _sproutingSettings;
        [SerializeField] private Alive.AliveStateSettings _aliveSettings;
        [SerializeField] private Rooted.RootedStateSettings _rootedSettings;
        [SerializeField] private Idle.IdleStateSettings _idleSettings;
        [SerializeField] private ReplacementAnimator _anim;

        [SerializeField] private YamState.YamStateName _currentStateName;
        private YamState CurrentState => _states[_currentStateName];
        
        public ReplacementAnimator Anim => _anim;
        public Collider Collider;

        private void Start()
        {
            _states = new Dictionary<YamState.YamStateName, YamState>()
            {
                {YamState.YamStateName.Alive, new Alive(this, _aliveSettings)},
                {YamState.YamStateName.Sprouting, new Sprouting(this, _sproutingSettings)},
                {YamState.YamStateName.Rooted, new Rooted(this, _rootedSettings)},
                {YamState.YamStateName.Escaped, new Escaped(this)},
                {YamState.YamStateName.Idle, new Idle(this, _idleSettings)},
            };

            _currentStateName = YamState.YamStateName.Sprouting;
            CurrentState.Enter(YamState.YamStateName.None);
        }

        private void Update()
        {
            var newStateName = CurrentState.Update();
            
            if (_states[newStateName] != CurrentState)
            {
                Debug.Log($"{_currentStateName} ======> {newStateName}");
                CurrentState.Exit();
                var previousStateName = _currentStateName;
                _currentStateName = newStateName;
                if (newStateName == YamState.YamStateName.Destroyed)
                {
                    Destroy(this.gameObject);
                    return;
                }
                CurrentState.Enter(previousStateName);
            }
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }
    }
}