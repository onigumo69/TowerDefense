using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] Transform ResourceTemplate;


    ResourceTypeListSO _resourceTypeList;
    Dictionary<ResourceTypeSO, Transform> _resourceTypeTransformDict;

    void Awake()
    {
        ResourceTemplate.gameObject.SetActive(false);

        _resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        _resourceTypeTransformDict = new Dictionary<ResourceTypeSO, Transform>();

        int index = 0;
        foreach (ResourceTypeSO resourceType in _resourceTypeList.List)
        {
            Transform resourceTransform = Instantiate(ResourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.Sprite;

            _resourceTypeTransformDict[resourceType] = resourceTransform;

            index++;
        }
    }

    void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;

        UpdateResourceAmount();
    }

    void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceTypeList.List)
        {
            Transform resourceTransform = _resourceTypeTransformDict[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
