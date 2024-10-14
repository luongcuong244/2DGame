using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager
{
    // Biến tĩnh để giữ thể hiện duy nhất của DatabaseManager
    private static DatabaseManager _instance;

    // Danh sách lịch sử
    private const string HISTORY_KEY = "HistoryList";

    // Constructor riêng tư để ngăn tạo đối tượng bên ngoài
    private DatabaseManager() { }

    // Phương thức để lấy thể hiện của DatabaseManager
    public static DatabaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DatabaseManager();
            }
            return _instance;
        }
    }

    public void AddHistory(History history)
    {
        // Lấy dữ liệu hiện tại từ PlayerPrefs
        string json = PlayerPrefs.GetString(HISTORY_KEY, "{}");

        // Chuyển đổi thành danh sách lịch sử
        HistoryList historyList = JsonUtility.FromJson<HistoryList>(json);
        if (historyList == null)
        {
            historyList = new HistoryList();
        }

        // Thêm lịch sử mới vào danh sách
        historyList.histories.Add(history);

        // Chuyển đổi lại thành JSON và lưu vào PlayerPrefs
        string newJson = JsonUtility.ToJson(historyList);
        PlayerPrefs.SetString(HISTORY_KEY, newJson);
        PlayerPrefs.Save();
    }

    public List<History> GetAllHistory()
    {
        // Lấy dữ liệu từ PlayerPrefs
        string json = PlayerPrefs.GetString(HISTORY_KEY, "{}");

        // Chuyển đổi từ JSON về danh sách lịch sử
        HistoryList historyList = JsonUtility.FromJson<HistoryList>(json);
        return historyList != null ? historyList.histories : new List<History>();
    }
}

[System.Serializable]
public class HistoryList
{
    public List<History> histories = new List<History>();
}