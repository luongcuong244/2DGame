using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public GameObject playerPrefab;

    // private void Start()
    // {
    //     // create a player
    //     Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    // }

    private void newGame(GameObject player)
    {
        // reset player position
        player.transform.position = Vector3.zero;
    }
}
