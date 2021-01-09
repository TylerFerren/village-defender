using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    [HideInInspector] public GameObject performer;

    
    public virtual void OnTriggered(GameObject caller) {
        performer = caller;
        BroadcastMessage("OnTrigger", null,SendMessageOptions.DontRequireReceiver);
    }

    public virtual void OnReleased() {
        BroadcastMessage("OnRelease", null, SendMessageOptions.DontRequireReceiver);
    }
}
