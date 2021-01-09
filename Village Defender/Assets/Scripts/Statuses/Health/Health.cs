using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHitPoint = 100f; 
    public float MaxHitPoint { get => maxHitPoint; set => maxHitPoint = value; }
    public float currentHitPoint;
    public float CurrentHitPoint { get => currentHitPoint; set => currentHitPoint = value; }

    public GameObject healthBarCanvas;
    private  GameObject healthBar;
    private UiSliders healthBarSlider;

    public Vector3 HealthBarOffset;

    void Awake()
    {
        currentHitPoint = maxHitPoint;
        if (healthBarCanvas)
        {
            healthBar = Instantiate(healthBarCanvas, transform.position + HealthBarOffset, Quaternion.identity);
            healthBar.transform.SetParent(transform);
            healthBarSlider = healthBar.GetComponentInChildren<UiSliders>();
            healthBarSlider.SetMaxValue(CurrentHitPoint);
        }
    }

    public void Damage(float damage) {
        currentHitPoint -= damage;

        if (currentHitPoint < 0) {
            currentHitPoint = 0;
            Death();
        }
        if (healthBarCanvas)
            healthBarSlider.SetValue(currentHitPoint);
    }

    public void Death() {
        Destroy(gameObject);
    }

    private void Update()
    {
        if(healthBar != null)
            healthBar.transform.position = transform.position + HealthBarOffset;
    }
}
