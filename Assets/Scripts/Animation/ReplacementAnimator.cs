using System;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public class ReplacementAnimator : MonoBehaviour
    {
        [Serializable]
        struct Animation
        {
            public string name;
            public ReplacementFrame parent;
        }

        [SerializeField] private Animation[] animations;
        [SerializeField] private Transform controlledTransform;

        public float Speed { get; set; } = 1f; // this controlls how much we move the controlledTransform on each Tick

        private Dictionary<string, ReplacementFrame[]> _frames;
        private string _currAnimation;
        private int _currFrame;
        private int _ttlFrames;


        private void Start()
        {
            _frames = new Dictionary<string, ReplacementFrame[]>();
            foreach (var anim in animations)
            {
                _frames[anim.name] = anim.parent.GetComponentsInChildren<ReplacementFrame>();
                foreach (var frame in _frames[anim.name])
                    frame.TurnOff();
            }
        }

        public void Tick()
        {
            _frames[_currAnimation][_currFrame].TurnOff();
            _currFrame = (_currFrame + 1) % _ttlFrames;
            var offset = _frames[_currAnimation][_currFrame].TurnOn();
            if (controlledTransform != null)
                controlledTransform.transform.localPosition += offset * Speed;
        }

        //TODO: add "bool immediate" argument to enable non-immediate transitions
        public void ChangeAnim(string animationName)
        {
            if (!_frames.ContainsKey(animationName))
                throw new Exception($"No such animation as {animationName}");

            _currAnimation = animationName;
            _ttlFrames = _frames[_currAnimation].Length;
            _currFrame = 0;
        }
    }
}
