using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 5;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.onEvent.AddListener(OnEnemyDestroyed);
            // You can set up any necessary configurations for the enemy here
        }
    }

    private void OnEnemyDestroyed()
    {
        Debug.Log("Enemy destroyed");
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Provide the logic to get a random spawn position for the enemy
        // For example, you can use Random.Range to generate random x, y, z coordinates within a certain range
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        return spawnPosition;
    }
}