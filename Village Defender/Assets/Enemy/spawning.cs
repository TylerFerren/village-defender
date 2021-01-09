using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawning : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    [SerializeField][Tooltip("Number of spawns per minute")] private float spawnRate;
    [SerializeField] private bool Spawn = true;
    private float spawnTimer = 0f;


    void Start()
    {
        
    }

    
    void Update()
    {
        if (Spawn) {
            spawnTimer += 1 * Time.deltaTime;

            if (spawnTimer >= 60 / spawnRate) {
                Instantiate(spawn, (transform.position + new Vector3(0, 2, 0)), Quaternion.identity);
                spawnTimer = 0f;
            }
        }
    }
}
