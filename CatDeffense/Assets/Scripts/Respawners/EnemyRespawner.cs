using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    [Space]
    [SerializeField] private Transform[] respawnPoints;
    [Header("Lifetime Settings")]
    [SerializeField] private float cooldown = 5f; 
    [SerializeField] private float coolDownDecreaseRate = 0.02f; 
    [SerializeField] private float coolDownCap = 0.7f; 

    private float timer;
    private Transform player;
    private float initialCooldown;

    private void Start()
    {
        ResetRespawner();
    }

    private void Awake()
    {
        initialCooldown = cooldown;
    }

    private void Update()
    {

        if (GameManager.instance != null && GameManager.instance.IsGameOver)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = cooldown;
            CreateNewEnemy();
            cooldown = Mathf.Max(coolDownCap, cooldown - coolDownDecreaseRate);
        }

    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        Vector3 spawnPoint = respawnPoints[respawnPointIndex].position;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        if (player != null)
        {
            bool createdOnTheRight = newEnemy.transform.position.x > player.transform.position.x;
            if (createdOnTheRight)
                newEnemy.GetComponent<Enemy>().Flip();
        }
    }

    public void ResetRespawner()
    {
        timer = cooldown;
        player = FindFirstObjectByType<Player>()?.transform;
        cooldown = initialCooldown;
    }
}
