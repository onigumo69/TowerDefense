using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform enemyPrefab = Resources.Load<Transform>("EnemyPrefab");
        Transform enemyTransform = Instantiate(enemyPrefab, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    Rigidbody2D _rigidbody2D;
    Transform _targetTransform;
    float _lookForTargetTimer;
    float _lookForTargetTimerMax = 0.2f;

    HealthSystem _healthSystem;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (BuildingManager.Instance.GetHQBuilding() != null)
        {
            _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }

        _lookForTargetTimer = UnityEngine.Random.Range(0f, _lookForTargetTimerMax);

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        _healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);

        CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);

        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);

        Instantiate(Resources.Load<Transform>("pfEnemyDieParticles"), transform.position, Quaternion.identity);

        CinemachineShake.Instance.ShakeCamera(5f, 0.15f);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if(building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);

            _healthSystem.Damage(999);
        }
    }

    void HandleMovement()
    {
        if (_targetTransform != null)
        {
            Vector3 moveDir = (_targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            _rigidbody2D.velocity = moveDir * moveSpeed;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
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

    void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if(building != null)
            {
                if(_targetTransform == null)
                {
                    _targetTransform = building.transform;
                }
                else
                {
                    if(Vector3.Distance(transform.position, building.transform.position) < 
                       Vector3.Distance(transform.position, _targetTransform.position))
                    {
                        _targetTransform = building.transform;
                    }
                }
            }
        }

        if(_targetTransform == null)
        {
            if(BuildingManager.Instance.GetHQBuilding() != null)
            {
                _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
        }
    }
}
