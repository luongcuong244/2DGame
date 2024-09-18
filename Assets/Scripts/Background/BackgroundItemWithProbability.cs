using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundItemWithProbability
{
    public GameObject backgroundItem;  // Đối tượng GameObject cần chứa
    [Range(0, 100)] public float probability;  // Xác suất xuất hiện (từ 0 đến 1)
}
