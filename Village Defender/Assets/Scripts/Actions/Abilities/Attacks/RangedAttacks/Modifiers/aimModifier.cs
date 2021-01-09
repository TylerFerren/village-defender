using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimModifier : MonoBehaviour
{
    public Material aimMaterial;

    private RangedAttacks attack;
    private MeshRenderer attackMesh;
    private Material attackMaterial;

    void Awake()
    {
        attack = GetComponent<RangedAttacks>();
        attackMesh = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        attackMaterial = attackMesh.material;
        attackMesh.material = aimMaterial;
    }

    public void OnRelease() {
        attackMesh.material = attackMaterial;
    }
}
