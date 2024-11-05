using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Image soundImage;
    public List<Sprite> soundSprites;

    void Start()
    {
        backgroundMusic.Play();
        soundImage.sprite = soundSprites[1];
    }

    public void clickStart() 
    {
        ScreenManager.instance.Push("PlayerChooserScreen");
    }

    public void clickHistory()
    {
        ScreenManager.instance.Push("HistoryScreen");
    }

    public void clickAbout()
    {
        ScreenManager.instance.Push("AboutScreen");
    }

    public void toggleSound()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
            soundImage.sprite = soundSprites[0];
        }
        else
        {
            backgroundMusic.Play();
            soundImage.sprite = soundSprites[1];
        }
    }
}
