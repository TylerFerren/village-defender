using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectileModifer : MonoBehaviour
{
    public RangedAttacks rangedAttacks;
    public float rotation;

    private Camera cam;
    private GameObject target;

    void Start()
    {
        cam = Camera.main;
        rangedAttacks = GetComponent<RangedAttacks>();
    }

    private void OnTrigger()
    {
        LayerMask layerMask = LayerMask.GetMask("Player", "Projectile");
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000, ~layerMask))
        {
            if (hit.transform.gameObject)
            {
                target = hit.transform.gameObject;
                print(target);
            }
        }
    }

    void Update()
    {
        var targetDirection = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, targetDirection, rotation * Time.deltaTime));
    }
}
