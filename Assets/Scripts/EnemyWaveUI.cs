using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] EnemyWaveManager EnemyWaveManager;

    TextMeshProUGUI _waveNumberText;
    TextMeshProUGUI _waveMessageText;
    RectTransform _enemyWaveSpawnPositionIndicator;
    RectTransform _enemyClosestPositionIndicator;

    private void Awake()
    {
        _waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        _waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        _enemyWaveSpawnPositionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        _enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        EnemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;

        SetWaveNumberText("Wave " + EnemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPositionIndicator();
    }

    void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = EnemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    void HandleEnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition = (EnemyWaveManager.GetSpawnPosition() - Camera.main.transform.position).normalized;
        _enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        _enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(EnemyWaveManager.GetSpawnPosition(), Camera.main.transform.position);
        _enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition > Camera.main.orthographicSize * 1.5f);
    }

    void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(Camera.main.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;
        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                       Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - Camera.main.transform.position).normalized;
            _enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            _enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(EnemyWaveManager.GetSpawnPosition(), Camera.main.transform.position);
            _enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > Camera.main.orthographicSize * 1.5f);
        }
        else
        {
            _enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + EnemyWaveManager.GetWaveNumber());
    }

    void SetWaveNumberText(string text)
    {
        _waveNumberText.SetText(text);
    }

    void SetMessageText(string message)
    {
        _waveMessageText.SetText(message);
    }
}
