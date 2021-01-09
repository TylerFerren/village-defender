using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : Actions, IUseEnergy
{
    [SerializeField] protected float EnergyCost;
    public float energyCost { get => EnergyCost; set => EnergyCost = value; }
    [SerializeField] protected Energy.EnergyUseType EnergyUseType;
    public Energy.EnergyUseType energyUseType { get => EnergyUseType; set => EnergyUseType = value; }
    protected bool isUsingEnergy;
    public bool usingEnergy { get => isUsingEnergy; set => isUsingEnergy = value; }


    protected Energy energy;

    [SerializeField] protected float coolDown;
    [HideInInspector] public bool coolingdown;

    public virtual void Awake()
    {
        coolingdown = false;
    }

    public override void OnTriggered(GameObject caller)
    {
        base.OnTriggered(caller);
        energy = caller.GetComponentInChildren<Energy>();
        isUsingEnergy = true;
        energy.useEnergy(this);
    }

    public IEnumerator FireRate()
    {
        coolingdown = true;
        yield return new WaitForSeconds(coolDown);
        coolingdown = false;
    }
}
