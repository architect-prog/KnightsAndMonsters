using Game.Components.AbstractComponents;
using Game.Inputs;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

namespace Game.Components
{
    public class CharacterMovingComponent : MovingComponent
    {
        public event Action OnJump;

        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _contactLayers;
        [SerializeField] private Transform _surfaceCheck;
        [SerializeField] private float _checkRadius;

        //private ContactFilter2D _contactFilter;
        private PlayerInputs _input;

        public Vector2 MovingDirection { get; private set ; }

        private void Awake()
        {
            _input = new PlayerInputs();
            _input.Player.Jump.started += Jump;
        }     

        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }

        private void FixedUpdate()
        {       
            Move(_input.Player.Move.ReadValue<Vector2>().x);
            MovingDirection = _rigidbody.velocity;
        }

        public override void Move(float xDirection)
        {
            Vector2 direction = new Vector2(xDirection * Speed, _rigidbody.velocity.y);        
            _rigidbody.velocity = direction;

            if (_rigidbody.velocity.x > 0)            
                Flip(0);            
            else if (_rigidbody.velocity.x < 0)            
                Flip(180);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (OnGround())
            { 
                _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
                OnJump?.Invoke();
            }
        }

        public bool OnGround()
        {
            //List<Collider2D> colliders = new List<Collider2D>();
            //int count = _rigidbody.GetContacts(_contactFilter, colliders);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_surfaceCheck.position, _checkRadius, _contactLayers);
            return colliders.Length >= 1;           
        }

        public override void Initialize()
        {
            base.Initialize();
            //_contactFilter = new ContactFilter2D();
            //_contactFilter.useTriggers = false;
            //_contactFilter.SetLayerMask(_contactLayers);
            //_contactFilter.useLayerMask = true;
        }

        private void Flip(float yAngle)
        {
            Quaternion newRotation = transform.rotation;
            newRotation.y = yAngle;            
            transform.rotation = newRotation;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            if (_surfaceCheck != null)
            {
                Gizmos.DrawWireSphere(_surfaceCheck.position, _checkRadius);
            }
        }
    }
}