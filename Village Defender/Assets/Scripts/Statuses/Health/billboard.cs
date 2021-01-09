using UnityEngine;

public class billboard : MonoBehaviour
{
    
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward); 
    }
}
