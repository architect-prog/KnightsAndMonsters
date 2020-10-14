using Game.Components.AbstractComponents;
using Game.Utils;
using UnityEngine;

namespace Game.Components
{
    class EnemyVisionComponent : GameComponent
    {        
        [Header("Sensors")]
        [SerializeField] private RangeObjectSensor _targetSensor;
        [SerializeField] private Sensor _wallSensor;
        [SerializeField] private Sensor _abyssSensor;

        [Header("Time options")]
        [SerializeField] private float _checkingTime;
        [SerializeField] private float _memoryTime;
                
        private RepeatingTimer _repeatActions;
        private Timer _looseTimerTarget;
        private GameObject _target;

        public Vector2 MovingDirection { get; private set; }       
        public override void Initialize()
        {
            base.Initialize();

            MovingDirection = -transform.right;

            _repeatActions = new RepeatingTimer(_checkingTime == 0 ? 0.1f : _checkingTime);
            _repeatActions.OnTimerTriggered += ChangeDirection;
            _repeatActions.OnTimerTriggered += Scan;
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

        private void Scan()
        {
            if (_targetSensor.Collide && _targetSensor.InRange())
            {               
                _target = _targetSensor.Activator;
                _looseTimerTarget.StopTimer(this);                
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
            _repeatActions.OnTimerTriggered -= Scan;
            _repeatActions.StopTimer(this);

            _looseTimerTarget.OnTimerTriggered -= LooseTarget;
            _looseTimerTarget.StopTimer(this);
        }
    }
}
