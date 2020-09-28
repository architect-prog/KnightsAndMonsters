using Game.Components.AbstractComponents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public class DamageComponent : GameComponent
    {
        [SerializeField] private List<Damage> _damage;
        public IReadOnlyCollection<Damage> Damage { get => _damage; }

        public float PureDamage 
        { 
            get
            {
                float result = 0f;
                foreach (Damage damage in Damage)
                {
                    result += damage.Value;
                }
                return result;
            }
        }

        private void Start()
        {
            _damage = new List<Damage>();
        }      
    }


    [Serializable]
    public struct Damage
    {
        [SerializeField] private DamageType _type;
        [SerializeField] private float _value;

        public DamageType Type { get => _type; }
        public float Value { get => _value; }

        public Damage(DamageType type, float value)
        {
            _type = type;
            _value = value;
        }

        public void ChangeValue(float value)
        {
            _value = value;
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