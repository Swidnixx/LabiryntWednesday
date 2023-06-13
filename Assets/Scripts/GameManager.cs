using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    //Main Game Events
    public static event Action<bool> Paused;
    public static event Action Lose;
    public static event Action Win;

    //Game Logic
    public int timer = 60;
    bool paused;
    public bool Freeze { get; private set; }

    //Pickups
    public int DiamondsCount { get; private set; }

    public int RedKeys { get; private set; }
    public int GreenKeys { get; private set; }
    public int GoldKeys { get; private set; }

    private void Start()
    {
        InvokeRepeating(nameof(Stopper), 3, 1);
    }

    private void Update()
    {
        if( Input.GetButtonDown("Cancel") )
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
        Debug.Log("Game Over");
        Lose?.Invoke();
    }
    public void GameWin()
    {
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
