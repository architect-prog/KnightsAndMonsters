using UnityEngine;

namespace Game.Components.AbstractComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class MovingComponent : GameComponent
    {
        [SerializeField] private float _speed;
        protected Rigidbody2D _rigidbody;               
        public float Speed { get => _speed; }     
        
        public override void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public abstract void Move(float xAxis);
    }
}
