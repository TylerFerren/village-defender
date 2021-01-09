using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingProjectile : RangedAttacks
{
    [SerializeField] private float chargeTime = 3;
    private float currentCharge;
    private bool charging = false;

    public override void OnEnable()
    {
        base.OnEnable();
        currentCharge = 0;
    }

    public override void OnTriggered(GameObject caller)
    {
        isUsingEnergy = true;
        base.OnTriggered(caller);
        charging = true;
        StartCoroutine(Charging());
    }

    public override void OnReleased()
    {
        base.OnReleased();
        charging = false;
        isUsingEnergy = false;
        fire();
        StartCoroutine(SimpleProjectile());

    }

    IEnumerator Charging()
    {
        while (charging && currentCharge <= 1)
        {
            currentCharge += 1 * Time.deltaTime / chargeTime;
            size = 1 * currentCharge;
            yield return null;
        }
        isUsingEnergy = false;
        damage *= currentCharge;
    }
}
