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
}
