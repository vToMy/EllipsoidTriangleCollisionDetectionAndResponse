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


namespace BiologicalPrison.Input
{
    public class InputService : GameComponent
    {
        //Input states
        private KeyboardState currentKeyState;
        private KeyboardState previousKeyState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        //Caches
        private Dictionary<Keys, float> keyCache;
        private Dictionary<MouseButtons, float> mouseCache;

        //Configuration
        private InputConfiguration inputConf;

        //Consts
        private String badConfigurationMessage = "The action enum does not exist in the input configuraion. " +
                "Can be caused due to wrong/incomplete configuration or incorrect use of input actions.";

        public InputService(Game game, InputConfiguration inputConf)
            : base(game)
        {
            this.inputConf = inputConf;

            keyCache = new Dictionary<Keys, float>(inputConf.UsedKeys.Count);
            mouseCache = new Dictionary<MouseButtons, float>(inputConf.UsedButtons.Count);
        }

        public override void Initialize()
        {
            foreach (Keys key in inputConf.UsedKeys)
            {
                keyCache.Add(key, 0.0f);
            }
            foreach (MouseButtons button in inputConf.UsedButtons)
            {
                mouseCache.Add(button, 0.0f);
            }

            previousKeyState = currentKeyState = Keyboard.GetState();
            previousMouseState = currentMouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)(gameTime.ElapsedGameTime.TotalSeconds);

            previousKeyState = currentKeyState;
            previousMouseState = currentMouseState;

            currentKeyState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            foreach (Keys key in inputConf.UsedKeys)
            {
                if (IsDown(key))
                    keyCache[key] += elapsedTime;
                else
                    keyCache[key] = 0.0f;
            }

            foreach (MouseButtons mouseButton in inputConf.UsedButtons)
            {
                if (IsDown(mouseButton))
                    mouseCache[mouseButton] += elapsedTime;
                else
                    mouseCache[mouseButton] = 0.0f;
            }
        }

        public void SetConfiguration(InputConfiguration inputConf)
        {
            this.inputConf = inputConf;
        }

        #region InputChecks

        public bool IsDown(Enum action)
        {
            if (inputConf.ActionsToKeys.ContainsKey(action))
            {
                return IsDown(inputConf.ActionsToKeys[action]);
            }
            else if (inputConf.ActionsToButtons.ContainsKey(action))
            {
                return IsDown(inputConf.ActionsToButtons[action]);
            }
            throw new ArgumentException(badConfigurationMessage, action.ToString());
        }

        private bool IsDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        private bool IsDown(MouseButtons mouseButton)
        {
            return MouseButtonState(currentMouseState, mouseButton, ButtonState.Pressed);
        }

        public bool IsUp(Enum action)
        {
            if (inputConf.ActionsToKeys.ContainsKey(action))
            {
                return IsUp(inputConf.ActionsToKeys[action]);
            }
            else if (inputConf.ActionsToButtons.ContainsKey(action))
            {
                return IsUp(inputConf.ActionsToButtons[action]);
            }
            throw new ArgumentException(badConfigurationMessage, action.ToString());
        }

        private bool IsUp(Keys key)
        {
            return currentKeyState.IsKeyUp(key);
        }

        private bool IsUp(MouseButtons mouseButton)
        {
            return MouseButtonState(currentMouseState, mouseButton, ButtonState.Released);
        }

        public bool IsPressed(Enum action)
        {
            if (inputConf.ActionsToKeys.ContainsKey(action))
            {
                return IsPressed(inputConf.ActionsToKeys[action]);
            }
            else if (inputConf.ActionsToButtons.ContainsKey(action))
            {
                return IsPressed(inputConf.ActionsToButtons[action]);
            }
            throw new ArgumentException(badConfigurationMessage, action.ToString());
        }

        private bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key);
        }

        private bool IsPressed(MouseButtons mouseButton)
        {
            return MouseButtonState(currentMouseState, mouseButton, ButtonState.Pressed) &&
                MouseButtonState(previousMouseState, mouseButton, ButtonState.Released);
        }

        public bool IsReleased(Enum action)
        {
            if (inputConf.ActionsToKeys.ContainsKey(action))
            {
                return IsReleased(inputConf.ActionsToKeys[action]);
            }
            else if (inputConf.ActionsToButtons.ContainsKey(action))
            {
                return IsReleased(inputConf.ActionsToButtons[action]);
            }
            throw new ArgumentException(badConfigurationMessage, action.ToString());
        }

        private bool IsReleased(Keys key)
        {
            return currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key);
        }

        private bool IsReleased(MouseButtons mouseButton)
        {
            return MouseButtonState(currentMouseState, mouseButton, ButtonState.Released) &&
                MouseButtonState(previousMouseState, mouseButton, ButtonState.Pressed);
        }

        private bool MouseButtonState(MouseState mouseState, MouseButtons mouseButton, ButtonState buttonState)
        {
            switch (mouseButton)
            {
                case MouseButtons.LeftButton:
                    return mouseState.LeftButton == buttonState;
                case MouseButtons.RightButton:
                    return mouseState.RightButton == buttonState;
                case MouseButtons.MiddleButton:
                    return mouseState.MiddleButton == buttonState;
                case MouseButtons.XButton1:
                    return mouseState.XButton1 == buttonState;
                case MouseButtons.XButton2:
                    return mouseState.XButton2 == buttonState;
            }
            throw new ArgumentException("Received unsupported mousebutton.", mouseButton.ToString());
        }

        public float ElapsedDown(Enum action)
        {
            if (inputConf.ActionsToKeys.ContainsKey(action))
            {
                return ElapsedDown(inputConf.ActionsToKeys[action]);
            }
            else if (inputConf.ActionsToButtons.ContainsKey(action))
            {
                return ElapsedDown(inputConf.ActionsToButtons[action]);
            }
            throw new ArgumentException(badConfigurationMessage, action.ToString());
        }

        private float ElapsedDown(Keys key)
        {
            return keyCache[key];
        }

        private float ElapsedDown(MouseButtons mouseButton)
        {
            return mouseCache[mouseButton];
        }
        #endregion

        public Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouseState.X, currentMouseState.Y);
            }
        }

        public Vector2 MouseDelta
        {
            get
            {
                return new Vector2(currentMouseState.X - previousMouseState.X,
                    currentMouseState.Y - previousMouseState.Y);
            }
        }

        public int MouseScrollWheelDelta
        {
            get
            {
                return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
            }
        }
    }
}