using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    GameObject _spriteGameObject;
    ResourceNearbyOverlay _resourceNearbyOverlay;

    void Awake()
    {
        _spriteGameObject = transform.Find("sprite").gameObject;
        _resourceNearbyOverlay = transform.Find("ResourceNearbyOverlayPrefab").GetComponent<ResourceNearbyOverlay>();

        Hide();
    }

    void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    void Update()
    {
        transform.position = UtilClass.GetMouseWorldPosition();
    }

    void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.ActiveBuildingType == null)
        {
            Hide();
            _resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.ActiveBuildingType.Sprite);
            if(e.ActiveBuildingType.HasResourceGeneratorData)
            {
                _resourceNearbyOverlay.Show(e.ActiveBuildingType.ResourceGeneratorData);
            }
            else
            {
                _resourceNearbyOverlay.Hide();
            }
        }
    }

    void Show(Sprite ghostSprite)
    {
        _spriteGameObject.SetActive(true);
        _spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    void Hide()
    {
        _spriteGameObject.SetActive(false);
    }
}
