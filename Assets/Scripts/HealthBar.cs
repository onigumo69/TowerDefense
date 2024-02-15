using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem HealthSystem;

    Transform _barTransform;
    Transform _separatorContainer;

    private void Awake()
    {
        _barTransform = transform.Find("bar");
        _separatorContainer = transform.Find("separatorContainer");
    }

    private void Start()
    {
        ConstructHealthBarSeparators();

        HealthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;
        HealthSystem.OnDamaged += HealthSystem_OnDamaged;
        HealthSystem.OnHealed += HealthSystem_OnHealed;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e)
    {
        ConstructHealthBarSeparators();
    }

    void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void ConstructHealthBarSeparators()
    {
        Transform separatorTemplate = _separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorTransform in _separatorContainer)
        {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }


        int healthAmountPerSeparator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / HealthSystem.GetHealthAmountMax();
        int healthSeparatorCount = Mathf.FloorToInt(HealthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

        for (int i = 1; i < healthSeparatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, _separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
        }
    }

    void UpdateBar()
    {
        _barTransform.localScale = new Vector3(HealthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    void UpdateHealthBarVisible()
    {
        if(HealthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
