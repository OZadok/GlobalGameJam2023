using System;
using UnityEngine;

namespace Yams.States
{
    public class Idle : YamState
    {
        
        [Serializable]
        public struct IdleStateSettings
        {
            public float timeToWalk;
        }

        private IdleStateSettings _settings;
        private float _timeSinceIdle;

        public Idle(YamStateManager manager, IdleStateSettings settings) : base(manager)
        {
            _settings = settings;
        }
        
        public override void Enter(YamStateName prevState)
        {
            _timeSinceIdle = 0f;
        }

        public override YamStateName Update()
        {
            _timeSinceIdle += Time.deltaTime;
            if (_timeSinceIdle > _settings.timeToWalk)
            {
                _timeSinceIdle = 0f;
                return YamStateName.Alive;
            }

            return YamStateName.Idle;
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}