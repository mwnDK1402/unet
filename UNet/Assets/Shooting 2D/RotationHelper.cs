namespace Shooting2D
{
    using UnityEngine;

    internal static class RotationHelper
    {
        public static float TransformToPhysicsRotation(Transform transform)
        {
            return -Mathf.Atan2(transform.up.x, transform.up.y) * Mathf.Rad2Deg;
        }

        public static float DirectionToPhysicsRotation(Vector2 direction)
        {
            return -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
    }
}