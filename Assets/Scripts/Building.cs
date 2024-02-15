using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    BuildingTypeSO _buildingType;
    HealthSystem _healthSystem;

    Transform _buildingDemolishButton;
    Transform _buildingRepairButton;

    private void Awake()
    {
        _buildingDemolishButton = transform.Find("BuildingDemolishButtonPrefab");
        _buildingRepairButton = transform.Find("BuildingRepairButtonPrefab");

        HideBuildingDemolishButton();
        HideBuildingRepairButton();
    }

    private void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingType;

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmountMax(_buildingType.HealthAmountMax, true);

        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        _healthSystem.OnHealed += HealthSystem_OnHealed;
        _healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        ShowBuildingRepairButton();

        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        if(_healthSystem.IsFullHealth())
        {
            HideBuildingRepairButton();
        }
    }

    private void Update()
    {

    }

    void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);

        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishButton();
    }

    private void OnMouseExit()
    {
        HideBuildingDemolishButton();
    }

    void ShowBuildingDemolishButton()
    {
        if(_buildingDemolishButton != null)
        {
            _buildingDemolishButton.gameObject.SetActive(true);
        }
    }

    void HideBuildingDemolishButton()
    {
        if (_buildingDemolishButton != null)
        {
            _buildingDemolishButton.gameObject.SetActive(false);
        }
    }

    void ShowBuildingRepairButton()
    {
        if (_buildingRepairButton != null)
        {
            _buildingRepairButton.gameObject.SetActive(true);
        }
    }

    void HideBuildingRepairButton()
    {
        if (_buildingRepairButton != null)
        {
            _buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
