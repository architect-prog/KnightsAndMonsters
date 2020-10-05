using Game.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Enemies
{
    [RequireComponent(typeof(EnemyMovingComponent), typeof(Animator), typeof(EnemyVisionComponent))]
    public class MeleeEnemy : Unit
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyMovingComponent _movingComponent;
        [SerializeField] private EnemyVisionComponent _visionComponent;

        private void Update()
        {
            UpdateOnMove(_movingComponent.MovingDirection.x > 0.1f || _movingComponent.MovingDirection.x < -0.1f);
            //UpdateIsAgred(_movingComponent.MovingDirection.y);
            _movingComponent.Move(_visionComponent.MovingDirection.x);
        }

        public override void ApplyDamage(DamageComponent damage)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize()
        {
            base.Initialize();
            _movingComponent = GetComponent<EnemyMovingComponent>();
            _animator = GetComponent<Animator>();
            _visionComponent = GetComponent<EnemyVisionComponent>();
        }

        private void UpdateIsAgred(bool isAgred) => _animator.SetBool("isAgred", isAgred);
        private void UpdateOnMove(bool onMove) => _animator.SetBool("onMove", onMove);


    }
}


