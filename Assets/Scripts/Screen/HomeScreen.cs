using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public ScreenManager screenManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickStart() {
        screenManager.Push("GamePlayScreen");
    }
}
