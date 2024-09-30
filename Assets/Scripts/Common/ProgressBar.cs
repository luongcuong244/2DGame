using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public int max = 40;
    public int current = 0;
    public GameObject fill;
    // Start is called before the first frame update
    void Start()
    {
        SetValue(0);
    }

    public void SetValue(float value)
    {
        Debug.Log("Value: " + value);
        fill.transform.localScale = new Vector3(value, 1, 1);
    }

    public void SetMax(int value)
    {
        max = value;
    }
}
