using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnTest : MonoBehaviour
{
    float timeBetweenSpawn = 0.01f;
    float currentSpawnTime = 0f;

    public GameObject enemy;

    private void Update()
    {
        currentSpawnTime -= Time.deltaTime;
    }

    public void SpawnEnemy(Transform spawnPoint, float HP, float newPowerLVL)
    {
        if (currentSpawnTime <= 0)
        {
            GameObject baby = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            baby.GetComponent<EnemyStats>().ChangeHealth(HP);
            baby.GetComponent<EnemyStats>().powerLVL = newPowerLVL;
            currentSpawnTime = timeBetweenSpawn;
        }
    }
}
