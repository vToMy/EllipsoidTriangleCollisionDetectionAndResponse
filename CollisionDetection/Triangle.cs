using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    public class Triangle
    {
        private Vector3[] vertices;
        private Plane plane;

        public Vector3 Vertex1 { get { return vertices[0]; } }
        public Vector3 Vertex2 { get { return vertices[1]; } }
        public Vector3 Vertex3 { get { return vertices[2]; } }
        public Vector3[] Vertices { get { return vertices; } }
        public Plane Plane { get { return plane; } }
        public IEnumerable<Edge> Edges
        {
            get
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    yield return new Edge(vertices[i], vertices[(i + 1) % vertices.Length]);
                }
            }
        }
        

        public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            vertices = new Vector3[3] { vertex1, vertex2, vertex3 };

            plane = new Plane(vertices[0], vertices[1], vertices[2]);
        }

        public bool ContainsPoint(Vector3 point)
        {
            // Compute vectors        
            Vector3 v0 = vertices[2] - vertices[0];
            Vector3 v1 = vertices[1] - vertices[0];
            Vector3 v2 = point - vertices[0];

            // Compute dot products
            float dot00 = v0.Dot(v0);
            float dot01 = v0.Dot(v1);
            float dot02 = v0.Dot(v2);
            float dot11 = v1.Dot(v1);
            float dot12 = v1.Dot(v2);

            // Compute barycentric coordinates
            float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            // Check if point is in triangle
            return (u > 0) && (v > 0) && (u + v < 1);
        }

        public static Triangle operator /(Triangle triangle, Vector3 vector)
        {
            return new Triangle(triangle.Vertices[0] / vector,
                triangle.Vertices[1] / vector,
                triangle.Vertices[2] / vector);
        }

        public Triangle Transform(Matrix matrix)
        {
            return new Triangle(Vector3.Transform(vertices[0], matrix),
                Vector3.Transform(vertices[1], matrix),
                Vector3.Transform(vertices[2], matrix));
        }

        public Triangle TransformNormal(Matrix matrix)
        {
            return new Triangle(Vector3.TransformNormal(vertices[0], matrix),
                Vector3.TransformNormal(vertices[1], matrix),
                Vector3.TransformNormal(vertices[2], matrix));
        }
    }
}
