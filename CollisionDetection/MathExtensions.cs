using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionDetection
{
    public static class MathExtensions
    {
        /// <summary>
        /// Solves a quadratic equation. The results are returned sorted by value.
        /// </summary>
        /// <param name="a">Parameter a of the equation</param>
        /// <param name="b">Parameter b of the equation</param>
        /// <param name="c">Parameter c of the equation</param>
        /// <returns>
        /// A nulable Vector2 with the two solutions sorted by value,
        /// or null if there is no solution.
        /// </returns>
        public static Vector2? SolveQuadricEquation(float a, float b, float c)
        {
            float delta = b * b - 4 * a * c;
            if (delta < 0.0f)
            {
                return null;
            }
            float sqrtD = (float)Math.Sqrt(delta);
            float r1 = (-b + sqrtD) / (2 * a);
            float r2 = (-b - sqrtD) / (2 * a);

            MinMax(ref r1, ref r2);

            return new Vector2(r1, r2);
        }

        /// <summary>
        /// Swaps the numbers if min > max.
        /// </summary>
        /// <param name="min">After calling the method - the lower number of the two</param>
        /// <param name="max">After calling the method - the higher number of the two</param>
        public static void MinMax(ref float min, ref float max)
        {
            if (min > max)
            {
                float temp = max;
                max = min;
                min = temp;
            }
        }

        /// <summary>
        /// Checks whether the given number is between min (inclusive) and max (inclusive).
        /// </summary>
        /// <param name="num">The number to check if in range</param>
        /// <param name="min">The minimum of the range</param>
        /// <param name="max">The maximum of the range</param>
        /// <returns>True if the given number is in range, false otherwise</returns>
        public static bool InRange(this float num, float min, float max)
        {
            return num >= min && num <= max;
        }
    }
}
