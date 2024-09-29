using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public int max;
    public int current;
    public GameObject fill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetValue(current);
    }

    public void SetValue(int value)
    {
        current = value;
        float fillAmount = (float)current / max;
        if (fillAmount < 0)
        {
            fillAmount = 0;
        }
        if (fillAmount > 1)
        {
            fillAmount = 1;
        }
        fill.transform.localScale = new Vector3(fillAmount, 1, 1);
    }

    public void SetMax(int value)
    {
        max = value;
    }
}
