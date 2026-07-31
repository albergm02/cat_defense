using System.Collections.Generic;
using UnityEngine;

public class CoinRespawner : MonoBehaviour
{
    [Header("Coin Prefabs")]
    [SerializeField] private GameObject goldCoinPrefab;
    [SerializeField] private GameObject silverCoinPrefab;
    [SerializeField] private GameObject rubyCoinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Lifetime Settings")]
    [SerializeField] private float cooldown = 5f;


    private float timer;
    private List<int> occupiedPoints = new List<int>();

    private void Update()
    {

        if (GameManager.instance != null && GameManager.instance.IsGameOver)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = cooldown;
            SpawnRandomCoin();
        }
    }

    private void Start()
    {
        ResetRespawner();
    }

    private void SpawnRandomCoin()
    {
        List<int> freePoints = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!occupiedPoints.Contains(i))
                freePoints.Add(i);
        }

        if (freePoints.Count == 0)
            return;

        int pointIndex = freePoints[Random.Range(0, freePoints.Count)];
        Vector3 spawnPos = spawnPoints[pointIndex].position;
        spawnPos.z = 0;

        GameObject prefabToSpawn = GetRandomCoinPrefab();
        GameObject coin = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        occupiedPoints.Add(pointIndex);

        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
            coinScript.Init(this, pointIndex);
    }



    private GameObject GetRandomCoinPrefab()
    {
        int random = Random.Range(0, 3); 
        switch (random)
        {
            case 0: return goldCoinPrefab;
            case 1: return silverCoinPrefab;
            case 2: return rubyCoinPrefab;
            default: return silverCoinPrefab;
        }
    }

    public void FreeSpawnPoint(int index)
    {
        occupiedPoints.Remove(index);
    }

    public void ResetRespawner()
    {
        timer = cooldown;
        occupiedPoints.Clear();
    }
}
