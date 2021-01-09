using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AttackHandler : MonoBehaviour
{
    [HideInInspector] public List<RangedAttacks> rangedAttacks;

    public Attacks lightAttack;
    public Attacks heavyAttack;

    public RangedAttacks PrimaryRange;
    public RangedAttacks SecondaryRange;
    public RangedAttacks TertiaryRange;

    public GameObject caster;

    public Transform RightHand;
    public Transform leftHand;

    public PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        caster = transform.root.gameObject;
        
        rangedAttacks.Add(PrimaryRange);
        rangedAttacks.Add(SecondaryRange);
        rangedAttacks.Add(TertiaryRange);

        foreach (RangedAttacks rangedAttacks in rangedAttacks) {
            rangedAttacks.Setup();
        }
    }

    RangedAttacks primaryAttack;
    
    public void _OnPrimaryAbility(InputAction.CallbackContext ctx)
    {
        primaryAttack = PrimaryRange.RangedAttack(this, ref PrimaryRange, primaryAttack, ctx);
        if (ctx.canceled) { StartCoroutine(PrimaryRange.FireRate()); }
    }

    RangedAttacks secondaryAttack;
    public void _OnSecondaryAbility(InputAction.CallbackContext ctx)
    {
        secondaryAttack = SecondaryRange.RangedAttack(this, ref SecondaryRange, secondaryAttack, ctx); ;
        if (ctx.canceled)
        {
            StartCoroutine(SecondaryRange.FireRate());
        }
    }

    RangedAttacks tertiaryAttack;
    public void _OnTertiaryAbility(InputAction.CallbackContext ctx)
    {
        tertiaryAttack = TertiaryRange.RangedAttack(this, ref TertiaryRange, tertiaryAttack, ctx); ;
        if (ctx.canceled)
        {
            StartCoroutine(SecondaryRange.FireRate());
        }
    }


    //private RangedAttacks RangedAttack(RangedAttacks prefabAttack, RangedAttacks currentAttack, InputAction.CallbackContext ctx)
    //{
    //        if (playerManager.energy.currentEnergy - prefabAttack.energyCost > 0 && !prefabAttack.coolingdown)
    //        {
    //            if (ctx.started)
    //            {
    //                GameObject attack = ObjectPooler.SharedInstance.GetPooledObject(prefabAttack.tag);
    //                attack.transform.position = transform.position;
    //                attack.transform.rotation = transform.rotation;
    //                attack.SetActive(true);

    //                currentAttack = attack.GetComponent<RangedAttacks>();
    //                currentAttack.performer = caster;
    //            }
    //        }

    //        if (currentAttack != null)
    //        {
    //            if (ctx.performed)
    //            {
    //                currentAttack.OnTriggered(caster);
                    
    //            }
    //            if (ctx.canceled)
    //            {
    //                currentAttack.OnReleased();
    //                currentAttack = null;
    //                StartCoroutine(prefabAttack.FireRate());
    //            }
    //    }
    //    return currentAttack;
    //}

    public void _OnRange(InputAction.CallbackContext ctx) {
        if (ctx.canceled) {
            foreach (RangedAttacks rangedAttacks in rangedAttacks)
            {
                rangedAttacks.Setup();
                if (rangedAttacks != null)
                    rangedAttacks.gameObject.SetActive(false);
            }
        }
    }

}
