using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectileModifer : MonoBehaviour
{
    public RangedAttacks rangedAttacks;
    
    public int CurveOffset;

    public float Curviture;
    public float rotationRange;
    public float flightPercentage;
    private float distanceToTarget;
    private float currentDistance;
    private float distanceDampiner;
    private float distanceMultiplier;

    void OnEnable()
    {
        rotationRange = Random.Range(-135, 135);
        rangedAttacks = GetComponent<RangedAttacks>();
        distanceToTarget = Vector3.Distance(rangedAttacks.origin, rangedAttacks.targetPoint);
        currentDistance = 0.1f;
        flightPercentage = 0.1f;
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(rangedAttacks.origin, rangedAttacks.targetPoint);
        currentDistance = Vector3.Distance(rangedAttacks.origin, rangedAttacks.transform.position);
        flightPercentage = currentDistance / distanceToTarget;
        Curviture = Mathf.Cos(flightPercentage * (3.14159263538979f));

        distanceMultiplier = distanceToTarget / (rangedAttacks.range/4);
        distanceDampiner = 1 / (1 + Mathf.Pow(flightPercentage - 0.5f, 4));
        
        float yOffset = ((Curviture / distanceToTarget) * distanceDampiner * distanceMultiplier * CurveOffset * rangedAttacks.speed) * Time.deltaTime;

        transform.rotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, rotationRange);
        transform.Translate(new Vector3(0, yOffset, 0), Space.Self);

    }

    
}