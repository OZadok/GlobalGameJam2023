using System;
using UnityEngine;

namespace Yams
{
    public class Alive : YamState
    {
        
        [Serializable]
        public struct AliveStateSettings
        {
            public float speed;
        }

        private AliveStateSettings _settings;
        
        public Alive(YamStateManager manager, AliveStateSettings settings) : base(manager)
        {
            _settings = settings;
        }

        public override void Enter(YamStateName prevState)
        {
            manager.Anim.ChangeAnim("Walk");
        }

        public override YamStateName Update()
        {
            manager.transform.Rotate(Vector3.up, 0.1f);
            return YamStateName.Alive;
        }

        public override void Exit()
        {
            
        }
    }
}