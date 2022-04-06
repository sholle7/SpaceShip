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
    private static float _nextObstacleSpawnTime = 2f;
    private static float _nextCoinSpawnTime = 3f;
        
    
    public void Update()
    {
        UpdateTime();
        SpawnObstacle();
        SpawnCoin();
    }
    public void UpdateTime()
    {
        _currentObstacleTime += Time.deltaTime;
        _currentCoinTime += Time.deltaTime;
    }
    public void SpawnObstacle()
    {
        GameObject obstacle= GetRandomObstacle();
     
        if(_nextObstacleSpawnTime < _currentObstacleTime)
        {
            _currentObstacleTime = 0;
            Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(-8,8), transform.position.y, transform.position.z);
            Instantiate(obstacle, spawnPoint, transform.rotation);
        }
    }
    public void SpawnCoin()
    {
        if(_nextCoinSpawnTime < _currentCoinTime){
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
        _nextObstacleSpawnTime -= 0.4f;
    }
    public static void ChangeCoinSpeed()
    {
        _nextCoinSpawnTime -= 0.5f;
    }
}
