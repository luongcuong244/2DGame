using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject pauseDialog;
    public Text timeText;

    private float elapsedTime = 0f;  // Biến để lưu thời gian đã trôi qua
    private bool isPaused = false;   // Biến kiểm tra trạng thái tạm dừng

    private void Start()
    {
        // Tạo một player khi bắt đầu game
        // Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        // Bắt đầu đếm thời gian từ 0
        elapsedTime = 0f;
        Time.timeScale = 1f; // Đảm bảo game bắt đầu ở trạng thái chạy
    }

    private void Update()
    {
        // Nếu game không bị tạm dừng, thì cập nhật thời gian
        if (!isPaused)
        {
            // Tăng thời gian đã trôi qua mỗi frame
            elapsedTime += Time.deltaTime;

            // Chuyển đổi thành định dạng mm:ss
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            // Cập nhật text hiển thị
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseDialog.SetActive(true);  // Hiển thị dialog tạm dừng
        Time.timeScale = 0f;  // Dừng thời gian
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseDialog.SetActive(false);  // Ẩn dialog tạm dừng
        Time.timeScale = 1f;  // Tiếp tục thời gian
    }

    private void NewGame(GameObject player)
    {
        // Reset vị trí của player
        player.transform.position = Vector3.zero;
    }
}