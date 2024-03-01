using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BiologicalPrison.Input;

namespace BiologicalPrison.Cameras
{
    class FirstPersonCamera : Camera
    {
        //The initial directions to which the rotation is applied.
        private Vector3 referenceForward;
        private Vector3 referenceLeft;

        private Vector3 rotation;
        private Vector3 rotationSpeed = new Vector3(0.02f);

        private float speed = 4;

        //Services
        private InputService input;

        public FirstPersonCamera(Game game) : base(game)
        {
            referenceForward = Vector3.Forward;
            referenceLeft = Vector3.Left;
        }

        public FirstPersonCamera(Game game, Vector3 position) : this(game)
        {
            this.position = position;
        }

        public FirstPersonCamera(Game game, Vector3 position, Vector3 target)
            : this (game, position)
        {
            referenceForward = target - position;
            referenceForward.Normalize();

            referenceLeft = Vector3.Cross(up, referenceForward);
            referenceLeft.Normalize();
        }

        public override void Update(GameTime gameTime)
        {
            input = (InputService)Game.Services.GetService(typeof(InputService));

            rotation.X -= input.MouseDelta.Y * rotationSpeed.X;
            rotation.Y -= input.MouseDelta.X * rotationSpeed.Y;

            Matrix rotationMatrix = Matrix.CreateRotationX(rotation.X) *
                                    Matrix.CreateRotationY(rotation.Y) *
                                    Matrix.CreateRotationZ(rotation.Z);

            Vector3 forward = Vector3.Transform(referenceForward, rotationMatrix);
            Vector3 left = Vector3.Transform(referenceLeft, rotationMatrix);

            if (input.IsDown(ActionInputActions.Forward))
            {
                position += forward * speed;
            }
            if (input.IsDown(ActionInputActions.Backward))
            {
                position -= forward * speed;
            }
            if (input.IsDown(ActionInputActions.Left))
            {
                position += left * speed;
            }
            if (input.IsDown(ActionInputActions.Right))
            {
                position -= left * speed;
            }

            target = position + forward;

            base.Update(gameTime);
        }
    }
}
