using System.Collections.Generic;
using UnityEngine;

public class OurPhysicsSystem : MonoBehaviour
{
    public enum Layers
    {
        Player,
        Yam,
        Vine
    }

    public const float VineRadius = 0.5f;
    public const float PlayerRadius = 0.5f;
    public static OurPhysicsSystem Instance;

    [HideInInspector] public List<Transform> vines;

    private void Awake()
    {
        Instance = this;
        vines = new List<Transform>();
    }

    public void RegisterVine(Transform vineTransform)
    {
        vines.Add(vineTransform);
    }

    public bool RemoveVine(Transform vineTransform)
    {
        return vines.Remove(vineTransform);
    }

    public bool CheckCollisionWithVine(Vector3 position, float radius, out Transform vine)
    {
        position.y = 0;
        var sqrDistance = (radius + VineRadius) * (radius + VineRadius);
        for (var i = 0; i < vines.Count; i++)
        {
            var vineTransform = vines[i];
            var sqrHorizontalDistance = GetHorizontalSqrDistance(vineTransform.position, position);
            if (!(sqrHorizontalDistance <= sqrDistance)) continue;
            vine = vineTransform;
            return true;
        }

        vine = null;
        return false;
    }

    public static float GetHorizontalSqrDistance(Vector3 a, Vector3 b)
    {
        var diff = a - b;
        return diff.x * diff.x + diff.z * diff.z;
    }
}