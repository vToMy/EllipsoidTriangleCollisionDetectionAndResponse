using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BiologicalPrison.Cameras
{
    public class Camera : GameComponent
    {
        //Members
        protected Vector3 position;
        protected Vector3 target;
        protected Vector3 up;
        protected Matrix view;
        protected Matrix projection;
        protected float fieldOfView;
        protected float aspectRatio;
        protected float nearPlaneDistance;
        protected float farPlaneDistance;

        //Properties
        public Vector3 Position { get { return position; } set { position = value; } }
        public float PositionX { get { return position.X; } set { position.X = value; } }
        public float PositionY { get { return position.Y; } set { position.Y = value; } }
        public float PositionZ { get { return position.Z; } set { position.Z = value; } }
        public Vector3 Target { get { return target; } set { target = value; } }
        public Vector3 Up { get { return up; } set { up = value; } }
        public Matrix View { get { return view; } }
        public Matrix Projection { get { return projection; } }
        public float FieldOfView { get { return fieldOfView; } set { fieldOfView = value; } }
        public float AspectRatio { get { return aspectRatio; } set { aspectRatio = value; } }
        public float NearPlaneDistance { get { return nearPlaneDistance; } set { nearPlaneDistance = value; } }
        public float FarPlaneDistance { get { return farPlaneDistance; } set { farPlaneDistance = value; } }
        
        public Camera(Game game)
            : base(game)
        {
            position = Vector3.Zero;
            target = Vector3.Forward;
            up = Vector3.Up;
            fieldOfView = MathHelper.ToRadians(45.0f);
            aspectRatio = game.GraphicsDevice.Viewport.AspectRatio;
            nearPlaneDistance = 0.1f;
            farPlaneDistance = 10000f;

            projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio,
                nearPlaneDistance, farPlaneDistance);
        }

        public override void Update(GameTime gameTime)
        {
            view = Matrix.CreateLookAt(position, target, up);
        }
    }
}
