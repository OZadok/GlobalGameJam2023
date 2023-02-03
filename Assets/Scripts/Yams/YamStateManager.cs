using System.Collections.Generic;
using UnityEngine;

namespace Yams
{
    public class YamStateManager : MonoBehaviour
    {
        private Dictionary<YamState.YamStateName, YamState> _states;
        
        [SerializeField] private Sprouting.SproutingStateSettings _sproutingSettings;
        [SerializeField] private Alive.AliveStateSettings _aliveSettings;
        [SerializeField] private Rooted.RootedStateSettings _rootedSettings;

        private YamState _currentState;

        private void Start()
        {
            _states = new Dictionary<YamState.YamStateName, YamState>()
            {
                {YamState.YamStateName.Alive, new Alive(this, _aliveSettings)},
                {YamState.YamStateName.Sprouting, new Sprouting(this, _sproutingSettings)},
                {YamState.YamStateName.Rooted, new Rooted(this, _rootedSettings)},
                {YamState.YamStateName.Escaped, new Escaped(this)},
            };

            _currentState = _states[YamState.YamStateName.Sprouting];
            _currentState.Enter();
        }

        private void Update()
        {
            var newStateName = _currentState.Update();
            
            if (_states[newStateName] != _currentState)
            {
                _currentState.Exit();
                _currentState = _states[newStateName];
                _currentState.Enter();
            }
        }
    }
}