using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string NameString;
    public GameObject Prefab;
    public ResourceGeneratorData ResourceGeneratorData;

    public Sprite Sprite;
}
