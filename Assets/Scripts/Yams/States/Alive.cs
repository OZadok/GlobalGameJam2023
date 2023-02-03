using System;
using SuperMaxim.Messaging;
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

        private YamStateName _yamStateName; 
        
        public Alive(YamStateManager manager, AliveStateSettings settings) : base(manager)
        {
            _settings = settings;
        }

        public override void Enter(YamStateName prevState)
        {
            manager.Anim.ChangeAnim("Walk");
            Messenger.Default.Subscribe<YamHitEvent>(OnHit);
            _yamStateName = YamStateName.Alive;
            Messenger.Default.Subscribe<TickEvent>(OnTick);
        }

        public override YamStateName Update()
        {
            
            // manager.transform.Rotate(Vector3.up, 0.1f);
            return _yamStateName;
        }

        public void OnTick(TickEvent tickEvent)
        {
            var managerTransform = manager.transform;
            var position = managerTransform.position;
            var forward = managerTransform.forward;
            var right = managerTransform.right;
            var distance = 1;
            var forwardCollision = Physics.Raycast(position, forward, distance);
            var forwardRightCollision = Physics.Raycast(position, forward + right, distance);
            var forwardLeftCollision = Physics.Raycast(position, forward - right, distance);
            var rotationSpeed = new Vector3(0, 90, 0);
            if (forwardRightCollision || forwardCollision)
            {
                manager.transform.Rotate(-rotationSpeed * tickEvent.DeltaTime);
            }
            else if (forwardLeftCollision)
            {
                manager.transform.Rotate(rotationSpeed * tickEvent.DeltaTime);
            }
        }
        // public override void FixedUpdate()
        // {
        //     base.FixedUpdate();
        //     var managerTransform = manager.transform;
        //     var position = managerTransform.position;
        //     var forward = managerTransform.forward;
        //     var right = managerTransform.right;
        //     var distance = 1;
        //     var forwardCollision = Physics.Raycast(position, forward, distance);
        //     var forwardRightCollision = Physics.Raycast(position, forward + right, distance);
        //     var forwardLeftCollision = Physics.Raycast(position, forward - right, distance);
        //     var rotationSpeed = new Vector3(0, 90, 0);
        //     if (forwardRightCollision || forwardCollision)
        //     {
        //         manager.transform.Rotate(-rotationSpeed * Time.fixedDeltaTime);
        //     }
        //     else if (forwardLeftCollision)
        //     {
        //         manager.transform.Rotate(rotationSpeed * Time.fixedDeltaTime);
        //     }
        // }

        public override void Exit()
        {
            Messenger.Default.Unsubscribe<TickEvent>(OnTick);
            Messenger.Default.Unsubscribe<YamHitEvent>(OnHit);
        }

        private void OnHit(YamHitEvent yamHitEvent)
        {
            if (manager.Collider == yamHitEvent.Collider)
            {
                // change state to Rooted
                _yamStateName = YamStateName.Rooted;
                Messenger.Default.Unsubscribe<YamHitEvent>(OnHit);
            }
        }
    }
}