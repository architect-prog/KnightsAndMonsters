using Game.Components.AbstractComponents;
using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;

namespace Game.Components
{
    [Serializable]
    public sealed class DamageComponent : GameComponent
    {
        [SerializeField] private DamageSerializableDictionary _damage;
        public IReadOnlyDictionary<DamageType, float> Damage 
        { 
            get
            {
                Dictionary<DamageType, float> result = new Dictionary<DamageType, float>();
                foreach (DamageType key in _damage.Keys)
                {
                    result.Add(key, _damage[key]);
                }
                return result;
            }
        }

        public float PureDamage 
        { 
            get
            {
                float result = 0f;
                foreach (float damage in Damage.Values)
                {
                    result += damage;
                }
                return result;
            }
        }

        public override void Initialize()
        {
            _damage = new DamageSerializableDictionary();
        }
    }

    public enum DamageType
    {
        Physycs,
        Fire,
        Ice,
        Darkness,
        Poison
    }
}