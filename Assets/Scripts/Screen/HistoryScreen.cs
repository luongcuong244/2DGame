using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryScreen : MonoBehaviour
{
    public void clickBack() 
    {
        ScreenManager.instance.Pop();
    }

    void OnEnable()
    {
        // Lấy danh sách lịch sử từ DatabaseManager
        List<History> histories = DatabaseManager.Instance.GetAllHistory();
        // in
        foreach (History history in histories)
        {
            Debug.Log("Killed enemies: " + history.killedEnemies);
            Debug.Log("Survived: " + history.survived);
            Debug.Log("Level reached: " + history.levelReached);
            Debug.Log("Timestamp: " + history.timestamp);
        }
    }
}
