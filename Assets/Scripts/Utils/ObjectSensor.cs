using UnityEngine;

namespace Game.Utils
{
    public class ObjectSensor : Sensor
    {
        private GameObject _activator;
        public GameObject Activator { get => _activator; private set => _activator = value; }
        protected override void Enter(Collider2D collision)
        {
            base.Enter(collision);
            Activator = collision.gameObject;
        }

        protected override void Exit(Collider2D collision)
        {
            base.Exit(collision);
            Activator = null;
        }
    }
}