using Game.Components.AbstractComponents;
using Game.Utils;
using UnityEditor;
using UnityEngine;

namespace Game.Components
{
    class EnemyVisionComponent : GameComponent
    {        
        [Header("Scanning settings")]
        [SerializeField] private ObjectSensor _targetSensor;
        [SerializeField, Range(0, 360)] private float _visionDirection;
        [SerializeField, Range(0, 360)] private float _visionAngle;
        [SerializeField] private float _visionRange;
        [SerializeField] private LayerMask _checkingLayers;

        [Header("Moving sensors")]
        [SerializeField] private Sensor _wallSensor;
        [SerializeField] private Sensor _abyssSensor;

        [Header("Scanning time")]
        [SerializeField] private float _checkingTime;

        [SerializeField] private GameObject _target;

        private bool _changeDurection;
        private RepeatingTimer _timer;
        public Vector2 MovingDirection { get; private set; }       
        public override void Initialize()
        {
            base.Initialize();

            _changeDurection = false;

            MovingDirection = -transform.right;

            _timer = new RepeatingTimer(_checkingTime == 0 ? 0.1f : _checkingTime);
            _timer.OnTimerTriggered += ChangeDirection;
            //_timer.OnTimerTriggered += Scan;
            _timer.StartTimer(this);
        }

        public void ChangeDirection()
        {
            _changeDurection = _wallSensor.Collide;
            if (!_changeDurection)
            {
                _changeDurection = _abyssSensor.NotCollide;
            }

            if (_changeDurection)
            {
                MovingDirection = -MovingDirection;
            }
        }

        private void Update()
        {
             Scan();    
        }

        private void Scan()
        {
            if (_targetSensor.Collide)
            {
                Vector2 targetDirection = (_targetSensor.Activator.transform.position + Vector3.up) - _targetSensor.transform.position;

                Vector3 endpoint = Quaternion.Euler(0, 0, _visionDirection) * -_targetSensor.transform.right;
                if (Mathf.Acos(Vector2.Dot(targetDirection.normalized, endpoint.normalized)) * Mathf.Rad2Deg < _visionAngle / 2) 
                {
                    //Debug.Log(Mathf.Acos(Vector2.Dot(targetDirection.normalized, endpoint.normalized)) * Mathf.Rad2Deg);
                    Debug.DrawLine(_targetSensor.transform.position, targetDirection + (Vector2)_targetSensor.transform.position);
                } 

            }
        }

        private void OnDisable()
        {
            _timer.OnTimerTriggered -= ChangeDirection;
            _timer.StopTimer(this);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_targetSensor != null)
            {                
                Handles.color = new Color(0, 1.0f, 0, 0.2f);
                Vector3 endpoint = Quaternion.Euler(0, 0, _visionDirection + _visionAngle / 2) * -_targetSensor.transform.right;               
                Handles.DrawSolidArc(_targetSensor.transform.position, Vector3.back, endpoint.normalized, _visionAngle, _visionRange);

                endpoint = Quaternion.Euler(0, 0, _visionDirection) * -_targetSensor.transform.right;
                Handles.DrawLine(_targetSensor.transform.position, endpoint + _targetSensor.transform.position);

                Handles.color = new Color(1.0f, 0, 0, 0.1f);
                Handles.DrawSolidDisc(_targetSensor.transform.position, Vector3.back, 1);
            }
        }
#endif
    }
}
