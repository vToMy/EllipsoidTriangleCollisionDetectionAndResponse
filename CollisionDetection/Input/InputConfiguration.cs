using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BiologicalPrison.Input
{
    /// <summary>
    /// An interface that contains the input configuration for a scene.
    /// </summary>
    public interface InputConfiguration
    {
        /// <summary> The keys used by the scene. </summary>
        ICollection<Keys> UsedKeys { get; }
        /// <summary> The buttons used by the scene. </summary>
        ICollection<MouseButtons> UsedButtons { get; }

        /// <summary> The map converting a logical action to a key. </summary>
        Dictionary<Enum, Keys> ActionsToKeys { get; }
        /// <summary> The map converting a logical action to a button. </summary>
        Dictionary<Enum, MouseButtons> ActionsToButtons { get; }
    }
}
