using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount = 3;
    public int enemyBosses;
    public Vector2[] enemyPos;
    public static EnemyManger instance;
    public GameObject enemyBoss;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemy, enemyPos[i], Quaternion.identity);
        }
    }
    private void Update()
    {
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        //int randomValue = Random.Range(0, 3);
        for (int i=0;i<enemyBosses;i++)
        {
           
            if (enemyCount < 1 && enemyBosses>0)
            {
                Instantiate(enemyBoss, enemyPos[0], Quaternion.identity);
                enemyBosses--;
            }
        }
    }
}
