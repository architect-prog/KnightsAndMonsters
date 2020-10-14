using System;
using UnityEngine;

namespace Game.Utils
{
    public static class LayerMaskExtension
    {
        public static bool Includes(this LayerMask mask, int layer)
        {
            return (mask.value & 1 << layer) > 0;
        }
    }

    public static class GameObjectExtension
    {
        public static void HandleComponent<T>(this GameObject gameObject, Action<T> action)
        {
            T component = gameObject.GetComponent<T>();
            if (component != null)
            {
                action?.Invoke(component);
            }

        }
    }
}
