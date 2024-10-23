using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryScreen : MonoBehaviour
{
    public GameObject scrollViewContent;
    public GameObject historyItemPrefab;

    public void clickBack() 
    {
        ScreenManager.instance.Pop();
    }

    void OnEnable()
    {
        // clear all children
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
        // Lấy danh sách lịch sử từ DatabaseManager
        List<History> histories = DatabaseManager.Instance.GetAllHistory();
        foreach (History history in histories)
        {
            // Debug.Log("Killed enemies: " + history.killedEnemies);
            // Debug.Log("Survived: " + history.survived);
            // Debug.Log("Level reached: " + history.levelReached);
            // Debug.Log("Timestamp: " + history.timestamp);
            GameObject historyItem = Instantiate(historyItemPrefab, scrollViewContent.transform);
            Text killedEnemiesText = getTextComponentByName(historyItem, "KilledEnemiesText");
            killedEnemiesText.text = history.killedEnemies + "";
            Text survivedText = getTextComponentByName(historyItem, "SurvivedText");
            survivedText.text = history.survived;
            Text levelReachedText = getTextComponentByName(historyItem, "LevelReachedText");
            levelReachedText.text = history.levelReached + "";
            Text timestampText = getTextComponentByName(historyItem, "DateTime");
            timestampText.text = history.timestamp;
            // set parent
            historyItem.transform.SetParent(scrollViewContent.transform, false);
        }
    }

    private Text getTextComponentByName(GameObject parent, string name)
    {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            Debug.Log(child.name);
            if (child.name == name)
            {
                return child.GetComponent<Text>();
            }
        }
        return null;
    }
}
