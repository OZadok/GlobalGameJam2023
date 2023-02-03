using UnityEngine;

namespace Yams
{
    public class Sprouting : YamState
    {
        [SerializeField]
        public struct SproutingStateSettings
        {
            public float timeToSprout; 
        }

        private SproutingStateSettings _settings; 

        public Sprouting(YamStateManager manager, SproutingStateSettings settings) : base(manager)
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