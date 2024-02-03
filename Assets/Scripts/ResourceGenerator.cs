using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    ResourceGeneratorData _resourceGeneratorData;
    float _timer;
    float _timerMax;

    void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().BuildingType.ResourceGeneratorData;
        _timerMax = _resourceGeneratorData.TimerMax;
    }

    void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.ResourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if(resourceNode != null)
            {
                nearbyResourceAmount++;
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.MaxResourceAmount);

        if(nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.TimerMax / 2f) +
                _resourceGeneratorData.TimerMax * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.MaxResourceAmount);
        }

        Debug.Log("nearbyResourceAmount " + nearbyResourceAmount + " timerMax " + _timerMax);
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;

            ResourceManager.Instance.AddResource(_resourceGeneratorData.ResourceType, 1);
        }
    }
}
