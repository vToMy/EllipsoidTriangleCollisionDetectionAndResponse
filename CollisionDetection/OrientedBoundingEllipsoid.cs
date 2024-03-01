using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    struct OrientedBoundingEllipsoid
    {
        private Vector3 center;
        private Vector3 radius;
        private Quaternion orientation;
        private Matrix world;

        public Vector3 Center { get { return center; } set { center = value; } }
        public Vector3 Radius { get { return radius; } set { radius = value; } }
        public Quaternion Orientation { get { return orientation; } set { orientation = value; } }
        public Matrix World { get { return world; } set { world = value; } }

        public OrientedBoundingEllipsoid(Vector3 center, Vector3 radius, Quaternion orientation)
        {
            this.center = center;
            this.radius = radius;
            this.orientation = orientation;
            world = Matrix.Identity;
        }

        public OrientedBoundingEllipsoid(BoundingSphere sphere)
            : this(sphere.Center, new Vector3(sphere.Radius), Quaternion.Identity) { }
    }
}
