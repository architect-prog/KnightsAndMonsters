using Game.Components.AbstractComponents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public class ResistComponent : GameComponent
    {
        [SerializeField] private List<Resist> _resists;
        public IReadOnlyCollection<Resist> Resists { get => _resists; }

        private void Start()
        {
            _resists = new List<Resist>();
        }

        public float ReduceDamage(DamageComponent damage)
        {
            return 0f;
        }
    }

    [Serializable]
    public struct Resist
    {
        [SerializeField] private DamageType _type;
        [SerializeField, Range(-1, 1)] private float _value;

        public DamageType Type { get => _type; }
        public float Value { get => _value; }

        public Resist(DamageType type, float value) : this()
        {
            _type = type;
            ChangeValue(value);           
        }
                
        public void ChangeValue(float value)
        {
            if (value > -1 && value < 1)
            {
                _value = value;
            }
            else
            {
                throw new ArgumentException("Value must be greater than -1 and lower than 1");
            }
        }
    }


}