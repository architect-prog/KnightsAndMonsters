using System;
using UnityEngine;
using Game.Utils;
using Game.Components.AbstractComponents;

namespace Game.Components
{
    class EnemyMeleeAttackComponent : MeleeAttackComponent
    {
        [SerializeField] Transform _attackPointCenter;
        [SerializeField] private float _attackRadius;
        [SerializeField] private LayerMask _damagableLayers;
        public override void Attack(DamageComponent damage)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPointCenter.position, _attackRadius, _damagableLayers);

            foreach (Collider2D collider in colliders)
            {
                collider.gameObject.HandleComponent<HealthComponent>(component => component.ApplyDamage(damage));
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_attackPointCenter != null)
            {
                Gizmos.DrawWireSphere(_attackPointCenter.position, _attackRadius);
            }
        }
    }
}
