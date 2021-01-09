using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHud : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject healthBar;
    private UiSliders healthBarSlider;

    public GameObject energyBar;
    private UiSliders energyBarSlider;

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        healthBarSlider = healthBar.GetComponent<UiSliders>();
        energyBarSlider = energyBar.GetComponent<UiSliders>();
    }

    private void Start()
    {
        healthBarSlider.SetMaxValue(playerManager.health.maxHitPoint);
        energyBarSlider.SetMaxValue(playerManager.energy.maxEnergy);
    }

    private void Update()
    {
        healthBarSlider.SetValue(playerManager.health.CurrentHitPoint);
        energyBarSlider.SetValue(playerManager.energy.currentEnergy);
    }

}
