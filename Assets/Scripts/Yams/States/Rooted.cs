using UnityEngine;

namespace Yams
{
    public class Rooted : YamState
    {
        
        [SerializeField]
        public struct RootedStateSettings
        {
            public float timeToBecomeVine;
            public GameObject vinePrefab;
        }

        private RootedStateSettings _settings;
        
        public Rooted(YamStateManager manager, RootedStateSettings settings) : base(manager)
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