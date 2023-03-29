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

    private void Start()
    {
        InvokeRepeating(nameof(Stopper), 3, 1);
    }

    bool paused;
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