﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    public class SphereTrianglesResponse
    {
        private const int MAX_RECURSSION_DEPTH = 1;
        private static Vector3 gravity = new Vector3(0, -0.08f, 0);
        public static bool gravityOn = false;

        public const float unitsPerMeter = 100.0f;
        public const float VeryCloseDistance = unitsPerMeter * .00005f;
        private static float radius;

        public static Vector3 CollideAndSlide(BoundingSphere sphere, Vector3 velocity, Triangle[] triangles)
        {
            radius = sphere.Radius;
            sphere.Center /= radius;
            sphere.Radius = 1.0f;

            velocity = CollideWithTriangles(sphere, velocity / radius, triangles, 0);

            //Add gravity pool
            if (gravityOn)
            {
                sphere.Center += velocity;
                velocity += CollideWithTriangles(sphere, gravity / radius, triangles, 0);
            }

            return velocity * radius;
        }

        private static Vector3 CollideWithTriangles(BoundingSphere sphere, Vector3 velocity,
                                                        Triangle[] triangles, int recurssionDepth)
        {
            if (recurssionDepth > MAX_RECURSSION_DEPTH)
            {
                return Vector3.Zero;
            }

            CollisionResult collision = CollidesWith(sphere, triangles, velocity);

            if (!collision.FoundCollision)
            {
                return velocity;
            }

            Vector3 originalDestinationPoint = sphere.Center + velocity;

            float intersectionDistance = velocity.Length() * collision.IntersectionTime;
            //Only update if we aren't very close, and if so only move very close
            //if (intersectionDistance >= VeryCloseDistance)
            //{
            Vector3 normalizedVelocity = velocity.Normalized();

            velocity = (intersectionDistance - VeryCloseDistance) * normalizedVelocity;
            sphere.Center += velocity;

            //Fake the collision results to match the very close approximation
            collision.IntersectionPoint -= normalizedVelocity * VeryCloseDistance;
            //}

            Plane slidingPlane = CollisionExtensions.Plane(collision.IntersectionPoint, sphere.Center - collision.IntersectionPoint);
            Vector3 destinationPoint = originalDestinationPoint.ProjectOn(slidingPlane);

            Vector3 newVelocityVector = destinationPoint - collision.IntersectionPoint;
            if (newVelocityVector.Length() < VeryCloseDistance)
            {
                return velocity;
            }

            return velocity + CollideWithTriangles(sphere, newVelocityVector, triangles, recurssionDepth + 1);
        }

        private static CollisionResult CollidesWith(BoundingSphere sphere, Triangle[] triangles, Vector3 velocity)
        {
            CollisionResult closestCollision = CollisionResult.NoCollision;
            foreach (Triangle triangle in triangles)
            {
                CollisionResult collision = sphere.CollidesWith(triangle / new Vector3(radius), velocity);
                if (collision.IntersectionTime < closestCollision.IntersectionTime)
                {
                    closestCollision = collision;
                }
            }
            return closestCollision;
        }
    }
}
