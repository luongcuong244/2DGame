using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public int exp = 10;
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddExp(exp);
            Destroy(gameObject);
        }
    }

    public void setGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
