using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    ResourceGeneratorData _resourceGeneratorData;
    float _timer;
    float _timerMax;

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.ResourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if(resourceNode.ResourceType == resourceGeneratorData.ResourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.MaxResourceAmount);

        return nearbyResourceAmount;
    }

    void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().BuildingType.ResourceGeneratorData;
        _timerMax = _resourceGeneratorData.TimerMax;
    }

    void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData, transform.position);

        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timerMax = (_resourceGeneratorData.TimerMax / 2f) +
                _resourceGeneratorData.TimerMax * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.MaxResourceAmount);
        }
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

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return _timer / _timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / _timerMax;
    }
}
