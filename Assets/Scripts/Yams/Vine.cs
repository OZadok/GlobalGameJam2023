using UnityEngine;
using Utils;

namespace Yams
{
    public class Vine : MonoBehaviour
    {
        [SerializeField] private int yamNumMin = 3;
        [SerializeField] private int yamNumMax = 5;
        [SerializeField] private float radiusMin = 1.8f;
        [SerializeField] private float radiusMax = 3f;

        private void Start()
        {
            GenerateYams();
        }

        private void GenerateYams()
        {
            int yamNum = Random.Range(yamNumMin, yamNumMax + 1);
            var ang = 360f / yamNum;
            for (var i = 0; i < yamNum; i++)
            {
                var rot = Quaternion.AngleAxis(ang * i + Random.value * Random.value * ang, Vector3.up);
                var pos = transform.position + rot * (Vector3.forward * Random.Range(radiusMin, radiusMax));
                var yamGo = Instantiate(GameManager.Instance.yamPrefab, pos.WithY(0f), rot, GameManager.Instance.yamsParent);
            }
        }
    }
}