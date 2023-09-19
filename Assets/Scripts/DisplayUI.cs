using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    //Singleton
    public static DisplayUI Instance;
    private void Awake()
    {
        Instance = this;
    }

    //Interaction Info
    public Text infoText;

    //Lose, Win, Pause
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;

    //Time
    public Text timeText;
    public Image freezeImage;

    //Pickups
    public Text diamondsText;
    public Text goldKeysText;
    public Text redKeysText;
    public Text greenKeysText;

    private void Start()
    {
        GameManager.GamePaused += OnPause;
        GameManager.Lose += OnLose;
        GameManager.Win += OnWin;

        ClearInfoText();
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.GamePaused -= OnPause;
        GameManager.Lose -= OnLose;
        GameManager.Win -= OnWin;
    }

    private void Update()
    {
        timeText.text = GameManager.Instance.timer.ToString();
        freezeImage.enabled = GameManager.Instance.Freeze;

        diamondsText.text = GameManager.Instance.DiamondsCount.ToString();
        goldKeysText.text = GameManager.Instance.GoldKeys.ToString();
        redKeysText.text = GameManager.Instance.RedKeys.ToString();
        greenKeysText.text = GameManager.Instance.GreenKeys.ToString();
    }

    void OnPause(bool paused)
    {
        if(paused)
        {
            ClearInfoText();
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }
    void OnLose()
    {
        losePanel.SetActive(true);
    }
    void OnWin()
    {
        winPanel.SetActive(true);
    }

    public void DisplayInfo( string info)
    {
      //  infoText.color = color;
        infoText.text = info;
    }

    public void ClearInfoText()
    {
        infoText.text = "";
    }
}
