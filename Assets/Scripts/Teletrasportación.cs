using UnityEngine;

public class TuberiasScript : MonoBehaviour
{
    public Transform targetPipe; // Arrastra aquí el punto de aparición superior (tubería de arriba)
    public bool isSpawner = false; // Marcar si esta tubería también genera enemigos al inicio

    // Variables para Spawning (solo si esSpawner = true)
    public GameObject enemyPrefab;
    public float spawnInterval = 4f;
    private float timer;

    void Update()
    {
        // Lógica de Generación automática (Spawner)
        if (isSpawner)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnEnemy();
                timer = 0; // Reiniciamos contador
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

    // Lógica de Teletransporte (Loop del video)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si un enemigo toca la tubería de abajo
        if (collision.CompareTag("Enemy"))
        {
            // Lo mandamos a la posición de la tubería objetivo (arriba)
            if (targetPipe != null)
            {
                collision.transform.position = targetPipe.position;

                // Opcional: Aumentar velocidad al respawnear (como en el arcade)
                // collision.GetComponent<EnemyScript>().speed += 1f; 
            }
        }
    }
}
