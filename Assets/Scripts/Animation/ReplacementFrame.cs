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
            _holdCounter = hold; // reset;
            _mr.enabled = false;
        }
        
        public FrameResult TurnOn()
        {
            _mr.enabled = true;
            
            _holdCounter -= 1;
            if (_holdCounter == 0) 
                return new FrameResult() {hold = false, offset = offset};
            
            TweakModel();
            return new FrameResult() {hold = true, offset = offset};

        }

        private void TweakModel()
        {
            //TODO - tweak model from shader to assimilate a "breathing frame"
        }
    }
}