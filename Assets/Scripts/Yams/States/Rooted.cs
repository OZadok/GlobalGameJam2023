using System;
using UnityEngine;

namespace Yams.States
{
    public class Rooted : YamState
    {
        
        [Serializable]
        public struct RootedStateSettings
        {
            public float timeToBecomeVine;
            public GameObject vinePrefab;
        }
        
        private RootedStateSettings _settings;
        private float timeSinceRooted;
        
        public Rooted(YamStateManager manager, RootedStateSettings settings) : base(manager)
        {
            _settings = settings;
        }
        
        public override void Enter()
        {
            timeSinceRooted = 0f;
            manager.Anim.ChangeAnim("Rooted");
        }

        public override YamStateName Update()
        {
            timeSinceRooted += Time.deltaTime;
            if (timeSinceRooted >= _settings.timeToBecomeVine)
            {
                timeSinceRooted = 0f;
                InstantiateVine();
                return YamStateName.Destroyed;
            }

            return YamStateName.Rooted;
        }

        private void InstantiateVine()
        {
            UnityEngine.Object.Instantiate(_settings.vinePrefab, manager.transform.position, Quaternion.identity);
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}