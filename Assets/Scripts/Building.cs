using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    BuildingTypeSO _buildingType;
    HealthSystem _healthSystem;

    Transform _buildingDemolishButton;

    private void Awake()
    {
        _buildingDemolishButton = transform.Find("BuildingDemolishButtonPrefab");

        HideBuildingDemolishButton();
    }

    private void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingType;

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmountMax(_buildingType.HealthAmountMax, true);

        _healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {

    }

    void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
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
}
