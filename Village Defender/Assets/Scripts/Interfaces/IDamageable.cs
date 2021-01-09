using UnityEngine;

public interface IDamageable
{
    void Damage(float damage);
    public float MaxHitPoint { get; set; }
    public float CurrentHitPoint { get; set; }

}
