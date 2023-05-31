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


    //Game Logic
    public int timer = 60;
    bool paused;

    //Pickups
    int diamondsCount;

    int redKeys;
    int greenKeys;
    int goldKeys;

    internal void AddKey(Key.KeyType type)
    {
        switch (type)
        {
            case Key.KeyType.Red:
                redKeys++;
                break;
            case Key.KeyType.Green:
                greenKeys++;
                break;
            case Key.KeyType.Gold:
                goldKeys++;
                break;
        }
    }
    public void FreezeTime( int time )
    {
        CancelInvoke();
        InvokeRepeating(nameof(Stopper), time, 1);
    }

    internal bool CheckTheKey(Key.KeyType keyColor)
    {
        switch (keyColor)
        {
            case Key.KeyType.Red:
                return redKeys > 0;
                
            case Key.KeyType.Green:
                return greenKeys > 0;
                
            case Key.KeyType.Gold:
                return goldKeys > 0;
        }

        return false;
    }

    internal void UseTheKey(Key.KeyType keyColor)
    {
        switch (keyColor)
        {
            case Key.KeyType.Red:
                redKeys--;
                break;
            case Key.KeyType.Green:
                greenKeys--;
                break;
            case Key.KeyType.Gold:
                goldKeys--;
                break;
        }
    }

    public void AddDiamond()
    {
        diamondsCount++;
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
    }

    private void Resume()
    {
        paused = false;
        Time.timeScale = 1;
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    void Stopper()
    {
        timer--;
    }
}
