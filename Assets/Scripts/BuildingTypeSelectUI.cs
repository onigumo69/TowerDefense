using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] Sprite ArrowSprite;
    [SerializeField] Transform ButtonTemplate;

    Dictionary<BuildingTypeSO, Transform> _btnTransformDict;
    Transform _arrowBtn;

    void Awake()
    {
        ButtonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        _btnTransformDict = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        _arrowBtn = Instantiate(ButtonTemplate, transform);
        _arrowBtn.gameObject.SetActive(true);

        float offsetAmount = 130f;
        _arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        _arrowBtn.Find("image").GetComponent<Image>().sprite = ArrowSprite;
        _arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(-40, -40);

        _arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;


        foreach (BuildingTypeSO buildingType in buildingTypeList.List)
        {
            Transform buttonTransform = Instantiate(ButtonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.Sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            _btnTransformDict[buildingType] = buttonTransform;

            index++;
        }
    }

    void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;

        UpdateActiveBuildingTypeButton();
    }

    void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    void UpdateActiveBuildingTypeButton()
    {
        _arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in _btnTransformDict.Keys)
        {
            Transform btnTransform = _btnTransformDict[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }


        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            _arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            _btnTransformDict[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
