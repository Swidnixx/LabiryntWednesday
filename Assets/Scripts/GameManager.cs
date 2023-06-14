using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GMs in the Scene!");
        }
        Instance = this;
        Key.LoadGoldKeys();
    }

    //Main Game Events
    public static event Action<bool> Paused;
    public static event Action Lose;
    public static event Action Win;

    //Game Logic
    public int timer = 60;
    bool paused;
    public bool Freeze { get; private set; }
    bool gameEnded;

    //Pickups
    public int DiamondsCount { get; private set; }

    public int RedKeys { get; private set; }
    public int GreenKeys { get; private set; }
    public int GoldKeys { get; private set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InvokeRepeating(nameof(Stopper), 3, 1);
    }
    private void Update()
    {
        if( Input.GetButtonDown("Cancel") && !gameEnded)
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(gameEnded || paused)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
    private void OnApplicationQuit()
    {
        Key.SaveGoldKeys();
    }

    #region GameFlow - Pause, Win, Lose, Time
    private void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        Paused?.Invoke(false);
    }
    private void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        Paused?.Invoke(true);
    }
    void Stopper()
    {
        Freeze = false;
        timer--;
    }
    internal void GameOver()
    {
        gameEnded = true;
        Lose?.Invoke();
    }
    public void GameWin()
    {
        gameEnded = true;
        Win?.Invoke();
    }
    #endregion

    #region Pickups
    internal void AddKey(Key.KeyType type)
    {
        switch (type)
        {
            case Key.KeyType.Red:
                RedKeys++;
                break;
            case Key.KeyType.Green:
                GreenKeys++;
                break;
            case Key.KeyType.Gold:
                GoldKeys++;
                break;
        }
    }
    public void FreezeTime(int time)
    {
        Freeze = true;
        CancelInvoke();
        InvokeRepeating(nameof(Stopper), time, 1);
    }

    internal bool CheckTheKey(Key.KeyType keyColor)
    {
        switch (keyColor)
        {
            case Key.KeyType.Red:
                return RedKeys > 0;

            case Key.KeyType.Green:
                return GreenKeys > 0;

            case Key.KeyType.Gold:
                return GoldKeys > 0;
        }

        return false;
    }

    internal void UseTheKey(Key.KeyType keyColor)
    {
        switch (keyColor)
        {
            case Key.KeyType.Red:
                RedKeys--;
                break;
            case Key.KeyType.Green:
                GreenKeys--;
                break;
            case Key.KeyType.Gold:
                GoldKeys--;
                break;
        }
    }

    public void AddDiamond()
    {
        DiamondsCount++;
    }
    public void AddTime(int time)
    {
        timer += time;
    }
    #endregion
}
