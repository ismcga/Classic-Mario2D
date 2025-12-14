using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject enemyPrefab; 
    public float timeBetweenSpawns = 3f; 
    public bool randomizeTime = true; 

    [Header("Rango Aleatorio (Solo si randomizeTime es true)")]
    public float minTime = 2f;
    public float maxTime = 5f;

    private float timer;
    private float currentWaitTime;

    void Start()
    {
        
        currentWaitTime = timeBetweenSpawns;
        if (randomizeTime) currentWaitTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        timer += Time.deltaTime;

       
        if (timer >= currentWaitTime)
        {
            SpawnEnemy();
            timer = 0; 

            if (randomizeTime)
            {
                currentWaitTime = Random.Range(minTime, maxTime);
            }
            else
            {
                currentWaitTime = timeBetweenSpawns;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}