using Game.Components.AbstractComponents;
using Game.Utils;
using UnityEngine;

namespace Game.Components
{
    class EnemyVisionComponent : GameComponent
    {
        [SerializeField] private bool _doubleSideVision;
        [SerializeField] private Transform _visionPoint;
        [SerializeField] private LayerMask _checkingLayers;
        [SerializeField] private float _visionRange;
        [SerializeField] private Sensor _wallSensor;
        [SerializeField] private Sensor _abyssSensor;
        private bool _changeDurection;
        private RepeatingTimer _timer;
        public Vector2 MovingDirection { get; private set; }       
        public override void Initialize()
        {
            base.Initialize();

            _changeDurection = false;

            MovingDirection = -transform.right;

            _timer = new RepeatingTimer(0.2f);
            _timer.OnTimerTriggered += ChangeDirection;
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
            //Scan();    
        }

        private void Scan()
        {
            //RaycastHit2D hit = Physics2D.Raycast(_visionPoint.position, -_visionPoint.right, _visionRange, _checkingLayers);

            RaycastHit2D cast = Physics2D.BoxCast(_visionPoint.position, Vector2.one, 0, -_visionPoint.right, _visionRange, _checkingLayers);

            if (cast.rigidbody != null)
            {
                Debug.Log(cast.rigidbody.position);
            }
            Debug.DrawLine(_visionPoint.position + Vector3.up, new Vector3(cast.rigidbody?.position.x ?? 
                _visionPoint.position.x + Vector3.left.x * _visionRange, _visionPoint.position.y + 1),
                cast.rigidbody == null ? Color.white : Color.green);
        }

        private void OnDrawGizmos()
        {
            if (_visionPoint != null)
            {
                Gizmos.color = Color.blue;
                if (_doubleSideVision)
                {
                    Gizmos.DrawLine(_visionPoint.position, new Vector3(_visionPoint.position.x + Vector3.right.x * _visionRange, _visionPoint.position.y));
                    //Gizmos.DrawWireCube(_visionPoint.position, new Vector3(_visionRange, _visionRange));
                }
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_visionPoint.position, new Vector3(_visionPoint.position.x + Vector3.left.x * _visionRange, _visionPoint.position.y));
            }           
        }
    }
}
