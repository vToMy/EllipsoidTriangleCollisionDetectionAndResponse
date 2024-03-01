using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    static class SpherePointCollision
    {
        public static CollisionResult CollidesWith(this BoundingSphere sphere, Vector3 point, Vector3 velocity)
        {
            CollisionResult collisionResult = CollisionResult.NoCollision;

            /* The collision with a vertex comes down to a quadratic equation
             * with a maximum of two possible points of collision.           */
            float a = velocity.LengthSquared();
            float b = 2.0f * velocity.Dot(sphere.Center - point);
            float c = (point - sphere.Center).LengthSquared() - sphere.Radius * sphere.Radius;
            Vector2? result = MathExtensions.SolveQuadricEquation(a, b, c);

            if (result.HasValue)
            {
                Vector2 solutions = result.GetValueOrDefault();
                //Not "bigger or equals to" to prevent being stuck when touching an object
                if (solutions.X > 0.0f)
                {
                    if (solutions.X <= 1.0f)
                    {
                        collisionResult.FoundCollision = true;
                        collisionResult.IntersectionTime = solutions.X;
                        collisionResult.IntersectionPoint = point;
                    }
                }
                else if (solutions.Y > 0.0f && solutions.Y <= 1.0f)
                {
                    collisionResult.FoundCollision = true;
                    collisionResult.IntersectionTime = solutions.Y;
                    collisionResult.IntersectionPoint = point;
                }
            }
            return collisionResult;
        }
    }
}
