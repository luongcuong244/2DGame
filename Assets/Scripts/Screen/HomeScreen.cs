using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickStart() {
        ScreenManager.instance.Push("GamePlayScreen");
        GameManager.instance.setPlayerPrefab(playerPrefab);
        GameManager.instance.NewGame();
    }
}
