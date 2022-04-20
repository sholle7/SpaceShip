using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private GameObject _coin;

    private static float _currentObstacleTime = 0f;
    private static float _currentCoinTime = 0f;
    private static float _currentLevelChangeTime = 0f;
    private static float _currentObstacleUpgradeTime = 0f;
    private static float _nextObstacleSpawnTime = 2f;
    private static float _nextCoinSpawnTime = 3f;
    private static float _nextLevelChangeTime = 15f;
    private static float _nextObstacleUpgradeTime = 30f;

    public void Update()
    {
        UpdateTime();
        SpawnObstacle();
        SpawnCoin();

        if (CanChangeLevel())
        {
            _currentLevelChangeTime = 0;
            ChangeObstacleSpeed();
            ChangeCoinSpeed();
        }
        if (CanUpgradeOBbstacle())
        {
            _currentObstacleUpgradeTime = 0;
            UpgradeObstacle();
        }

    }
    public bool CanChangeLevel()
    {
        return _nextLevelChangeTime < _currentLevelChangeTime;
    }
    public bool CanUpgradeOBbstacle()
    {
        return _nextObstacleUpgradeTime < _currentObstacleUpgradeTime;
    }
    public void UpdateTime()
    {
        _currentObstacleTime += Time.deltaTime;
        _currentCoinTime += Time.deltaTime;
        _currentLevelChangeTime += Time.deltaTime;
        _currentObstacleUpgradeTime += Time.deltaTime;
    }
    public void SpawnObstacle()
    {
        GameObject obstacle = GetRandomObstacle();

        if (_nextObstacleSpawnTime < _currentObstacleTime)
        {
            _currentObstacleTime = 0;
            Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(-8, 8), transform.position.y, transform.position.z);
            Instantiate(obstacle, spawnPoint, transform.rotation);
        }
    }
    public void SpawnCoin()
    {
        if (_nextCoinSpawnTime < _currentCoinTime) {
            _currentCoinTime = 0;
            Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(-8, 8), transform.position.y, transform.position.z);
            Instantiate(_coin, spawnPoint, transform.rotation);
        }
    }
    public GameObject GetRandomObstacle()
    {
        int i = UnityEngine.Random.Range(0, _obstacles.Length);
        return _obstacles[i];
    }

    public static void ChangeObstacleSpeed()
    {
        if (_nextObstacleSpawnTime > 0.5f) _nextObstacleSpawnTime -= 0.4f;
    }
    public static void ChangeCoinSpeed()
    {
        if (_nextCoinSpawnTime > 0.3f) _nextCoinSpawnTime -= 0.2f;
    }
    public void UpgradeObstacle()
    {
       Obstacle.UpgradeObstacle();
    }
    public static void ResetVariables()
    {
        _currentObstacleTime = 0f;
        _currentCoinTime = 0f;
        _currentLevelChangeTime = 0f;
        _currentObstacleUpgradeTime = 0f;
        _nextObstacleSpawnTime = 2f;
        _nextCoinSpawnTime = 3f;
        _nextLevelChangeTime = 15f;
        _nextObstacleUpgradeTime = 30f;
    }
   
}
