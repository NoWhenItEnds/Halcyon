using System;
using Godot;

namespace Halcyon.Utilities
{
    /// <summary> Represents 8-point direction. </summary>
    public enum Direction
    {
        E = 0,
        SE = 1,
        S = 2,
        SW = 3,
        W = 4,
        NW = 5,
        N = 6,
        NE = 7
    }


    /// <summary> Helper functions for working with the direction enum. </summary>
    public static class DirectionExtensions
    {
        /// <summary> Convert from a direction vector. </summary>
        /// <param name="vector"> The raw vector. </param>
        /// <returns> The direction represented as an enum. </returns>
        public static Direction ToDirection(this Vector2 vector)
        {
            Double angle = vector.Angle();
            if (angle < 0)
            {
                angle += 2 * Math.PI;
            }
            Int32 index = (Int32)Math.Round(angle / (Math.PI / 4)) % 8;
            return (Direction)index;
        }
    }
}
