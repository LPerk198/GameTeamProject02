using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int waveAmount;
    public GameObject gameOverUI;

    private GameObject confetti;

    private GameObject[] spawnPoints;
    private List<GameObject> enemyList = new List<GameObject>();

    private int waveCount = 0;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        allEnemiesKilled();
        confetti = GameObject.FindGameObjectWithTag("VictoryConfetti");
        confetti.SetActive(false);
    }

    private void spawnWave()
    {
        foreach (GameObject p in spawnPoints)
        {
            Vector3 spawnPos = p.transform.position;
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPos, Quaternion.identity, transform.Find("Enemies").transform);
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
            confetti.SetActive(true);
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void enemyKilled(GameObject enemy)
    {
        enemyList.Remove(enemy);
        if(enemyList.Count <= 0) 
        {
            allEnemiesKilled();
        }
    }
}
