
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ReplacementFrame : MonoBehaviour
    {
        
        [SerializeField] Vector3 offset;
        
        private MeshRenderer _mr;

        
        
        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();
        }

        public void TurnOff()
        {
            _mr.enabled = false;
        }
        
        public Vector3 TurnOn()
        {
            _mr.enabled = true;
            return offset;
        }
    }
}