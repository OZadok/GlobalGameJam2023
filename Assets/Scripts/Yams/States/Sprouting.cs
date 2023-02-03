using System;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Yams
{
    public class Sprouting : YamState
    {
        [Serializable]
        public struct SproutingStateSettings
        {
            public float timeToSprout; 
        }

        private SproutingStateSettings _settings;
        private float timeSinceSprouted;

        public Sprouting(YamStateManager manager, SproutingStateSettings settings) : base(manager)
        {
            _settings = settings;
        }
        
        public override void Enter(YamStateName prevState)
        {
            timeSinceSprouted = 0f;
            manager.Anim.ChangeAnim("Sprout");
            Messenger.Default.Publish(new YamCreatedEvent());
        }

        public override YamStateName Update()
        {
            timeSinceSprouted += Time.deltaTime;
            if (timeSinceSprouted >= _settings.timeToSprout)
            {
                timeSinceSprouted = 0f;
                return YamStateName.Alive;
            }

            return YamStateName.Sprouting;
        }

        public override void Exit()
        {
            
        }

    }
}