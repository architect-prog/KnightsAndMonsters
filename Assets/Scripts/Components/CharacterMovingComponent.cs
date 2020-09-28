using Game.Components.AbstractComponents;
using Game.Inputs;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Components
{
    public class CharacterMovingComponent : MovingComponent
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _contactLayers;
        [SerializeField] private Transform _surfaceCheck;
        private ContactFilter2D _contactFilter;
        private PlayerInputs _input;

        private void Awake()
        {
            _input = new PlayerInputs();
        }

        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            Move();          
        }

        public override void Move()
        {
            float XAxis = _input.Player.Move.ReadValue<Vector2>().x;
            Vector2 direction = new Vector2(XAxis * Speed, _rigidbody.velocity.y);        
            _rigidbody.velocity = direction;
        }

        public void Jump()
        {
            if (OnGround())
            {
                //_rigidbody.
            }
        }

        public bool OnGround()
        {
            List<Collider2D> colliders = new List<Collider2D>();
            int count = _rigidbody.GetContacts(_contactFilter, colliders);
            return count > 0;           
        }

        public override void Initialize()
        {
            base.Initialize();
            _contactFilter = new ContactFilter2D();
            _contactFilter.useTriggers = false;
            _contactFilter.SetLayerMask(_contactLayers);
            _contactFilter.useLayerMask = true;
        }
    }
}
