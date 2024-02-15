using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem HealthSystem;

    Transform _barTransform;

    private void Awake()
    {
        _barTransform = transform.Find("bar");
    }

    private void Start()
    {
        HealthSystem.OnDamaged += HealthSystem_OnDamaged;
        HealthSystem.OnHealed += HealthSystem_OnHealed;

        UpdateBar();
        UpdateHealthBarVisible();
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
