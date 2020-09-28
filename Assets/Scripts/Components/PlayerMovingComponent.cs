using Game.Components.AbstractComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public class PlayerMovingComponent : MovingComponent
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerMask _contactLayers;
        [SerializeField] private Transform _surfaceCheck;
        private ContactFilter2D _contactFilter;
        
        public override void Move()
        {
            
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
