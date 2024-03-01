using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BiologicalPrison.Input
{
    class DefaultActionInputConfiguration : InputConfiguration
    {
        private ICollection<Keys> keys;
        private ICollection<MouseButtons> buttons;

        private Dictionary<Enum, Keys> actionsToKeys;
        private Dictionary<Enum, MouseButtons> actionsToButtons;

        public ICollection<Keys> UsedKeys { get { return keys; } }
        public ICollection<MouseButtons> UsedButtons { get { return buttons; } }

        public Dictionary<Enum, Keys> ActionsToKeys { get { return actionsToKeys; } }
        public Dictionary<Enum, MouseButtons> ActionsToButtons { get { return actionsToButtons; } }

        public DefaultActionInputConfiguration()
        {
            actionsToKeys = new Dictionary<Enum, Keys>();
            actionsToKeys.Add(ActionInputActions.Forward, Microsoft.Xna.Framework.Input.Keys.W);
            actionsToKeys.Add(ActionInputActions.Left, Keys.A);
            actionsToKeys.Add(ActionInputActions.Backward, Keys.S);
            actionsToKeys.Add(ActionInputActions.Right, Keys.D);
            actionsToKeys.Add(ActionInputActions.Jump, Keys.Space);
            actionsToKeys.Add(ActionInputActions.Up, Keys.E);
            actionsToKeys.Add(ActionInputActions.Down, Keys.Q);

            keys = (ICollection<Keys>)actionsToKeys.Values;

            actionsToButtons = new Dictionary<Enum, MouseButtons>();
            //TODO: add inserts...

            buttons = (ICollection<MouseButtons>)actionsToButtons.Values;
        }
    }
}
