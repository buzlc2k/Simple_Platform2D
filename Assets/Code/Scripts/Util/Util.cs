using UnityEngine;

namespace Platformer2D
{
    public static class Util{
        public static Vector2 DirectionFromAngle2D(float angleInDegrees)
        {
            float radians = (angleInDegrees + 90) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
        }   
    }
}