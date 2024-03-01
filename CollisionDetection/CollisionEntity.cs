using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BiologicalPrison.Cameras;
using BiologicalPrison.Input;

namespace CollisionDetection
{
    class CollisionEntity : DrawableGameComponent, IPositionable
    {
        private Model model;
        private Triangle[] triangles;
        private Vector3 position = Vector3.Zero;
        private Vector3 velocity = Vector3.Zero;
        private float speed = 0.1f;
        private BoundingSphere boundingSphere;
        private BoundingSphere transformedSphere;
        private OrientedBoundingEllipsoid ellipsoid;

        private Matrix world;

        public Vector3 Position { get { return position; } set { position = value; } }
        public Vector3 Forward { get { return world.Forward; } }
        public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
        //public BoundingEllipsoid Ellipsoid { get { return ellipsoid; } set { ellipsoid = value; } }
        public Triangle[] Triangles { get { return triangles; } set { triangles = value; } }

        public CollisionEntity(Game game)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            loadModelWithBoundingSphere();
            SetMatrices();
        }

        private void loadModelWithBoundingSphere()
        {
            model = Game.Content.Load<Model>("sphere");

            BoundingSphere completeBoundingSphere = new BoundingSphere();
            foreach (ModelMesh mesh in model.Meshes)
            {
                BoundingSphere origMeshSphere = mesh.BoundingSphere;
                completeBoundingSphere = BoundingSphere.CreateMerged(completeBoundingSphere, origMeshSphere);
            }

            boundingSphere = completeBoundingSphere;
            ellipsoid = new OrientedBoundingEllipsoid(completeBoundingSphere);
        }

        private void ProcessInput()
        {
            InputService input = (InputService)Game.Services.GetService(typeof(InputService));
            velocity = Vector3.Zero;

            if (input.IsDown(ActionInputActions.Forward))
            {
                velocity += Vector3.Forward * speed;
            }
            if (input.IsDown(ActionInputActions.Backward))
            {
                velocity += Vector3.Backward * speed;
            }
            if (input.IsDown(ActionInputActions.Left))
            {
                velocity += Vector3.Left * speed;
            }
            if (input.IsDown(ActionInputActions.Right))
            {
                velocity += Vector3.Right * speed;
            }
            if (input.IsDown(ActionInputActions.Up))
            {
                velocity += Vector3.Up * speed;
            }
            if (input.IsDown(ActionInputActions.Down))
            {
                velocity += Vector3.Down * speed;
            }
        }

        private void SetMatrices()
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            world = transforms[model.Meshes[0].ParentBone.Index] *
                        Matrix.CreateScale(0.01f) * //0.02 0.005 0.01
                        //Matrix.CreateRotationX(MathHelper.PiOver4) *
                        Matrix.CreateTranslation(position);

            ellipsoid.World = world;
            transformedSphere = boundingSphere.Transform(world);
        }

        public override void Update(GameTime gameTime)
        {
            ProcessInput();

            //velocity = OrientedCollision.CollideAndSlide(ellipsoid, velocity, triangles);
            velocity = SphereTrianglesResponse.CollideAndSlide(transformedSphere, velocity, triangles);

            position += velocity;

            SetMatrices();
        }

        public override void Draw(GameTime gameTime)
        {
            Camera camera = (Camera)Game.Services.GetService(typeof(Camera));

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    //effect.TextureEnabled = true;
                    effect.DiffuseColor = Color.White.ToVector3();
                    effect.VertexColorEnabled = true;
                    effect.World = world;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }
                mesh.Draw();
            }
        }
    }
}
