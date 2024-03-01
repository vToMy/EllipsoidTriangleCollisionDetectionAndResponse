using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BiologicalPrison.Input;
using BiologicalPrison.Misc;

namespace BiologicalPrison.Cameras
{
    public class ThirdPersonCamera : Camera
    {
        private IPositionable positionable;

        //Camera ajustments
        private Vector3 targetOffset;
        private Zoom zoom;

        //Rotations
        private Rotation rotation;

        //Services
        private InputService input;
        
        //Public members
        public float RotationX { get { return rotation.X; } set { rotation.X = value; } }
        public float RotationY { get { return rotation.Y; } set { rotation.Y = value; } }
        public float RotationZ { get { return rotation.Z; } set { rotation.Z = value; } }

        public ThirdPersonCamera(Game game, IPositionable target, Vector3 zoom)
            : this(game, target, Vector3.Zero, zoom, 1) { }

        public ThirdPersonCamera(Game game, IPositionable target, Vector3 targetOffset, Vector3 zoom)
            : this(game, target, targetOffset, zoom, 1) { }

        public ThirdPersonCamera(Game game, IPositionable target, Vector3 targetOffset, Vector3 zoomDirection, float zoomLength)
            : base(game)
        {
            this.positionable = target;
            this.target = positionable.Position;
            this.targetOffset = targetOffset;

            zoom = new Zoom(game)
            {
                Direction = zoomDirection,
                Length = zoomLength,
                Delay = 0.1f,
                Speed = 1 / 30f
            };

            rotation = new Rotation(game) { Delay = 0.2f, Velocity = new Vector3(0.02f) };
        }

        protected void ProcessInput()
        {
            input = (InputService)Game.Services.GetService(typeof(InputService));

            rotation.X -= input.MouseDelta.Y * rotation.Velocity.X;
            rotation.Y -= input.MouseDelta.X * rotation.Velocity.Y;

            zoom.ProcessInput();
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput();
            
            rotation.Update(gameTime);
            zoom.Update(gameTime);
            target = positionable.Position + targetOffset;

            position = target + Vector3.Transform(zoom.Direction * zoom.CurrentLength, rotation.Quaternion);
            up = Vector3.Transform(Vector3.Up, rotation.Quaternion);

            base.Update(gameTime);
        }
    }
}
