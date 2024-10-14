using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CinemachineVirtualCamera virtualCamera;
    public FixedJoystick fixedJoystick;
    public GameObject pauseDialog;
    public GameObject gameOverDialog;
    public GameObject levelUpDialog;
    public Image weaponImage;
    public Text killedEnemiesResultText;
    public Text timeResultText;
    public Text levelResultText;
    public Text killedEnemiesText;
    public Text timeText;
    public Text levelText;
    public ProgressBar expProgressBar;
    public List<BaseWeapon> weapons;
    private int weaponIndex = 0;

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
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
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
        gameOverDialog.SetActive(false);

        Time.timeScale = 1f; // Tiếp tục thời gian

        ClearWeapon();
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
        EnemyManager.instance.player = player.transform;
        EnemyManager.instance.StartSpawnEnemies();
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
        EnemyManager.instance.StopSpawnEnemies();
        ScreenManager.instance.Pop();
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        int expNeededForThisLevel = GetExpNeededForThisLevel();
        if (this.exp >= expNeededForThisLevel)
        {
            this.exp -= expNeededForThisLevel;
            level++;
            ShowLevelUpDialog();
        }
        levelText.text = "LV." + level;
        expProgressBar.SetValue((float)this.exp / expNeededForThisLevel);
    }

    public void AddHP(int hp)
    {
        Debug.Log("Add HP: " + hp);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.AddHP(hp);
    }

    public void IncreaseKilledEnemies()
    {
        killedEnemies++;
        killedEnemiesText.text = killedEnemies + "";
    }

    public void GameOver() {
        EnemyManager.instance.StopSpawnEnemies();
        showGameOverDialog();
    }

    private void showGameOverDialog()
    {
        isPaused = true;
        killedEnemiesResultText.text = killedEnemies + "";
        timeResultText.text = timeText.text;
        levelResultText.text = level + "";
        gameOverDialog.SetActive(true);
        Time.timeScale = 0f;

        // save to database
        History history = new History(killedEnemies, timeText.text, level, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        DatabaseManager.Instance.AddHistory(history);
    }

    private int GetExpNeededForThisLevel()
    {
        return 40 + (level - 1) * 30;
    }

    private void ShowLevelUpDialog()
    {
        isPaused = true;
        levelUpDialog.SetActive(true);
        weaponImage.sprite = weapons[weaponIndex].GetImage();
        Time.timeScale = 0f;
    }

    private void HideLevelUpDialog()
    {
        isPaused = false;
        levelUpDialog.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AddNewSickle()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.AddNewSickle();
        HideLevelUpDialog();
    }

    public void IncreaseSicklesSpeed()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.IncreaseSicklesSpeed(30);
        HideLevelUpDialog();
    }

    public void AddWeapon()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.AddWeapon(weapons[weaponIndex]);
        weaponIndex++;
        if (weaponIndex >= weapons.Count)
        {
            weaponIndex = 0;
        }
        HideLevelUpDialog();
    }

    private void ClearWeapon()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.ClearWeapon();
        weaponIndex = 0;
    }
}