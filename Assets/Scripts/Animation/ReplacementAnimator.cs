using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using SuperMaxim.Messaging;
using UnityEngine;

namespace Animation
{
    public class ReplacementAnimator : MonoBehaviour
    {
        [Serializable]
        class Animation
        {
            public string name;
            public Transform parent;
            public bool loop;
            public bool notifyOnEnd;
            public string nextAnimName;

            private ReplacementFrame[] _frames;

            public ReplacementFrame[] Frames
            {
                get 
                {
                    _frames ??= parent.GetComponentsInChildren<ReplacementFrame>().ToArray();
                    return _frames;
                }
            }
        }

        [SerializeField] private Animation[] animations;
        [SerializeField] private Transform controlledTransform;

        public float Speed { get; set; } = 1f; // this controls how much we move the controlledTransform on each Tick

        private Dictionary<string, Animation> _anims;
        private string _currAnimation;
        private int _currFrameIdx;
        private int _ttlFrames;
        private ReplacementFrame.FrameResult _lastFrameResult;
        private ReplacementFrame _frameToTurnOff;
        private bool _waitingAnimChange;

        private Animation CurrAnim => _anims[_currAnimation];
        private ReplacementFrame CurrFrame => CurrAnim.Frames[_currFrameIdx];


        private void Start()
        {
            _anims = new Dictionary<string, Animation>();
            foreach (var anim in animations)
            {
                _anims[anim.name] = anim;
                foreach (var frame in _anims[anim.name].Frames)
                    frame.TurnOff();
            }

            ChangeAnim(_anims.Keys.First());
        }

        private void OnEnable()
        {
            Messenger.Default.Subscribe<TickEvent>(Tick);
        }

        private void OnDisable()
        {
            Messenger.Default.Unsubscribe<TickEvent>(Tick);
        }

        public void Tick(TickEvent tickEvent)
        {
            if (_waitingAnimChange)
            {
                CurrFrame.TurnOn();
                CurrFrame.KeepAliveTweak(); //keepalive of currframe
                return;
            }
        
            
            if (!_lastFrameResult.hold)
            {
                _frameToTurnOff = CurrFrame;
                _currFrameIdx = (_currFrameIdx + 1) % _ttlFrames;

                // Animation cycle is over
                if (_currFrameIdx == 0)
                {
                    if (CurrAnim.notifyOnEnd)
                        Messenger.Default.Publish(new AnimationEndedEvent(){ animationName = _currAnimation});
                    
                    // We don't want to loop
                    if (!CurrAnim.loop)
                    {
                        //play next animation 
                        if (CurrAnim.nextAnimName != null)
                            ChangeAnim(CurrAnim.nextAnimName, true);

                        // stay stuck on curr frame
                        else
                        {
                            _frameToTurnOff = null;
                            _waitingAnimChange = true;
                        }
                    }
                }
            }

            // leftover after "ChangeAnim" was called
            if (_frameToTurnOff != null)
            {
                _frameToTurnOff.TurnOff();
                _frameToTurnOff.ResetHold();
                _frameToTurnOff = null;
            }
            
            _lastFrameResult = CurrFrame.TurnOn();
            if (controlledTransform != null)
                controlledTransform.transform.localPosition += _lastFrameResult.offset * Speed;
        }

        //TODO: add "bool immediate" argument to enable non-immediate transitions
        public void ChangeAnim(string animationName)
        {
            ChangeAnim(animationName, false);
        }
            
        private void ChangeAnim(string animationName, bool internalCall)
        {
            if (!_anims.ContainsKey(animationName))
                throw new Exception($"No such animation as {animationName}");

            Debug.Log($"Starting animation {animationName}");

            if (_currAnimation != null && !internalCall)
                _waitingAnimChange = false;
            
            _currAnimation = animationName;
            _ttlFrames = CurrAnim.Frames.Length;
            _currFrameIdx = 0;
            _lastFrameResult = new ReplacementFrame.FrameResult() {hold = true}; //this will make Tick stay on frame 0
        }
    }
}
