﻿using Game.Components.AbstractComponents;
using UnityEngine;

namespace Game.Components
{
    class EnemyMovingComponent : MovingComponent
    {
        public Vector2 MovingDirection { get; private set; }
        private void FixedUpdate()
        {
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

      
        private void Flip(float yAngle)
        {
            Quaternion newRotation = transform.rotation;
            newRotation.y = yAngle;
            transform.rotation = newRotation;
        }
    }
}
