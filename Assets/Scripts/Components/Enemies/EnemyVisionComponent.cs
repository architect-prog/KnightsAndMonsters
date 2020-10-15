using Game.Components.AbstractComponents;
using Game.Utils;
using System;
using UnityEngine;

namespace Game.Components
{
    class EnemyVisionComponent : GameComponent
    {
        public Action OnAttack;

        [Header("Sensors")]
        [SerializeField] private RangeObjectSensor _targetSensor;
        [SerializeField] private RangeObjectSensor _attackSensor;
        [SerializeField] private Sensor _wallSensor;
        [SerializeField] private Sensor _abyssSensor;

        [Header("Time options")]
        [SerializeField] private float _checkingTime;
        [SerializeField] private float _memoryTime;
                
        private RepeatingTimer _repeatActions;
        private Timer _looseTimerTarget;
        private GameObject _target;

        public Vector2 MovingDirection { get; private set; }  
        public GameObject Target { get => _target; }
        public override void Initialize()
        {
            base.Initialize();

            MovingDirection = -transform.right;

            _repeatActions = new RepeatingTimer(_checkingTime == 0 ? 0.1f : _checkingTime);
            _repeatActions.OnTimerTriggered += ChangeDirection;
            _repeatActions.OnTimerTriggered += ScanTargets;
            _repeatActions.StartTimer(this);

            _looseTimerTarget = new Timer(_memoryTime);
            _looseTimerTarget.OnTimerTriggered += LooseTarget;
        }

        public void ChangeDirection()
        {
            if (_wallSensor.Collide || _abyssSensor.NotCollide)
            {
                MovingDirection = -MovingDirection;
            }
        }

        private void Update()
        {
            if (_target != null)
            {
                Debug.DrawLine(_target.transform.position, transform.position);
            }

            if (_attackSensor.InRange())
            {
                OnAttack?.Invoke();
            }
        }

        private void ScanTargets()
        {
            if (_targetSensor.Collide && _targetSensor.InRange())
            {
                if (_target == null)
                {
                    _target = _targetSensor.Activator;
                    _looseTimerTarget.StopTimer(this);
                }          
            }
            else
            {
                _looseTimerTarget.StartTimer(this);
            }
        }

        private void LooseTarget()
        {
            if (_target == null)
                return;

            _target = null;
        }

        private void OnDisable()
        {
            _repeatActions.OnTimerTriggered -= ChangeDirection;
            _repeatActions.OnTimerTriggered -= ScanTargets;
            _repeatActions.StopTimer(this);

            _looseTimerTarget.OnTimerTriggered -= LooseTarget;
            _looseTimerTarget.StopTimer(this);
        }
    }
}
