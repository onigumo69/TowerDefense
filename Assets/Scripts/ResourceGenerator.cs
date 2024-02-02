using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    BuildingTypeSO _buildingType;
    float _timer;
    float _timerMax;

    void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingType;
        _timerMax = _buildingType.ResourceGeneratorData.TimerMax;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;

            ResourceManager.Instance.AddResource(_buildingType.ResourceGeneratorData.ResourceType, 1);
        }
    }
}
