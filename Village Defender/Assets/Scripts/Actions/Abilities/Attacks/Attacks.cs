using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : Abilities
{

    [SerializeField] protected float damage;
    
    [SerializeField] public float size;
    [HideInInspector] public GameObject target;
    
    public override void Awake() {
        base.Awake();
    }

    public override void OnTriggered(GameObject caller)
    {
        base.OnTriggered(caller);
    }

    public override void OnReleased()
    {
        base.OnReleased();
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject != performer)
        {
            var damagableObject = collision.gameObject.GetComponentInChildren<IDamageable>();
            if (damagableObject != null)
            {
                damagableObject.Damage(damage);
            }
         
        }
    }
}
