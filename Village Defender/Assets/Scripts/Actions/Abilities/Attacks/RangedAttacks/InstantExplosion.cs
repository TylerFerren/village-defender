using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class InstantExplosion : RangedAttacks
{
    [HideInInspector] public bool aiming;
    public float lifeTime;
    private float time;
    public AnimationCurve scalingCurve;

    public override void Awake() {
        base.Awake();
        attackCollider.enabled = false;  
    }

    public override void OnEnable()
    {
        base.OnEnable();
        transform.localScale = Vector3.one * size;
        attackCollider.enabled = false;
        aiming = true;
        time = 0;
        
    }

    public override void OnTriggered(GameObject caller)
    {
        performer = caller;
        energy = caller.GetComponentInChildren<Energy>();
        isUsingEnergy = true;
        BroadcastMessage("OnTrigger", null, SendMessageOptions.DontRequireReceiver);
        energy.useEnergy(this);
    }

    public override void OnReleased()
    {
        base.OnReleased();
        attackCollider.enabled = true;
        transform.localScale = Vector3.zero;
        aiming = false;
        transform.position = origin;
        isUsingEnergy = false;
        StartCoroutine(FireRate());
        fire();
        Invoke("disableObject", lifeTime);
    }

    override public void Update() {
        Aiming();
        sizeExpolode();
    } 
   
    void Aiming() {
        if (aiming)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position + (cam.transform.forward * 6), cam.transform.forward, out hit, 100, ~8))
            { origin = hit.point;
                var mesh = GetComponent<MeshFilter>();
                mesh.transform.position = Vector3.Lerp(mesh.transform.position, origin, 0.2f);
            }
            else { origin = Vector3.zero; }

        }
    }

    void sizeExpolode() {
        if (!aiming) {
            time += (1 * Time.deltaTime);
            var normalizedTime = time / lifeTime;
            transform.localScale = Vector3.Slerp(Vector3.one * 0.1f, Vector3.one * size, scalingCurve.Evaluate(normalizedTime));
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        var damagableObject = collision.gameObject.GetComponentInChildren<IDamageable>();
        if (damagableObject != null)
        { damagableObject.Damage(damage); }
    }

    private void disableObject() {
        gameObject.SetActive(false);
    }
}
