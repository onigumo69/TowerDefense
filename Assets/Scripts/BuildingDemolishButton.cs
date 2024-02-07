using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] Building Building;

    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingTypeSO buildingType = Building.GetComponent<BuildingTypeHolder>().BuildingType;
            foreach(ResourceAmount resourceAmount in buildingType.ConstructionResourceCostArray)
            {
                ResourceManager.Instance.AddResource(resourceAmount.ResourceType, Mathf.FloorToInt(resourceAmount.Amount * 0.6f));
            }

            Destroy(Building.gameObject);
        });
    }
}
