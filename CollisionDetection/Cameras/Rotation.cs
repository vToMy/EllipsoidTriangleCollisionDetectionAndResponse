using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BiologicalPrison.Misc
{
    public class Rotation : GameComponent
    {
        private Vector3 angles;
        private Quaternion rotation;

        private Vector3 originalX;
        private Vector3 originalY;
        private Vector3 originalZ;

        private float delay;

        private Vector3 velocity;

        public float X { get { return angles.X; } set { angles.X = value; } }
        public float Y { get { return angles.Y; } set { angles.Y = value; } }
        public float Z { get { return angles.Z; } set { angles.Z = value; } }
        public float Delay { get { return delay; } set { delay = value; } }
        public Quaternion Quaternion { get { return rotation; } }
        public Matrix Matrix { get { return Matrix.CreateFromQuaternion(rotation); } }
        public Vector3 Velocity { get { return velocity; } set { velocity = value; } }

        public Rotation(Game game)
            : base(game)
        {
            angles = Vector3.Zero;
            rotation = Quaternion.Identity;

            originalX = Vector3.Right;
            originalY = Vector3.Up;
            originalZ = Vector3.Forward;

            delay = 1f;

            velocity = Vector3.One;
        }

        public override void Update(GameTime gameTime)
        {
            Quaternion nextRotation = Quaternion.CreateFromAxisAngle(originalZ, angles.Z) *
                Quaternion.CreateFromAxisAngle(originalY, angles.Y) *
                Quaternion.CreateFromAxisAngle(originalX, angles.X);

            rotation = Quaternion.Lerp(rotation, nextRotation, delay);
        }
    }
}
