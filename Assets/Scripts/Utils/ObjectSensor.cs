using UnityEngine;
using System.Collections;

namespace Game.Utils
{
    public sealed class ObjectSensor : Sensor
    {
        [SerializeField] private GameObject _activator;
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