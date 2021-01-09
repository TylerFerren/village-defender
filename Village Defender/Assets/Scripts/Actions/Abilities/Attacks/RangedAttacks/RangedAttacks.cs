using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedAttacks : Attacks
{
    public float range;
    public float speed;

    [HideInInspector] public Vector3 origin; //staring point of the projectile
    [HideInInspector] public Vector3 targetPoint; //target destination of the projectile
    [HideInInspector] public float distanceTravled; //current distance from origin
    [HideInInspector]public float timeToImpact; // time till impact
    protected Camera cam;
    protected SphereCollider attackCollider;

    public override void Awake()
    {
        cam = Camera.main;
        attackCollider = GetComponent<SphereCollider>();
        transform.localScale *= size;
    }

    public virtual void Setup() {
        //called when pooled
    }

    public virtual void OnEnable()
    {
        distanceTravled = 0;
        //UnityEditor.Selection.activeGameObject = this.gameObject;
    }

    public virtual void Update()
    {
        flightPath();
        transform.localScale = Vector3.one * size;
    }

        public void flightPath() {
        //calculate current distance and time to impact
            float distanceCheck;
                if (targetPoint != null)
                { distanceCheck = Vector3.Distance(transform.position, targetPoint); }
                else { distanceCheck = Vector3.Distance(transform.position, origin + (transform.forward * range)); }
            timeToImpact = distanceCheck / speed;

            distanceTravled = Vector3.Distance(origin, transform.position);
                if (distanceTravled > range && range > 0)
                { gameObject.SetActive(false); }
        }

        public void flightVector()
        {
        //calculate the direction of the projectile
            LayerMask layerMask = LayerMask.GetMask("Player", "Projectile");
            RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000, ~layerMask))
                {
                    transform.LookAt(hit.point);
                    target = hit.transform.gameObject;
                    targetPoint = hit.point;
                }
                else {
                        transform.LookAt(cam.transform.forward * 100);
                        targetPoint = (transform.forward * range) + transform.position;
                }
        }

        public virtual void fire()
        {
            origin = transform.position;
            flightVector();
            StartCoroutine(SimpleProjectile());
        }

        public IEnumerator SimpleProjectile()
        {
            while (distanceTravled < range)
            {
                transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
                yield return null;
            }
        }


    public RangedAttacks RangedAttack(AttackHandler handler, ref RangedAttacks prefab, RangedAttacks currentAttack, InputAction.CallbackContext ctx)
    {
        if (handler.playerManager.energy.currentEnergy - energyCost > 0 && !prefab.coolingdown)
        {
            if (ctx.started)
            {
                GameObject attack = ObjectPooler.SharedInstance.GetPooledObject(tag);
                attack.transform.position = handler.transform.position;
                attack.transform.rotation = handler.transform.rotation;
                attack.SetActive(true);

                currentAttack = attack.GetComponent<RangedAttacks>();
                currentAttack.performer = handler.caster;
                return currentAttack;
            }


            if (currentAttack != null)
            {

                if (ctx.performed)
                {
                    currentAttack.OnTriggered(handler.caster);
                    return currentAttack;
                }
                if (ctx.canceled)
                {
                    currentAttack.OnReleased();
                }
            }
            return currentAttack;
        }
        return null;
    }

    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject != performer)
        {
            gameObject.SetActive(false);
        }
    }

}
