using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDied;

    [SerializeField] int HealthAmountMax;

    int _healthAmount;

    private void Awake()
    {
        _healthAmount = HealthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, HealthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        _healthAmount += healAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, HealthAmountMax);

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        _healthAmount = HealthAmountMax;

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return _healthAmount == 0;
    }

    public bool IsFullHealth()
    {
        return _healthAmount == HealthAmountMax;
    }

    public int GetHealthAmount()
    {
        return _healthAmount;
    }

    public int GetHealthAmountMax()
    {
        return HealthAmountMax;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)_healthAmount / HealthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        HealthAmountMax = healthAmountMax;

        if(updateHealthAmount)
        {
            _healthAmount = healthAmountMax;
        }
    }
}
