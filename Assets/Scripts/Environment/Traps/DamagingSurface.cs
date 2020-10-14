using Game.Components;
using Game.Units;
using UnityEngine;

namespace Game.Environment.Traps
{
    public class DamagingSurface : MonoBehaviour
    {
        [SerializeField] private DamageComponent _damage;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Unit unit = collision.GetComponent<Unit>();
            if (unit != null)
            {
                unit.ApplyDamage(_damage);
                OnUnitDamaged(collision);
            }
        }

        protected virtual void OnUnitDamaged(Collider2D damagedUnit) { }
    }
}
