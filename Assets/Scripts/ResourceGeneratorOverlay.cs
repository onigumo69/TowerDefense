using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] ResourceGenerator ResourceGenerator;

    Transform _barTransform;

    private void Start()
    {
        _barTransform = transform.Find("bar");

        ResourceGeneratorData resourceGeneratorData = ResourceGenerator.GetResourceGeneratorData();

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.ResourceType.Sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(ResourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        _barTransform.localScale = new Vector3(1 - ResourceGenerator.GetTimerNormalized(), 0.5f, 1);
    }
}
