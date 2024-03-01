using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BiologicalPrison.Misc
{
    public class Translation : GameComponent
    {
        private Vector3 position;
        private Matrix matrix;

        public Vector3 Value { get { return position; } set { position = value; } }
        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        public float Z { get { return position.Z; } set { position.Z = value; } }
        public Matrix Matrix { get { return matrix; } set { matrix = value; } }

        public Translation(Game game, Vector3 position)
            : base(game)
        {
            this.position = position;
        }

        public Translation(Game game) : this(game, Vector3.Zero) { }

        public override void Update(GameTime gameTime)
        {
            matrix = Matrix.CreateTranslation(position);
        }
    }
}
