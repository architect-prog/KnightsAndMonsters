using Assets.Scripts.Components;
using Game.Components;
using Game.Components.AbstractComponents;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Character
{
    [RequireComponent(typeof(DamageComponent), typeof(ResistComponent), typeof(CharacterMovingComponent))]
    [RequireComponent(typeof(ManaComponent)), RequireComponent(typeof(Animator)), RequireComponent(typeof(CharacterMeleeAttackComponent))]
    public class Character : Unit, ICollector
    {
        private ManaComponent _mana;
        private DamageComponent _damage;
        private ResistComponent _resist;
        private CharacterMovingComponent _movingComponent;
        private Animator _animator;
        private CharacterMeleeAttackComponent _attackComponent;

        public override void Initialize()
        {
            base.Initialize();
            _mana = GetComponent<ManaComponent>();
            _damage = GetComponent<DamageComponent>();
            _resist = GetComponent<ResistComponent>();
            _movingComponent = GetComponent<CharacterMovingComponent>();
            _animator = GetComponent<Animator>();
            _attackComponent = GetComponent<CharacterMeleeAttackComponent>();

            _movingComponent.OnJump += SetJumpTrigger;
            _attackComponent.OnAttack += SetAttackAnimation;
        }

        private void Update()
        {
            UpdateOnGround(_movingComponent.OnGround());
            UpdateOnMove(_movingComponent.MovingDirection.x > 0.1f || _movingComponent.MovingDirection.x < -0.1f);
            UpdateYSpeed(_movingComponent.MovingDirection.y);
        }

        public override void ApplyDamage(DamageComponent damage)
        {
            _health.ApplyDamage(damage, _resist);
        }      

        public void Take(ICollactable collactable)
        {

        }
    
        private void SetJumpTrigger() => _animator.SetTrigger("onJump");
        private void UpdateOnGround(bool onGround) => _animator.SetBool("onGround", onGround);
        private void UpdateOnMove(bool isMove) => _animator.SetBool("isMove", isMove);
        private void UpdateYSpeed(float ySpeed) => _animator.SetFloat("ySpeed", ySpeed);        

        private void SetAttackAnimation(int condition)
        {
            _animator.SetTrigger($"attack{condition}");
        }

        private void OnDisable()
        {
            _movingComponent.OnJump -= SetJumpTrigger;
            _attackComponent.OnAttack -= SetAttackAnimation;
        }
    }
}

