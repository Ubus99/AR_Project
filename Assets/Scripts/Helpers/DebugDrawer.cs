using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class DebugDrawer : MonoBehaviour
    {
        public static readonly Dictionary<string, Line> Lines = new Dictionary<string, Line>();
        public static readonly Dictionary<string, Ray> Rays = new Dictionary<string, Ray>();

        void Awake()
        {
            ClearCache();
        }

        void OnDestroy()
        {
            ClearCache();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var l in Lines.Values)
            {
                Gizmos.DrawSphere(l.A, 0.01f);
                Gizmos.DrawSphere(l.B, 0.01f);
                Gizmos.DrawLine(l.A, l.B);
            }
            foreach (var l in Rays.Values)
            {
                Gizmos.DrawSphere(l.A, 0.01f);
                Gizmos.DrawLine(l.A, l.Dir);
            }
        }

        static void ClearCache()
        {
            Lines.Clear();
            Rays.Clear();
        }

        public struct Line
        {
            public Vector3 A;
            public Vector3 B;
        }

        public struct Ray
        {
            public Vector3 A;
            public Vector3 Dir;
        }
    }
}
