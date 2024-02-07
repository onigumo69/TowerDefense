using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] BuildingConstruction BuildingConstruction;

    Image _constructionProgressImage;

    private void Awake()
    {
        _constructionProgressImage = transform.Find("mask").Find("image").GetComponent<Image>();
    }

    private void Update()
    {
        _constructionProgressImage.fillAmount = BuildingConstruction.GetConstructionTimerNormalized();
    }
}
