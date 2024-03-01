using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    public class Edge
    {
        private Vector3[] vertices;
        private Vector3 direction;

        public Vector3 Vertex1 { get { return vertices[0]; } }
        public Vector3 Vertex2 { get { return vertices[1]; } }
        public Vector3 Direction { get { return direction; } }

        public Edge(Vector3 vertex1, Vector3 vertex2)
        {
            vertices = new Vector3[2];
            vertices[0] = vertex1;
            vertices[1] = vertex2;

            direction = vertices[1] - vertices[0];
        }

        public float LengthSquared()
        {
            return direction.LengthSquared();
        }

        public float Dot(Vector3 vector)
        {
            return direction.Dot(vector);
        }
    }
}
