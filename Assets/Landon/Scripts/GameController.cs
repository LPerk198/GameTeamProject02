using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int waveAmount;

    private GameObject[] spawnPoints;
    private List<GameObject> enemyList = new List<GameObject>();

    private int waveCount = 0;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        allEnemiesKilled();
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

    private void allEnemiesKilled()
    {
        if(waveCount < waveAmount)
        {
            spawnWave();
            waveCount++;
        } else
        {
            // Pop up next level UI
        }
    }

    public void enemyKilled()
    {
        if(enemyList.Count <= 0) 
        {
            allEnemiesKilled();
        }
    }
}
