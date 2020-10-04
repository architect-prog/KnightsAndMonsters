using UnityEngine;
using System.Collections;
using Game.Components.AbstractComponents;
using Game.Components;
using System;
using UnityEngine.InputSystem;
using Game.Inputs;

namespace Assets.Scripts.Components
{
    public class CharacterMeleeAttackComponent : MeleeAttackComponent
    {
        public event Action<int> OnAttack;
        [SerializeField] private Transform _attackPointCenter;
        [SerializeField] private float _attackRadius;
        private PlayerInputs _input;

        private void Awake()
        {
            _input = new PlayerInputs();
            _input.Player.Attack.started += Attack_started;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Attack_started(InputAction.CallbackContext obj)
        {
            Attack(null);
        }

        public override void Attack(DamageComponent damage)
        {
            OnAttack?.Invoke(1);
        }

        public override void Initialize()
        {
            base.Initialize();

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