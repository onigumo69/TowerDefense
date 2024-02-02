using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO ActiveBuildingType;
    }

    BuildingTypeListSO _buildingTypeList;
    BuildingTypeSO _activeBuildingType;

    void Awake()
    {
        Instance = this;

        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_activeBuildingType != null)
            {
                Instantiate(_activeBuildingType.Prefab, UtilClass.GetMouseWorldPosition(), Quaternion.identity);
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { ActiveBuildingType = _activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _activeBuildingType;
    }
}
