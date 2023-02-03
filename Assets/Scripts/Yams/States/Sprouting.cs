using System;
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
        
        public override void Enter()
        {
            timeSinceSprouted = 0f;
            manager.Anim.ChangeAnim("Sprout");
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