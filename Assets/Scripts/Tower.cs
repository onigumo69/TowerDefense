using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float ShootTimerMax;
    float _shootTimer;

    Enemy _targetEnemy;

    float _lookForTargetTimer;
    float _lookForTargetTimerMax = 0.2f;

    Vector3 _projectileSpawnPosition;

    private void Awake()
    {
        _projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }
    void HandleTargeting()
    {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer < 0f)
        {
            _lookForTargetTimer += _lookForTargetTimerMax;
            LookForTargets();
        }
    }

    void HandleShooting()
    {
        _shootTimer -= Time.deltaTime;
        if(_shootTimer <= 0f)
        {
            _shootTimer += ShootTimerMax;

            if (_targetEnemy != null)
            {
                ArrowProjectile.Create(_projectileSpawnPosition, _targetEnemy);
            }
        }
    }

    void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (_targetEnemy == null)
                {
                    _targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                       Vector3.Distance(transform.position, _targetEnemy.transform.position))
                    {
                        _targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
