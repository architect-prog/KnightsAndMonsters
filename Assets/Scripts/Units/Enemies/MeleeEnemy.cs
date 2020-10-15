using Game.Components;
using UnityEngine;

namespace Game.Units.Enemies
{
    [RequireComponent(typeof(EnemyMovingComponent), typeof(Animator), typeof(EnemyVisionComponent))]
    [RequireComponent(typeof(ResistComponent), typeof(DamageComponent), typeof(EnemyMeleeAttackComponent))]
    public class MeleeEnemy : Unit
    {
        private Animator _animator;
        private EnemyMovingComponent _movingComponent;
        private EnemyVisionComponent _visionComponent;
        private ResistComponent _resist;
        private DamageComponent _damage;
        private EnemyMeleeAttackComponent _attackComponent;

        public override void Initialize()
        {
            base.Initialize();
            _animator = GetComponent<Animator>();
            _movingComponent = GetComponent<EnemyMovingComponent>();
            _visionComponent = GetComponent<EnemyVisionComponent>();
            _resist = GetComponent<ResistComponent>();
            _damage = GetComponent<DamageComponent>();
            _attackComponent = GetComponent<EnemyMeleeAttackComponent>();

            _visionComponent.OnAttack += () => _animator.SetTrigger("attack");
        }

        private void Update()
        {
            UpdateOnMove(_movingComponent.MovingDirection.x > 0.1f || _movingComponent.MovingDirection.x < -0.1f);
            UpdateInCombat(_visionComponent.Target != null);

            float xSpeed = 0;
            if (_visionComponent.Target != null)
            {
                xSpeed = 0;
            }
            else
            {
                xSpeed = _visionComponent.MovingDirection.x;
            }

            _movingComponent.Move(xSpeed);
        }

        public override void ApplyDamage(DamageComponent damage)
        {
            _health.ApplyDamage(damage, _resist);
        }

        public void Attack_AnimationEvent()
        {
            _attackComponent.Attack(_damage);
        }

        private void UpdateInCombat(bool inCombat) => _animator.SetBool("inCombat", inCombat);
        private void UpdateOnMove(bool onMove) => _animator.SetBool("onMove", onMove);
    }

    public enum EnemyState
    {
        Idle, Move, inCombat
    }
}


