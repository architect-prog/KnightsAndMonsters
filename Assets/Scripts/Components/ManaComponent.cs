using UnityEngine;
using System.Collections;
using Game.Components.AbstractComponents;
using System;

namespace Assets.Scripts.Components
{
    public class ManaComponent : GameComponent
    {
        [SerializeField] private float _mana;
        public float Mana { get => _mana; private set => _mana = value; }
        public float MaxMana { get; private set; }

        private void Start()
        {
            MaxMana = Mana;
        }

        public void RestoreMana(float value)
        {
            if (value > 0)
            {
                Mana = Mana + value >= MaxMana ? MaxMana : Mana + value;
            }
            else            
                throw new ArgumentException("Value in restore mana must be greater than 0");            
        }

        public void DrainMana(float value)
        {
            if (value > 0)
            {
                Mana = Mana - value > 0 ? Mana - value : 0;              
            }
            else            
                throw new ArgumentException("Value in restore mana must be greater than 0");            
        }
    }
}