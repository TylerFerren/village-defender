using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamage 
{

    //public IItem.ItemType Type { get; set; }
    public int Amount { get; set;}

    public float damageAmount { get; set; }
    public enum WeaponType {Sword, Axe}
    public WeaponType weaponType;

    void Start()
    {
        damageAmount = 15f;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Health health = other.GetComponent<Health>();
        if (!other.tag.Contains("Player"))
        {
            if (health != null) {
                health.Damage(damageAmount);
            }

        }
    }

}
