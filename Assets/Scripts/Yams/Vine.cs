using System;
using System.Collections;
using Animation;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Yams
{
    public class Vine : MonoBehaviour
    {
        [SerializeField] private int yamNumMin = 3;
        [SerializeField] private int yamNumMax = 5;
        [SerializeField] private float radiusMin = 1.8f;
        [SerializeField] private float radiusMax = 3f;
        [SerializeField] private float yamInstantiateTimeMin = 3f;
        [SerializeField] private float yamInstantiateTimeMax = 4.5f;
        [SerializeField] private ReplacementAnimator animator;

        private void Start()
        {
            // animator.ChangeAnim("Grow"); // this happens automatically when its the only animation
            GenerateYams();
        }

        private void OnEnable()
        {
            OurPhysicsSystem.Instance.RegisterVine(transform);
        }

        private void OnDisable()
        {
            OurPhysicsSystem.Instance.RemoveVine(transform);
        }

        private void GenerateYams()
        {
            int yamNum = Random.Range(yamNumMin, yamNumMax + 1);
            var ang = 360f / yamNum;
            for (var i = 0; i < yamNum; i++)
            {
                var rot = Quaternion.AngleAxis(ang * i + Random.value * Random.value * ang, Vector3.up);
                var pos = transform.position + rot * (Vector3.forward * Random.Range(radiusMin, radiusMax));
                var timeDelay = Random.Range(yamInstantiateTimeMin, yamInstantiateTimeMax);
                StartCoroutine(CreateVineInTime(rot, pos, timeDelay));
            }
        }

        private IEnumerator CreateVineInTime(Quaternion rotation, Vector3 position, float time)
        {
            yield return new WaitForSeconds(time);
            var yamGo = Instantiate(GameManager.Instance.yamPrefab, position.WithY(0f), rotation, GameManager.Instance.yamsParent);
        }
    }
}