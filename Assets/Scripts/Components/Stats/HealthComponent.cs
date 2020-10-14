using Game.Components.AbstractComponents;
using System;
using UnityEngine;

namespace Game.Components
{
    public sealed class HealthComponent : GameComponent
    {
        public event EventHandler<EventArgs> OnDie;
        [SerializeField] private float _health;
        public float Health { get => _health; private set => _health = value; }
        public float MaxHealth { get; private set; }
      
        public override void Initialize()
        {
            MaxHealth = Health;
        }

        public void ApplyDamage(DamageComponent damage, ResistComponent resist = null)
        {
            float resultDamage = resist == null ? damage.PureDamage : resist.ReduceDamage(damage);
            Health -= resultDamage;

            if (Health < 0)
            {
                OnDie?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Heal(float value)
        {
            Health = Health + value >= MaxHealth ? MaxHealth : Health + value;
        }
    }
}


