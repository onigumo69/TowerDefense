using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;

    enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }

    [SerializeField] List<Transform> SpawnPositionTransformList;
    [SerializeField] Transform NextWaveSpawnPositionTransform;

    State _currentState;
    int _waveNumber;
    float _nextWaveSpawnTimer;
    float _nextEnemySpawnTimer;
    int _remainEnemySpawnAmount;
    Vector3 _spawnPosition;

    private void Start()
    {
        _currentState = State.WaitingToSpawnNextWave;
        _nextWaveSpawnTimer = 3f;

        _spawnPosition = SpawnPositionTransformList[UnityEngine.Random.Range(0, SpawnPositionTransformList.Count)].position;
        NextWaveSpawnPositionTransform.position = _spawnPosition;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.WaitingToSpawnNextWave:
                _nextWaveSpawnTimer -= Time.deltaTime;
                if (_nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:
                if (_remainEnemySpawnAmount > 0)
                {
                    _nextEnemySpawnTimer -= Time.deltaTime;
                    if (_nextEnemySpawnTimer < 0f)
                    {
                        _nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                        Enemy.Create(_spawnPosition + UtilClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        _remainEnemySpawnAmount--;
                    }

                    if (_remainEnemySpawnAmount <= 0)
                    {
                        _currentState = State.WaitingToSpawnNextWave;
                        _spawnPosition = SpawnPositionTransformList[UnityEngine.Random.Range(0, SpawnPositionTransformList.Count)].position;
                        NextWaveSpawnPositionTransform.position = _spawnPosition;

                        _nextWaveSpawnTimer = 10f;
                    }
                }
                break;
        }
    }

    void SpawnWave()
    {
        _remainEnemySpawnAmount = 5 + 3 * _waveNumber;

        _currentState = State.SpawningWave;
        _waveNumber++;

        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return _waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return _nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return _spawnPosition;
    }
}
