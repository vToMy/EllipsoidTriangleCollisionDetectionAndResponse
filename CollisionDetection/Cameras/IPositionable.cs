using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BiologicalPrison.Cameras
{
    public interface IPositionable
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
    }
}
