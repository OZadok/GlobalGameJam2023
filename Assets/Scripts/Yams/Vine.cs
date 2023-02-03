using UnityEngine;
using Utils;

namespace Yams
{
    public class Vine : MonoBehaviour
    {
        [SerializeField] private int yamNum;
        [SerializeField] private float radius;

        private void Start()
        {
            GenerateYams();
        }

        private void GenerateYams()
        {
            var ang = 360f / yamNum;
            for (var i = 0; i < yamNum; i++)
            {
                var rot = Quaternion.AngleAxis(ang * i, Vector3.up);
                var pos = transform.position + rot * (Vector3.forward * radius);
                var yamGo = Instantiate(GameManager.Instance.yamPrefab, pos.WithY(0f), rot, GameManager.Instance.yamsParent);
            }
        }
    }
}