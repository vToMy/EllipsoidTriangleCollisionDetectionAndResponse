using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    class CollisionResult
    {
        bool foundCollision;
        float intersectionTime;
        Vector3 intersectionPoint;
        
        public bool FoundCollision { get { return foundCollision; } set { foundCollision = value; } }
        public float IntersectionTime { get { return intersectionTime; } set { intersectionTime = value; } }
        public Vector3 IntersectionPoint { get { return intersectionPoint; } set { intersectionPoint = value; } }

        public static CollisionResult NoCollision
        {
            get { return new CollisionResult(false, 2.0f, Vector3.Zero); }
        }

        public CollisionResult(bool foundCollision, float intersectionTime, Vector3 intersectionPoint)
        {
            this.intersectionTime = intersectionTime;
            this.foundCollision = foundCollision;
            this.intersectionPoint = intersectionPoint;
        }
    }
}
