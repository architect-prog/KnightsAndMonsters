using UnityEditor;
using UnityEngine;

namespace Game.Utils
{
    class RangeObjectSensor : ObjectSensor
    {
        [SerializeField, Range(0, 360)] private float _visionDirection;
        [SerializeField, Range(0, 360)] private float _visionAngle;
        [SerializeField] private float _visionRange;

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
            Handles.color = new Color(0, 1.0f, 0, 0.2f);
            Vector3 endpoint = Quaternion.Euler(0, 0, _visionDirection + _visionAngle / 2) * transform.right;
            Handles.DrawSolidArc(transform.position, Vector3.back, endpoint.normalized, _visionAngle, _visionRange);

            endpoint = Quaternion.Euler(0, 0, _visionDirection) * transform.right;
            Handles.DrawLine(transform.position, endpoint + transform.position);

            Handles.color = new Color(1.0f, 0, 0, 0.1f);
            Handles.DrawSolidDisc(transform.position, Vector3.back, 1);            
        }
#endif
    }
}
