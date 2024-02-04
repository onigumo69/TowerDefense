using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    BuildingTypeSO _buildingType;

    HealthSystem _healthSystem;

    private void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingType;

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetHealthAmountMax(_buildingType.HealthAmountMax, true);

        _healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            _healthSystem.Damage(10);
        }
    }

    void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
