using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action<bool> Paused;
    public static event Action Lose;
    public static event Action Win;

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


    //Game Logic
    public int timer = 60;
    bool paused;

    //Pickups
    public int DiamondsCount { get; private set; }
    
    public int RedKeys { get; private set; }
    public int GreenKeys { get; private set; }
    public int GoldKeys { get; private set; }

    public bool Freeze { get; private set; }

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
    public void FreezeTime( int time )
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
    public void AddTime( int time )
    {
        timer += time;
    }

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

        if(gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }

    private void Resume()
    {
        Paused?.Invoke(false);

        paused = false;
        Time.timeScale = 1;
    }

    private void Pause()
    {
        Paused?.Invoke(true);

        paused = true;
        Time.timeScale = 0;
    }

    void Stopper()
    {
        Freeze = false;
        timer--;
        if(timer < 1)
        {
            GameOver();
        }
    }

    bool gameEnded;
    public void GameOver()
    {
        gameEnded = true;
        CancelInvoke();
        Lose?.Invoke();
    }
    public void GameWin()
    {
        gameEnded = true;
        CancelInvoke();
        Win?.Invoke();
    }
}
