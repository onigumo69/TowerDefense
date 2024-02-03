using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    Dictionary<ResourceTypeSO, int> _resourceAmountDict;

    void Awake()
    {
        Instance = this;

        _resourceAmountDict = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.List)
        {
            _resourceAmountDict[resourceType] = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceTypeList.List[0], 2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceTypeList.List[1], 2);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDict[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return _resourceAmountDict[resourceType];
    }

    void TestLog()
    {
        foreach (ResourceTypeSO k in _resourceAmountDict.Keys)
        {
            Debug.Log("resourceType " + k.NameString + " Amount: " + _resourceAmountDict[k]);
        }
    }
}