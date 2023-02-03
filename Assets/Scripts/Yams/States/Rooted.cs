using System;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Yams.States
{
    public class Rooted : YamState
    {
        
        [Serializable]
        public struct RootedStateSettings
        {
            public float timeToBecomeVine;
        }
        
        private RootedStateSettings _settings;
        private float timeSinceRooted;
        
        public Rooted(YamStateManager manager, RootedStateSettings settings) : base(manager)
        {
            _settings = settings;
        }
        
        public override void Enter(YamStateName prevState)
        {
            timeSinceRooted = 0f;
            manager.Anim.ChangeAnim("Rooted");
            Messenger.Default.Publish(new YamRootedEvent());
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

        private int instantiateCounter = 0;
        private void InstantiateVine()
        {
            instantiateCounter++;
            Debug.Log($"instantiateCounter: {instantiateCounter}");
            UnityEngine.Object.Instantiate(GameManager.Instance.vinesParent, manager.transform.position, Quaternion.identity);
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}