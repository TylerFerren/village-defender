using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("General")]
    public float health = 200f;

    [Header("Movement")]
    public float speed = 3f;
    public float sprint = 12f;
    public float jump = 12f;

    void Awake() {
        GetComponentInChildren<Health>().maxHitPoint = health;
    }

}
