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
        }

        private void Update()
        {
            UpdateOnMove(_movingComponent.MovingDirection.x > 0.1f || _movingComponent.MovingDirection.x < -0.1f);
            //UpdateIsAgred(_movingComponent.MovingDirection.y);
            _movingComponent.Move(_visionComponent.MovingDirection.x);
        }

        public override void ApplyDamage(DamageComponent damage)
        {
            _health.ApplyDamage(damage, _resist);
        }

        public void Attack_AnimationEvent()
        {
            _attackComponent.Attack(_damage);
        }

        private void UpdateIsAgred(bool isAgred) => _animator.SetBool("isAgred", isAgred);
        private void UpdateOnMove(bool onMove) => _animator.SetBool("onMove", onMove);

    }
}


