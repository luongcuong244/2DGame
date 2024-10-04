using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance; // Biến static để lưu trữ thể hiện duy nhất của lớp này
    public Transform player; // Tham chiếu đến transform của người chơi
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public GameObject expItemPrefab; // Prefab của item EXP
    public List<ItemWithProbability> itemsWithProbabilities; // Danh sách item với xác suất xuất hiện
    public float spawnInterval = 5f; // Khoảng thời gian giữa các lần spawn
    public float minX = -10f; // Tọa độ x nhỏ nhất
    public float maxX = 10f; // Tọa độ x lớn nhất
    public float minY = -5f; // Tọa độ y nhỏ nhất
    public float maxY = 5f; // Tọa độ y lớn nhất
    public int maxEnemies = 10; // Số lượng kẻ thù tối đa

    // biến lưu coroutine
    private Coroutine spawnEnemiesCoroutine;

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
        spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawnEnemies()
    {
        if (spawnEnemiesCoroutine != null)
        {
            StopCoroutine(spawnEnemiesCoroutine);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        ClearEnemiesAndExpItem();
        while (true)
        {
            if (GetCurrentEnemyCount() < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
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

    private void SpawnEnemy()
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