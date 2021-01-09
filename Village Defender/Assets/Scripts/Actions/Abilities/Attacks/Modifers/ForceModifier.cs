using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceModifier : MonoBehaviour
{
    public float force;
    public float verticalForce;
    public float impactZone;

    private SphereCollider Collider;
    private Attacks attack;

    public enum ForceType {impact, explosive}
    public ForceType forceType;

    private void Start()
    {
        Collider = GetComponent<SphereCollider>();
        attack = GetComponent<Attacks>();
        impactZone = attack.size;
    }

    public void OnTriggerEnter(Collider other)
    {
        var rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            var distance = Vector3.Distance(transform.position, other.transform.position);
            var forceFalloff = Mathf.Cos(distance/(impactZone) * (1.57079f));

            switch(forceType)
            {
                default:
                    break;
                case ForceType.explosive:
                    rigidbody.AddExplosionForce(force * forceFalloff, transform.position, impactZone / 2, verticalForce);
                    break;
                case ForceType.impact:
                    rigidbody.AddForce(transform.forward  * force , ForceMode.Impulse);
                    break;
            }
        }
    }



}
