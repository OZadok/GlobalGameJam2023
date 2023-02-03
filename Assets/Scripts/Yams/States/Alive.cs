using UnityEngine;

namespace Yams
{
    public class Alive : YamState
    {
        
        [SerializeField]
        public struct AliveStateSettings
        {
            public float speed;
        }

        private AliveStateSettings _settings;
        
        public Alive(YamStateManager manager, AliveStateSettings settings) : base(manager)
        {
            _settings = settings;
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override YamStateName Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}