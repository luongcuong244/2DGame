using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutScreen : MonoBehaviour
{
    public void clickBack()
    {
        ScreenManager.instance.Pop();
    }
}
