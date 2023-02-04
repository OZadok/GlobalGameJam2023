using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Animation
{
    public class ReplacementFrame : MonoBehaviour
    {

        public struct FrameResult
        {
            public bool hold;
            public Vector3 offset; 
        }

        [SerializeField] private Vector3 offset;
        [SerializeField] private int hold = 1;
        
        private MeshRenderer[] _mrs;
        private int _holdCounter;

        [SerializeField] private AudioEvent _audioEvent;

        
        
        private void Awake()
        {
            _mrs = GetComponentsInChildren<MeshRenderer>(true);
            _holdCounter = hold;
        }

        public void TurnOff()
        {
            ResetHold();
            _mrs.ForEach(mr =>  mr.enabled = false);
        }
        
        public FrameResult TurnOn()
        {
            _audioEvent?.Play(transform);
            _mrs.ForEach(mr =>  mr.enabled = true);
            
            _holdCounter -= 1;
            if (_holdCounter == 0)
                return new FrameResult() {hold = false, offset = offset};
            
            KeepAliveTweak();
            return new FrameResult() {hold = true, offset = offset};

        }

        public void ResetHold()
        {
            _holdCounter = hold;
        }

        public void KeepAliveTweak()
        {
            //TODO - tweak model from shader to assimilate a "breathing frame"
        }
    }
}