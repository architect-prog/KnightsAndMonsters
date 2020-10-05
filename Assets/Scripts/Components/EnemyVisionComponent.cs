using Game.Components.AbstractComponents;
using UnityEngine;

namespace Game.Components
{
    class EnemyVisionComponent : GameComponent
    {
        [SerializeField] private bool _doubleSideVision;
        [SerializeField] private Transform _visionPoint;
        [SerializeField] private Transform _wallCheckPoint;
        [SerializeField] private Transform _abyssCheckPoint;
        [SerializeField] private LayerMask _checkingLayers;
        [SerializeField] private LayerMask _hardSurfaces;
        [SerializeField] private float _visionRange;
        [SerializeField] private float _wallcheckRadius;
        [SerializeField] private float _abyssCheckRadius;

        public Vector2 MovingDirection { get; private set; }
        protected bool CheckAbyss()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_abyssCheckPoint.position, _abyssCheckRadius, _hardSurfaces);
            return colliders.Length == 0;
        }

        protected bool CheckWall()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_wallCheckPoint.position, _wallcheckRadius, _hardSurfaces);
            return colliders.Length > 0;
        }

        public override void Initialize()
        {
            base.Initialize();
            MovingDirection = -transform.right;
        }

        private void Update()
        {
            Scan();
            if (CheckWall() || CheckAbyss())
            {
                MovingDirection = -MovingDirection;
            }
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

            if (_abyssCheckPoint != null) 
            {
                Gizmos.DrawWireSphere(_abyssCheckPoint.position, _abyssCheckRadius);
            }
            if (_wallCheckPoint != null)
            {
                Gizmos.DrawWireSphere(_wallCheckPoint.position, _wallcheckRadius);
            }
        }
    }
}
