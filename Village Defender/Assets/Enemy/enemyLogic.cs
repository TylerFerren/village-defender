using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLogic : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rb;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //rb.AddForce(transform.TransformVector(transform.forward) * moveSpeed * Time.deltaTime, ForceMode.Force);
    }
}
