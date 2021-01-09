using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHandler : MonoBehaviour
{
    public List<Abilities> Abilities;

    public Abilities PrimaryAbility;
    public Abilities SecondaryAbility;
    public Abilities TertiaryAbility;

    private GameObject caster;

    public Transform RightHand;
    public Transform leftHand;

    public PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        caster = transform.root.gameObject;

        Abilities.Add(PrimaryAbility);
        Abilities.Add(SecondaryAbility);
        Abilities.Add(TertiaryAbility);

        foreach (Abilities ability in Abilities)
        {
            
        }
    }


    Abilities primaryAbility;
    public void _OnPrimaryAttack(InputAction.CallbackContext ctx)
    {
        primaryAbility = Ability(PrimaryAbility, primaryAbility, ctx);
    }

    Abilities secondaryAbility;
    public void _OnSecondaryAttack(InputAction.CallbackContext ctx)
    {
        secondaryAbility = Ability(SecondaryAbility, secondaryAbility, ctx);
    }

    Abilities tertiaryAbility;
    public void _OnTertiaryAttack(InputAction.CallbackContext ctx)
    {
        tertiaryAbility = Ability(TertiaryAbility, tertiaryAbility, ctx);
    }


    private Abilities Ability(Abilities prefabAbility, Abilities currentAbility, InputAction.CallbackContext ctx)
    {
        if (playerManager.energy.currentEnergy - prefabAbility.energyCost > 0 && !prefabAbility.coolingdown)
        {
            if (ctx.started)
            {
                GameObject ability = ObjectPooler.SharedInstance.GetPooledObject(prefabAbility.tag);
                ability.transform.position = transform.position;
                ability.transform.rotation = transform.rotation;
                ability.SetActive(true);

                currentAbility = ability.GetComponent<Abilities>();
                currentAbility.performer = caster;
            }
        }

        if (currentAbility != null)
        {
            if (ctx.performed)
            {
                currentAbility.OnTriggered(caster);
            }
            if (ctx.canceled)
            {
                currentAbility.OnReleased();
                currentAbility = null;
                StartCoroutine(prefabAbility.FireRate());
            }
        }
        return currentAbility;
    }

    public void _OnRange(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            foreach (Abilities ability in Abilities)
            {
                //rangedAttacks.Setup();
                if (ability != null)
                    ability.gameObject.SetActive(false);
            }
        }
    }
}
