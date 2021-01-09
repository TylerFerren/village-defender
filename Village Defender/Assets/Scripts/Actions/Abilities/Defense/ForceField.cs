using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Defense
{
    [SerializeField] private float radius;
    [SerializeField] private Vector3 positionOffset;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * radius;
        transform.Translate(Vector3.up);
    }

    public override void OnTriggered(GameObject caller)
    {
        base.OnTriggered(caller);
        transform.SetParent(caller.transform);
    }

    public override void OnReleased()
    {
        isUsingEnergy = false;
        base.OnReleased();
        gameObject.SetActive(false);
    }
}
