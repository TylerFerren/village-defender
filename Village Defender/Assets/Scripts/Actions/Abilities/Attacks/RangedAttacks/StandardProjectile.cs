using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : RangedAttacks
{
    
    public override void OnTriggered(GameObject caller)
    {
        base.OnTriggered(caller);
        fire();
        StartCoroutine(SimpleProjectile());
    }

    public override void OnReleased() {
        return;
    }
}
