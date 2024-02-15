using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform buildingConstructionPrefab = Resources.Load<Transform>("BuildingConstructionPrefab");
        Transform buildingConstructionTransform = Instantiate(buildingConstructionPrefab, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }

    float _constructionTimer;
    float _constructionTimerMax;

    BuildingTypeSO _buildingType;
    BoxCollider2D _boxCollider2D;
    SpriteRenderer _spriteRenderer;

    BuildingTypeHolder _buildingTypeHolder;
    Material _constructionMaterial;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

        _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        _constructionMaterial = _spriteRenderer.material;
    }

    private void Update()
    {
        _constructionTimer -= Time.deltaTime;

        _constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());

        if(_constructionTimer <= 0f)
        {
            Instantiate(_buildingType.Prefab, transform.position, Quaternion.identity);

            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);

            Destroy(gameObject);
        }
    }

    void SetBuildingType(BuildingTypeSO buildingType)
    {
        _buildingType = buildingType;

        _constructionTimerMax = buildingType.ConstructionTimerMax;
        _constructionTimer = buildingType.ConstructionTimerMax;

        _spriteRenderer.sprite = buildingType.Sprite;

        _boxCollider2D.offset = buildingType.Prefab.GetComponent<BoxCollider2D>().offset;
        _boxCollider2D.size = buildingType.Prefab.GetComponent<BoxCollider2D>().size;

        _buildingTypeHolder.BuildingType = buildingType;
    }

    public float GetConstructionTimerNormalized()
    {
        return 1 - _constructionTimer / _constructionTimerMax;
    }
}
