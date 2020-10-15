using UnityEngine;
using System.Collections;
using Game.Components.AbstractComponents;
using Game.Components;
using System;
using UnityEngine.InputSystem;
using Game.Inputs;
using Game.Utils;

namespace Assets.Scripts.Components
{
    public sealed class CharacterMeleeAttackComponent : MeleeAttackComponent
    {


        public event Action OnAttack;
        [SerializeField] private Transform _attackPointCenter;
        [SerializeField] private float _attackRadius;
        [SerializeField] private LayerMask _damagableLayers;
        [SerializeField] private float _discardAttackSeriesTime;
        [SerializeField] private int _attackSeriesCount;

        private PlayerInputs _input;
        private Timer _discardAttackNumber;
        private int _currentAttack;

        public int CurrentAttack { get => _currentAttack; }

        private void Awake()
        {
            _input = new PlayerInputs();
            _input.Player.Attack.started += Attack;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Player.Attack.started -= Attack;
            _input.Disable();
        }

        private void Attack(InputAction.CallbackContext obj)
        {
            _discardAttackNumber.StopTimer(this);

            _currentAttack = _currentAttack >= _attackSeriesCount ? _currentAttack = 1 : _currentAttack + 1;
            OnAttack?.Invoke();

            _discardAttackNumber.StartTimer(this);
        }

        public override void Attack(DamageComponent damage)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPointCenter.position, _attackRadius, _damagableLayers);
            foreach (Collider2D collider in colliders)
            {
                collider.gameObject.HandleComponent<HealthComponent>(component => component.ApplyDamage(damage));
            }
        }

        private void DiscardAttackSeries()
        {
            _currentAttack = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
            _currentAttack = 0;
            _discardAttackNumber = new Timer(_discardAttackSeriesTime);

            _discardAttackNumber.OnTimerTriggered += DiscardAttackSeries;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_attackPointCenter != null)
            {
                Gizmos.DrawWireSphere(_attackPointCenter.position, _attackRadius);
            }
        }
#endif
    }
}