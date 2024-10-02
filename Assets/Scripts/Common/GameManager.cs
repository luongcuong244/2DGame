using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CinemachineVirtualCamera virtualCamera;
    public FixedJoystick fixedJoystick;
    public EnemyManager enemyManager;
    public GameObject pauseDialog;
    public GameObject gameOverDialog;
    public Text killedEnemiesText;
    public Text timeText;
    public Text levelText;

    public ProgressBar expProgressBar;

    public int expForEachLevel = 40;

    private GameObject playerPrefab;
    private GameObject player;
    private float elapsedTime = 0f;  // Biến để lưu thời gian đã trôi qua
    private bool isPaused = false;   // Biến kiểm tra trạng thái tạm dừng
    private int exp = 0;
    private int level = 1;
    private int killedEnemies = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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

    public void RePlay()
    {
        NewGame();
    }

    public void setPlayerPrefab(GameObject playerPrefab)
    {
        this.playerPrefab = playerPrefab;
    }

    public void NewGame()
    {
        setupPlayer();
        setupEnemy();

        killedEnemies = 0;
        killedEnemiesText.text = killedEnemies + "";

        exp = 0;

        level = 1;
        levelText.text = "LV." + level;
        expProgressBar.SetValue(0);
        elapsedTime = 0f;
        isPaused = false;
        pauseDialog.SetActive(false); // Ẩn dialog tạm dừng
        Time.timeScale = 1f; // Tiếp tục thời gian
    }

    private void setupPlayer()
    {
        // Xóa player cũ
        if (player != null)
        {
            Destroy(player);
        }

        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        // set parent and position
        player.transform.SetParent(transform, false);
        player.transform.position = Vector3.zero;
        // set joystick
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.fixedJoystick = fixedJoystick;

        // Camera follow player
        virtualCamera.Follow = player.transform;
    }

    private void setupEnemy()
    {
        enemyManager.player = player.transform;
        enemyManager.StartSpawnEnemies();
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
        gameOverDialog.SetActive(false);
        Time.timeScale = 1f;  // Tiếp tục thời gian
    }

    public void QuitGame()
    {
        enemyManager.StopSpawnEnemies();
        ScreenManager.instance.Pop();
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        if (this.exp >= expForEachLevel)
        {
            this.exp -= expForEachLevel;
            level++;
        }
        levelText.text = "LV." + level;
        expProgressBar.SetValue((float)this.exp / expForEachLevel);
    }

    public void IncreaseKilledEnemies()
    {
        killedEnemies++;
        killedEnemiesText.text = killedEnemies + "";
    }

    public void GameOver() {
        enemyManager.StopSpawnEnemies();
        showGameOverDialog();
    }

    private void showGameOverDialog()
    {
        isPaused = true;
        gameOverDialog.SetActive(true);
        Time.timeScale = 0f;
    }
}