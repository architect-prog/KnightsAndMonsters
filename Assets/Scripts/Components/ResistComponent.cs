using Game.Components.AbstractComponents;
using Game.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public class ResistComponent : GameComponent
    {
        [SerializeField] private ResistSerializableDictionary _resists;
        public IReadOnlyDictionary<DamageType, Resist> Resists
        {
            get
            {
                Dictionary<DamageType, Resist> result = new Dictionary<DamageType, Resist>();
                foreach (DamageType key in _resists.Keys)
                {
                    result.Add(key, _resists[key]);
                }
                return result;
            }
        }

        public override void Initialize()
        {
            _resists = new ResistSerializableDictionary();
        }

        public virtual float ReduceDamage(DamageComponent damage)
        {
            float result = 0;
            foreach (DamageType type in damage.Damage.Keys)
            {
                if (Resists.ContainsKey(type))                
                    result += Resists[type] * damage.Damage[type];                
                else                
                    result += damage.Damage[type];                
            }
            return result;
        }
    }

    [Serializable]
    public struct Resist
    {
        [SerializeField, Range(-1, 1)] public float value;

        public static implicit operator float(Resist resist)
        {
            return resist.value;
        }
    }
}