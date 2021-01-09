using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectile : RangedAttacks
{
    public ParticleSystem particle;
    private List<ParticleCollisionEvent> collisionEvents;
    public float spread;
    public float lifeTime;

    private Transform parent;
    
    public override void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        cam = Camera.main;
        parent = transform.parent;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        origin = transform.position;
        collisionEvents = new List<ParticleCollisionEvent>();
        isUsingEnergy = true;
    }

    public override void OnTriggered(GameObject caster)
    {
        transform.SetParent(caster.transform);
        base.OnTriggered(caster);
        particle.Play();
    }

    public override void OnReleased()
    {
        transform.SetParent(parent);
        particle.Stop();
        isUsingEnergy = false;
    }

    private void OnParticleCollision(GameObject collision)
    {
        if (collision.gameObject != performer)
        {
            var target = collision.gameObject.GetComponent<Health>();
            if (target)
            { target.Damage(damage); }
        }
        

        //print("hit");
        //if (collision != null)
        //{
        //    int numCollisionEvents = particle.GetCollisionEvents(collision, collisionEvents);

        //    int i = 0;

        //    while (i < numCollisionEvents)
        //    {
        //        if (collision.gameObject != performer)
        //        {
        //            var target = collision.gameObject.GetComponent<Health>();
        //            if (target)
        //            { target.damage(damage); }
        //        }
        //        i++;
        //    }
        //}
    }

    public override void OnTriggerEnter(Collider other)
    {
        return;
    }

    
    public override void Update()
    {
        flightVector();
        var distance = Vector3.Distance(transform.position, targetPoint);

        Collider[] hitColliders = Physics.OverlapSphere(targetPoint, 2);

        var particleShape = particle.shape;

        particleShape.angle = (particle.main.startSpeed.constant + hitColliders.Length) /(distance/2);

        if ( energy.currentEnergy < 0f)
        {
            OnReleased();
        }
    }
}
