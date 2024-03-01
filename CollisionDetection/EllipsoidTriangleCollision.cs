using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    static class EllipsoidTriangleCollision
    {
        public static CollisionResult CollidesWith(this BoundingEllipsoid ellipsoid, Triangle triangle, Vector3 velocity)
        {
            BoundingSphere sphere = new BoundingSphere(ellipsoid.Center / ellipsoid.Radius, 1f);
            Vector3 eVelocity = velocity / ellipsoid.Radius;
            Triangle eTriangle = triangle / ellipsoid.Radius;

            CollisionResult collisionResult = sphere.CollidesWith(eTriangle, eVelocity);
            collisionResult.IntersectionPoint *= ellipsoid.Radius;
            return collisionResult;
        }
    }
}
