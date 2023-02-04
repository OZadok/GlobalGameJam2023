using System;
using SuperMaxim.Messaging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Yams
{
    public class Sprouting : YamState
    {
        [Serializable]
        public struct SproutingStateSettings
        {
            public float timeToSproutMin; 
            public float timeToSproutMax;
            public float safeDistanceFromPlayer;
        }

        private SproutingStateSettings _settings;
        private float timeSinceSprouted;
        private float _timeToSprout;

        public Sprouting(YamStateManager manager, SproutingStateSettings settings) : base(manager)
        {
            _settings = settings;
        }
        
        public override void Enter(YamStateName prevState)
        {
            timeSinceSprouted = 0f;
            manager.Anim.ChangeAnim("Sprout");
            Messenger.Default.Publish(new YamCreatedEvent());
            _timeToSprout = Random.Range(_settings.timeToSproutMin, _settings.timeToSproutMax);
        }

        public override YamStateName Update()
        {
            timeSinceSprouted += Time.deltaTime;
            if (timeSinceSprouted >= _timeToSprout && IsFarFromPlayer())
            {
                timeSinceSprouted = 0f;
                return YamStateName.Alive;
            }

            return YamStateName.Sprouting;
        }

        private bool IsFarFromPlayer()
        {
            return (GameManager.Instance.PlayerTransform.position - manager.transform.position).sqrMagnitude >
                   _settings.safeDistanceFromPlayer * _settings.safeDistanceFromPlayer;
        }

        public override void Exit()
        {
            
        }

    }
}