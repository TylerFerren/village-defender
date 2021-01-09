using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CircularMovement : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    void Start()
    {
        
    }

    [System.Obsolete]
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.RotateAroundLocal(Vector3.up, rotateSpeed * Time.deltaTime);
        
    }


}
