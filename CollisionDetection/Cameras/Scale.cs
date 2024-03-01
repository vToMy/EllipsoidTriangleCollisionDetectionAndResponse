using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BiologicalPrison.Misc
{
    public class Scale : GameComponent
    {
        private Vector3 scale;
        private Matrix matrix;

        public Vector3 Value { get { return scale; } set { scale = value; } }
        public Matrix Matrix { get { return matrix; } set { matrix = value; } }

        public Scale(Game game, Vector3 scale)
            : base(game)
        {
            this.scale = scale;
        }

        public Scale(Game game) : this(game, Vector3.One) { }

        public override void Update(GameTime gameTime)
        {
            matrix = Matrix.CreateScale(scale);
        }
    }
}
