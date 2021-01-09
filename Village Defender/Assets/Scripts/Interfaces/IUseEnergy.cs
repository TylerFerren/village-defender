using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseEnergy 
{
    public float energyCost{ get; set;}
    public Energy.EnergyUseType energyUseType { get; set;}
    public bool usingEnergy { get; set;}
}
