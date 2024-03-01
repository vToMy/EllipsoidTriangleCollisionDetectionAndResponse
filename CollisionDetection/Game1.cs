using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using BiologicalPrison.Cameras;
using BiologicalPrison.Input;

namespace CollisionDetection
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        //Entities
        CollisionEntity entity;

        //Triangels
        VertexPositionColor[] vertices;
        VertexDeclaration vertexDeclaration;
        BasicEffect basicEffect;

        //Service
        InputService input;
        Camera camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            /*graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1024;
            graphics.IsFullScreen = true;*/
        }

        protected override void Initialize()
        {
            input = new InputService(this, new DefaultActionInputConfiguration());
            entity = new CollisionEntity(this);
            camera = new ThirdPersonCamera(this, entity, new Vector3(0, 0, 0), new Vector3(0, 1, 1), 10);

            Components.Add(input);
            Components.Add(entity);
            Components.Add(camera);
            

            Services.AddService(typeof(InputService), input);
            Services.AddService(typeof(Camera), camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Kootenay");

            vertices = new VertexPositionColor[6];
            vertices[0] = new VertexPositionColor(new Vector3(2, 0, -5), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(0, 3, -5), Color.Green);
            vertices[2] = new VertexPositionColor(new Vector3(-2, 0, -5), Color.Blue);

            vertices[3] = new VertexPositionColor(new Vector3(0, -3, -5), Color.Red);
            vertices[4] = new VertexPositionColor(new Vector3(5, -3, 5), Color.Green);
            vertices[5] = new VertexPositionColor(new Vector3(-5, -3, 5), Color.Blue);

            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);
            basicEffect = new BasicEffect(GraphicsDevice, new EffectPool());

            Triangle[] triangles = new Triangle[vertices.Length / 3];
            for (int i = 0; i < vertices.Length; i += 3)
            {
                triangles[i / 3] = new Triangle(vertices[i].Position, vertices[i + 1].Position, vertices[i + 2].Position);
            }

            entity.Triangles = triangles;

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (input.IsPressed(ActionInputActions.Jump))
            {
                OrientedCollision.gravityOn ^= true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.RenderState.CullMode = CullMode.None;

            base.Draw(gameTime);

            DrawTriangles();

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.SaveState);
            string output = "Gravity: " + (OrientedCollision.gravityOn ? "On" : "Off");
            spriteBatch.DrawString(font, output, Vector2.Zero, Color.LightGreen);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawTriangles()
        {
            basicEffect.EnableDefaultLighting();
            basicEffect.VertexColorEnabled = true;
            basicEffect.World = Matrix.Identity;
            basicEffect.View = camera.View;
            basicEffect.Projection = camera.Projection;
            basicEffect.PreferPerPixelLighting = true;
            basicEffect.AmbientLightColor = Color.White.ToVector3();

            basicEffect.Begin();
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Begin();

                GraphicsDevice.VertexDeclaration = vertexDeclaration;
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);

                pass.End();
            }
            basicEffect.End();
        }
    }
}
