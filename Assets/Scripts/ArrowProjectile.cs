using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Vector3 position, Enemy targetEnemy)
    {
        Transform arrowProjectilePrefab = Resources.Load<Transform>("ArrowProjectilePrefab");
        Transform arrowTransform = Instantiate(arrowProjectilePrefab, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetEnemy);

        return arrowProjectile;
    }

    Enemy _targetEnemy;
    Vector3 _lastMoveDir;
    float timeToDie = 2f;

    private void Update()
    {
        Vector3 moveDir;
        if(_targetEnemy != null)
        {
            moveDir = (_targetEnemy.transform.position - transform.position).normalized;
            _lastMoveDir = moveDir;
        }
        else
        {
            moveDir = _lastMoveDir;
        }

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, UtilClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if(timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    void SetTarget(Enemy targetEnemy)
    {
        _targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);

            Destroy(gameObject);
        }
    }
}
