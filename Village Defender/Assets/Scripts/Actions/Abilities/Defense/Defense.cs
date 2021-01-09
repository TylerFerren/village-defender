using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : Abilities, IDamageable
{
    protected Health health;
    public float maxHitPoint;
    public float MaxHitPoint { get => maxHitPoint; set => maxHitPoint = value; }
    public float currentHitPoint;
    public float CurrentHitPoint { get => currentHitPoint; set => currentHitPoint = value; }

    public override void Awake()
    {
        health = gameObject.AddComponent<Health>();
        base.Awake();
        health.MaxHitPoint = maxHitPoint;
    }

    public void Damage(float damage)
    {
        health.Damage(damage);
    }
}
