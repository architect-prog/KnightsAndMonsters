using UnityEditor;
using UnityEngine;

namespace Game.Utils
{
    [RequireComponent(typeof(CircleCollider2D))]
    class RangeObjectSensor : ObjectSensor
    {
        [Header("EditorOptions")]
        [SerializeField, ColorUsage(false)] private Color _gizmosColor; 

        [Header("Vision options")]
        [SerializeField, Range(0, 360)] private float _visionDirection;
        [SerializeField, Range(0, 360)] private float _visionAngle;
        [SerializeField] private float _visionRange;

        private CircleCollider2D _circleCollider;

        protected override void Enter(Collider2D collision)
        {
            base.Enter(collision);
        }

        protected override void Exit(Collider2D collision)
        {
            base.Exit(collision);
        }

        public bool InRange()
        {
            bool result = false;
            if (Activator != null)
            {
                Vector2 targetDirection = (Activator.transform.position + Vector3.up) - transform.position;

                Vector3 endpoint = Quaternion.Euler(0, 0, _visionDirection) * transform.right;
                if (Mathf.Acos(Vector2.Dot(targetDirection.normalized, endpoint.normalized)) * Mathf.Rad2Deg < _visionAngle / 2)
                {
                    result = true;
                }
            }   
            return result;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Color color = _gizmosColor;
            color.a = 0.2f;

            Handles.color = color;
            Vector3 endpoint = Quaternion.Euler(0, 0, _visionDirection + _visionAngle / 2) * transform.right;
            Handles.DrawSolidArc(transform.position, Vector3.back, endpoint.normalized, _visionAngle, _visionRange);

            color.a = 0.8f;
            Handles.color = color;
            endpoint = Quaternion.Euler(0, 0, _visionDirection) * transform.right;
            Handles.DrawLine(transform.position, endpoint + transform.position);       
        }

        private void OnValidate()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = _visionRange;
        }
#endif
    }
}
