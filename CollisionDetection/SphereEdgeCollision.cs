using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    static class SphereEdgeCollision
    {
        public static CollisionResult CollidesWith(this BoundingSphere sphere, Edge edge, Vector3 velocity)
        {
            CollisionResult collisionResult = CollisionResult.NoCollision;

            Vector3 centerToVertex = edge.Vertex1 - sphere.Center;
            float edgeSquaredLength = edge.LengthSquared();
            float edgeDotVelocity = edge.Dot(velocity);
            float edgeDotCenterToVertex = edge.Dot(centerToVertex);

            //calculate the parameters for the equation
            float a = edgeSquaredLength * -velocity.LengthSquared() + edgeDotVelocity * edgeDotVelocity;
            float b = 2 * (edgeSquaredLength * velocity.Dot(centerToVertex) -
                           edgeDotVelocity * edgeDotCenterToVertex);
            float c = edgeSquaredLength * (sphere.Radius * sphere.Radius - centerToVertex.LengthSquared()) +
                edgeDotCenterToVertex * edgeDotCenterToVertex;
            Vector2? result = MathExtensions.SolveQuadricEquation(a, b, c);
            if (result.HasValue)
            {
                Vector2 solutions = result.GetValueOrDefault();
                //Not "bigger or equals to" to prevent being stuck when touching an object
                if (solutions.X > 0.0f)
                {
                    if (solutions.X <= 1.0f)
                    {
                        float f = (edgeDotVelocity * solutions.X - edgeDotCenterToVertex) / edgeSquaredLength;
                        if (f.InRange(0.0f, 1.0f))
                        {
                            collisionResult.FoundCollision = true;
                            collisionResult.IntersectionTime = solutions.X;
                            collisionResult.IntersectionPoint = edge.Vertex1 + f * edge.Direction;
                        }
                    }
                }
                else if (solutions.Y > 0.0f && solutions.Y <= 1.0f)
                {
                    float f = (edgeDotVelocity * solutions.Y - edgeDotCenterToVertex) / edgeSquaredLength;
                    if (f.InRange(0.0f, 1.0f))
                    {
                        collisionResult.FoundCollision = true;
                        collisionResult.IntersectionTime = solutions.Y;
                        collisionResult.IntersectionPoint = edge.Vertex1 + f * edge.Direction;
                    }
                }
            }

            return collisionResult;
        }
    }
}
