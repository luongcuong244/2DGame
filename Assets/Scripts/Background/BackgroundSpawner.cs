using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject grass;
    public GameObject rockWall;

    public float backgroundSize = 50f;

    private float renderAreaHeight;
    private float renderAreaWidth;
    private float camHeight;
    private float camWidth;
    private float camPosX;
    private float camPosY;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;
        camPosX = Camera.main.transform.position.x;
        camPosY = Camera.main.transform.position.y;
        renderAreaHeight = camHeight + 2 * backgroundSize;
        renderAreaWidth = camWidth + 2 * backgroundSize;
        SpawnGrass();
        SpawnWall();
    }

    void SpawnGrass()
    {
        // get bounds of the grass object
        float grassHeight = grass.GetComponent<SpriteRenderer>().bounds.size.y;
        float grassWidth = grass.GetComponent<SpriteRenderer>().bounds.size.x;

        // calculate the number of grass objects needed to fill the screen
        int numGrassHeight = Mathf.CeilToInt(renderAreaHeight / grassHeight);
        int numGrassWidth = Mathf.CeilToInt(renderAreaWidth / grassWidth);

        Debug.Log("numGrassHeight: " + grassHeight);
        Debug.Log("numGrassWidth: " + grassWidth);

        // spawn grass objects
        for (int i = 0; i < numGrassHeight; i++)
        {
            for (int j = 0; j < numGrassWidth; j++)
            {
                Vector3 spawnPos = new Vector3(camPosX - renderAreaWidth / 2 + j * grassWidth + grassWidth / 2, camPosY + renderAreaHeight / 2 - i * grassHeight - grassHeight / 2, 0);
                GameObject grassObject = Instantiate(grass, spawnPos, Quaternion.identity);
                grassObject.transform.SetParent(this.transform, false);
            }
        }
    }

    void SpawnWall() {
        float rockWallHeight = rockWall.GetComponent<SpriteRenderer>().bounds.size.y;
        float rockWallWidth = rockWall.GetComponent<SpriteRenderer>().bounds.size.x;

        Debug.Log("rockWallHeight: " + rockWallHeight);
        Debug.Log("rockWallWidth: " + rockWallWidth);
    }
}
