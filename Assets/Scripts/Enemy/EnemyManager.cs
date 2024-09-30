using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform player; // Tham chiếu đến transform của người chơi
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public float spawnInterval = 5f; // Khoảng thời gian giữa các lần spawn
    public float minX = -10f; // Tọa độ x nhỏ nhất
    public float maxX = 10f; // Tọa độ x lớn nhất
    public float minY = -5f; // Tọa độ y nhỏ nhất
    public float maxY = 5f; // Tọa độ y lớn nhất
    public int maxEnemies = 10; // Số lượng kẻ thù tối đa

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (GetCurrentEnemyCount() < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
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

        // get exp item by tag
        GameObject expItem = enemy.transform.Find("ExpItem").gameObject;
        expItem.GetComponent<ExpItem>().setGameManager(gameManager);

        // get enemy controller on child
        EnemyController enemyController = enemy.GetComponentInChildren<EnemyController>();
        enemyController.setGameManager(gameManager);
        enemyController.setExpItemObject(expItem);
        enemyController.setPlayer(player);
    }

    private int GetCurrentEnemyCount()
    {
        // Đếm số lượng kẻ thù hiện có trong scene
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}