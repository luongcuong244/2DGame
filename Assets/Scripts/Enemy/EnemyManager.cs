using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance; // Biến static để lưu trữ thể hiện duy nhất của lớp này
    public Transform player; // Tham chiếu đến transform của người chơi
    public List<GameObject> enemiesPrefab; // Prefab của kẻ thù
    public GameObject expItemPrefab; // Prefab của item EXP
    public List<ItemWithProbability> itemsWithProbabilities; // Danh sách item với xác suất xuất hiện
    public float minX = -10f; // Tọa độ x nhỏ nhất
    public float maxX = 10f; // Tọa độ x lớn nhất
    public float minY = -5f; // Tọa độ y nhỏ nhất
    public float maxY = 5f; // Tọa độ y lớn nhất
    public int maxEnemies = 10; // Số lượng kẻ thù tối đa

    // biến lưu coroutine
    private List<Coroutine> spawnEnemiesCoroutines = new List<Coroutine>();

    private void Awake()
    {
        // Lưu trữ thể hiện duy nhất của lớp này
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartSpawnEnemies()
    {
        StopSpawnEnemies();
        ClearEnemiesAndExpItem();
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawnEnemies()
    {
        foreach (Coroutine coroutine in spawnEnemiesCoroutines)
        {
            StopCoroutine(coroutine);
        }
        spawnEnemiesCoroutines.Clear();
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (GameObject enemyPrefab in enemiesPrefab)
        {
            float spawnInterval = enemyPrefab.GetComponent<EnemyController>().spawnInterval;
            Coroutine coroutine = StartCoroutine(StartSpawnEnemies(enemyPrefab, spawnInterval));
            Debug.Log("Start spawn enemy: " + enemyPrefab.name + " with interval: " + spawnInterval);
            spawnEnemiesCoroutines.Add(coroutine);
        }
        yield return null;
    }

    private IEnumerator StartSpawnEnemies(GameObject enemyPrefab, float interval)
    {
        while (true)
        {
            if (GetCurrentEnemyCount() < maxEnemies)
            {
                SpawnEnemy(enemyPrefab);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private void ClearEnemiesAndExpItem()
    {
        // destroy all child
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Tạo vị trí ngẫu nhiên trong khoảng cho phép
        Vector2 spawnPosition = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );

        // Tạo kẻ thù tại vị trí ngẫu nhiên
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        // set parent
        enemy.transform.SetParent(transform, false);

        // get enemy controller on child
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.setPlayer(player);
    }

    private int GetCurrentEnemyCount()
    {
        // Đếm số lượng kẻ thù hiện có trong scene
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}