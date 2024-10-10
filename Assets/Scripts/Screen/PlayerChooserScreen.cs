using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooserScreen : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    public Image playerImage;
    public Text healthPointText;
    public Text damageText;
    public Text speedText;
    private int selectedPlayerIndex = 0;
    private GameObject selectedPlayer;

    void Start()
    {
        selectedPlayer = playerPrefabs[selectedPlayerIndex];
        updateUI();
    }

    public void clickPrevious()
    {
        if (selectedPlayerIndex > 0)
        {
            selectedPlayerIndex--;
        }
        else
        {
            selectedPlayerIndex = playerPrefabs.Count - 1;
        }
        selectedPlayer = playerPrefabs[selectedPlayerIndex];
        updateUI();
    }

    public void clickNext()
    {
        if (selectedPlayerIndex < playerPrefabs.Count - 1)
        {
            selectedPlayerIndex++;
        }
        else
        {
            selectedPlayerIndex = 0;
        }
        selectedPlayer = playerPrefabs[selectedPlayerIndex];
        updateUI();
    }

    public void clickBack() 
    {
        ScreenManager.instance.Pop();
    }

    public void clickConfirm() 
    {
        if (selectedPlayer == null)
        {
            Debug.LogWarning("Please choose a player!");
            return;
        }
        ScreenManager.instance.Push("GamePlayScreen");
        GameManager.instance.setPlayerPrefab(selectedPlayer);
        GameManager.instance.NewGame();
    }

    private void updateUI()
    {
        // get Player Controller
        PlayerController playerController = selectedPlayer.GetComponent<PlayerController>();
        healthPointText.text = playerController.health.ToString();
        damageText.text = playerController.damage.ToString();
        speedText.text = (playerController.moveSpeed * 5).ToString();
        playerImage.sprite = playerController.GetImage();
    }
}