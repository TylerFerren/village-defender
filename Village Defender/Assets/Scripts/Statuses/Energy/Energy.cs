using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float maxEnergy = 100;
    public float currentEnergy;
    [Tooltip("Charge per second")] public float rechargeRate = 5;
    public float stamina = 3;
    

    public enum EnergyUseType{instant, drain, exponential };

    public bool recharging;
    public bool chargeTimer;
    private bool draining;


    public void Awake() {
        
        currentEnergy = maxEnergy;
        recharging = true;
    }
    
    public void Update()
    {
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        if (currentEnergy < maxEnergy && recharging)
        {
            currentEnergy += rechargeRate * Time.deltaTime;
        }
        if (!recharging & !chargeTimer) {
            StopCoroutine(RechargeTimer());
            StartCoroutine(RechargeTimer());
        }
    }

    IEnumerator RechargeTimer()
    {
        chargeTimer = true;
        yield return new WaitForSeconds(stamina);
        recharging = true;
        chargeTimer = false;
    }

    public void useEnergy(IUseEnergy caller)
    {
        switch (caller.energyUseType)
        {
            default:
                break;
                
            case EnergyUseType.instant:
                useInstantEnergy(caller.energyCost);
                break;

            case EnergyUseType.drain:
                StartCoroutine(useEnergyOverTime(caller.energyCost, caller));
                break;

            case EnergyUseType.exponential:
                useExponetialEnergy(caller.energyCost, stamina);
                break;

        }
    }

    public void useInstantEnergy(float energyCost)
    {
        currentEnergy -= energyCost;
        recharging = false;
    }

    public IEnumerator useEnergyOverTime(float energyCost, IUseEnergy caller) {
        while (caller.usingEnergy)
        {
            currentEnergy -= energyCost * Time.deltaTime;
            recharging = false;
            yield return null;
        }
        recharging = false;
    }

    float lastUse = 0f;
    float energyCost;
    public void useExponetialEnergy(float initialCost, float recoveryTime) {
        
        float used = Time.time;
        if (lastUse != 0)
        {
            var timeElapsed = used - lastUse;
            var costScale = Mathf.Pow(10/recoveryTime, -timeElapsed);
            print(costScale);
            energyCost = Mathf.Lerp(initialCost, (energyCost * 2), costScale);
        }
        else { energyCost = initialCost; }
       
        currentEnergy -= energyCost;
        recharging = false;
        lastUse = used;
    }

}
