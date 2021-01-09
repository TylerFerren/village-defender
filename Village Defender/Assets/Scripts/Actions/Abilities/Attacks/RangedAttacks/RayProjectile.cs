using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayProjectile : RangedAttacks
{

    [SerializeField] private float spread;


    private BoxCollider boxCollider;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(spread, spread, range);
        boxCollider.center = new Vector3(0, 0, range / 2);
        
    }

    public override void OnTriggered(GameObject caster)
    {
        fire();
    }

    public override void OnReleased()
    {
        return;
    }

    
}
