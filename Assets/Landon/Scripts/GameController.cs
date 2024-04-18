using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    private GameObject[] spawnPoints;
    private List<GameObject> enemyList;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnWave();
    }

    private void spawnWave()
    {
        foreach (GameObject p in spawnPoints)
        {
            Vector3 spawnPos = p.transform.position;
            GameObject enemy = Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity, transform.Find("Enemies").transform);
            enemyList.Add(enemy);
        }
    }
}
