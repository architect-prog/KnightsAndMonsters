using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.AbstractComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class MovingComponent : GameComponent, IInitializable
    {
        [SerializeField] private float _speed;
        protected Rigidbody2D _rigidbody;
               
        public float Speed { get => _speed; } 
        private void Start()
        {
            Initialize();
        }
        public virtual void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        public abstract void Move(float xAxis);
    }

}
