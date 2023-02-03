using UnityEngine;

namespace Utils
{
    public static class ExtensionMethods
    {
        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }
        
        
    }
}