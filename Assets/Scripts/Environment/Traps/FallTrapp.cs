using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrapp : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private new ParticleSystem particleSystem;
    private bool wasTouched = false;
    private int damage = 0;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        Unit unit = collision.collider.GetComponent<Unit>();
        //if (unit)        
        //    unit.ReceiveDamage(damage); 
        //else        
        //    particleSystem.Play();
        //Destroy(GetComponentInChildren<SpriteRenderer>());
        //Destroy(gameObject, 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasTouched)
        {            
            Unit unit = collision.GetComponent<Unit>();
            if (unit)
            {
                wasTouched = true;
                if (rigidbody2D)               
                    rigidbody2D.gravityScale = 3;
            }
        }
    }
}
