using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    GameObject _spriteGameObject;

    void Awake()
    {
        _spriteGameObject = transform.Find("sprite").gameObject;

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
        }
        else
        {
            Show(e.ActiveBuildingType.Sprite);
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
