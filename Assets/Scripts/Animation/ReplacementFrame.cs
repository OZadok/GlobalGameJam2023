using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ReplacementFrame : MonoBehaviour
    {

        public struct FrameResult
        {
            public bool hold;
            public Vector3 offset; 
        }

        [SerializeField] private Vector3 offset;
        [SerializeField] private int hold = 1;
        
        private MeshRenderer _mr;
        private int _holdCounter;

        
        
        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
            _holdCounter = hold;
        }

        public void TurnOff()
        {
            ResetHold();
            _mr.enabled = false;
        }
        
        public FrameResult TurnOn()
        {
            _mr.enabled = true;
            
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