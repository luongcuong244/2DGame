using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public void clickStart() 
    {
        ScreenManager.instance.Push("PlayerChooserScreen");
    }

    public void clickHistory()
    {
        ScreenManager.instance.Push("HistoryScreen");
    }
}
