using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    static class CollisionExtensions
    {
        public static float SignedDistanceTo(this Plane plane, Vector3 point)
        {
            return plane.DotCoordinate(point);
        }

        public static float Dot(this Vector3 vector1, Vector3 vector2)
        {
            return Vector3.Dot(vector1, vector2);
        }

        public static bool IsParallelTo(this Vector3 vector, Plane plane)
        {
            return plane.DotNormal(vector) == 0.0f;
        }

        public static bool IsFrontFacingTo(this Plane plane, Vector3 direction)
        {
            return plane.DotNormal(direction) <= 0.0f;
        }

        public static bool EmbeddedIn(this BoundingSphere sphere, Plane plane)
        {
            return sphere.Intersects(plane) == PlaneIntersectionType.Intersecting;
        }

        public static Vector3 Normalized(this Vector3 vector)
        {
            Vector3 normalizedVector = vector;
            normalizedVector.Normalize();
            return normalizedVector;
        }

        public static Plane Plane(Vector3 origin, Vector3 normal)
        {
            return new Plane(normal, -normal.Dot(origin));
        }

        public static Vector3 ProjectOn(this Vector3 vector, Plane plane)
        {
            return vector - plane.SignedDistanceTo(vector) * plane.Normal;
        }
    }
}
