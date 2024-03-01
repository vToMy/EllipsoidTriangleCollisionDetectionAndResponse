using Microsoft.Xna.Framework;
using BiologicalPrison.Input;

namespace BiologicalPrison.Cameras
{
    class Zoom : GameComponent
    {
        private Vector3 direction;
        private float length;
        private float currentLength;
        private float speed;
        private float delay;

        public Vector3 Direction { get { return direction; } set { direction = value; direction.Normalize(); } }
        public float Length { get { return length; } set { length = value; } }
        public float CurrentLength { get { return currentLength; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public float Delay { get { return delay; } set { delay = value; } }

        public Zoom(Game game)
            : base(game)
        {
            speed = 1f;
            delay = 1f;
        }

        public void ProcessInput()
        {
            InputService input = (InputService)Game.Services.GetService(typeof(InputService));
            length -= input.MouseScrollWheelDelta * speed;
        }

        public override void Update(GameTime gameTime)
        {
            currentLength = MathHelper.Lerp(currentLength, length, delay);
        }
    }
}
