using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] HealthSystem HealthSystem;
    [SerializeField] ResourceTypeSO GoldResourceType;

    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = HealthSystem.GetHealthAmountMax() - HealthSystem.GetHealthAmount();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[]
            {
                new ResourceAmount{ResourceType = GoldResourceType, Amount = repairCost}
            };

            if(ResourceManager.Instance.CanAfford(resourceAmountCost))
            {
                ResourceManager.Instance.SpendResource(resourceAmountCost);
                HealthSystem.HealFull();
            }
            else
            {
                TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer { Timer = 2f });
            }
        });
    }
}
