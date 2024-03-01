using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    public class BoundingEllipsoid
    {
        private Vector3 originalCenter;
        private Vector3 originalRadius;

        private Vector3 center;
        private Vector3 radius;

        private Matrix world;

        public Vector3 Center { get { return center; } set { center = value; } }
        public Vector3 Radius { get { return radius; } set { radius = value; } }
        public Matrix World { get { return world; } }

        public BoundingEllipsoid(Vector3 center, Vector3 radius)
        {
            this.originalCenter = this.center = center;
            this.originalRadius = this.radius = radius;
        }

        public BoundingEllipsoid(BoundingSphere sphere)
        {
            this.originalCenter = center = sphere.Center;
            this.originalRadius = radius = new Vector3(sphere.Radius);
        }

        public void Transform(Matrix matrix)
        {
            world = matrix;
            center = Vector3.Transform(originalCenter, matrix);
            radius = Vector3.TransformNormal(originalRadius, matrix);
        }

        public override string ToString()
        {
            return "Center: " + center + " Radius: " + radius;
        }
    }
}
