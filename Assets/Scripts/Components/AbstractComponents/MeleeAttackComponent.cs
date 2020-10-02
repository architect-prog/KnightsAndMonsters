using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

namespace Game.Components.AbstractComponents
{
    public abstract class MeleeAttackComponent : MonoBehaviour, IInitializable
    {
        private void Start()
        {
            Initialize();
        }
        public abstract void Attack(DamageComponent damage);
        public virtual void Initialize() { }
    }
}