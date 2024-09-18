using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public List<BackgroundItemWithProbability> backgroundItemsWithProbabilities;
    public GameObject rockWall;
    public float backgroundSize = 50f;
    public int cellPadding = 3;

    private float startSpawnPosX;
    private float startSpawnPosY;
    private int numCellWidth;
    private int numCellHeight;

    void Awake()
    {
        InitVariables();
        SpawnToFillBackground();
        SpawnWall();
    }

    void InitVariables()
    {
        float camHeight = Camera.main.orthographicSize * 2;
        float camWidth = camHeight * Camera.main.aspect;
        float renderAreaHeight = camHeight + 2 * backgroundSize;
        float renderAreaWidth = camWidth + 2 * backgroundSize;
        startSpawnPosX = Camera.main.transform.position.x - renderAreaWidth / 2;
        startSpawnPosY = Camera.main.transform.position.y + renderAreaHeight / 2;

        float itemHeight = backgroundItemsWithProbabilities[0].backgroundItem.GetComponent<SpriteRenderer>().bounds.size.y;
        float itemWidth = backgroundItemsWithProbabilities[0].backgroundItem.GetComponent<SpriteRenderer>().bounds.size.x;

        numCellHeight = Mathf.CeilToInt(renderAreaHeight / itemHeight);
        numCellWidth = Mathf.CeilToInt(renderAreaWidth / itemWidth);
    }

    void SpawnToFillBackground()
    {
        float itemHeight = backgroundItemsWithProbabilities[0].backgroundItem.GetComponent<SpriteRenderer>().bounds.size.y;
        float itemWidth = backgroundItemsWithProbabilities[0].backgroundItem.GetComponent<SpriteRenderer>().bounds.size.x;

        float totalProbability = 0f;

        foreach (var item in backgroundItemsWithProbabilities)
        {
            totalProbability += item.probability;
        }

        // spawn objects
        for (int i = 0; i < numCellHeight; i++)
        {
            for (int j = 0; j < numCellWidth; j++)
            {
                float randomValue = Random.value * totalProbability;
                float cumulativeProbability = 0f;
                foreach (var item in backgroundItemsWithProbabilities)
                {
                    cumulativeProbability += item.probability;
                    if (randomValue < cumulativeProbability)
                    {
                        Vector3 spawnPos = new Vector3(startSpawnPosX + j * itemWidth, startSpawnPosY - i * itemHeight, 0);
                        GameObject backgroundItem = Instantiate(item.backgroundItem, spawnPos, Quaternion.identity);
                        backgroundItem.transform.SetParent(this.transform, false);
                        backgroundItem.GetComponent<SpriteRenderer>().sortingOrder = -2;
                        break;
                    }
                }
            }
        }
    }

    void SpawnWall() {
        float rockWallHeight = rockWall.GetComponent<SpriteRenderer>().bounds.size.y;
        float rockWallWidth = rockWall.GetComponent<SpriteRenderer>().bounds.size.x;

        // spawn rock wall objects around the screen
        for (int i = 0; i < numCellHeight; i++)
        {
            if (i == cellPadding || i == numCellHeight - cellPadding - 1)
            {
                for (int j = 0; j < numCellWidth; j++)
                {
                    if (j < cellPadding || j >= numCellWidth - cellPadding)
                    {
                        continue;
                    }
                    Vector3 spawnPos = new Vector3(startSpawnPosX + j * rockWallWidth, startSpawnPosY - i * rockWallHeight, 0);
                    GameObject rockWallObject = Instantiate(rockWall, spawnPos, Quaternion.identity);
                    rockWallObject.transform.SetParent(this.transform, false);

                    // make sure the rock above the grass
                    rockWallObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
                }
            }
            else if (i > cellPadding && i < numCellHeight - cellPadding - 1)
            {
                Vector3 spawnPos = new Vector3(startSpawnPosX + cellPadding * rockWallWidth, startSpawnPosY - i * rockWallHeight, 0);
                GameObject rockWallObject = Instantiate(rockWall, spawnPos, Quaternion.identity);
                rockWallObject.transform.SetParent(this.transform, false);

                spawnPos = new Vector3(startSpawnPosX + (numCellWidth - cellPadding - 1) * rockWallWidth, startSpawnPosY - i * rockWallHeight, 0);
                rockWallObject = Instantiate(rockWall, spawnPos, Quaternion.identity);
                rockWallObject.transform.SetParent(this.transform, false);

                // make sure the rock above the grass
                rockWallObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
        }
    }
}
