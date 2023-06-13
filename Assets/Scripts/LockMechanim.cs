using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockMechanim : MonoBehaviour
{
    public UnityEvent OnUnlocked;
    public UnityEvent OnOpened;

    public DoorMechanim[] doorToOpen;
    public Key.KeyType keyColor;
    public float gearsDelay = 1;

    bool playerInRange;
    bool alreadyOpen = false; 

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player out of range");
            playerInRange = false;
        }
    }

    private void Update()
    {
       if( Input.GetKeyDown(KeyCode.E) && playerInRange )
       {
            if ( !alreadyOpen )
            {
                bool playerHasProperKey = GameManager.Instance.CheckTheKey(keyColor);
                if (playerHasProperKey)
                {
                    GameManager.Instance.UseTheKey(keyColor);
                    animator.SetTrigger("open");
                    alreadyOpen = true;
                    OnUnlocked.Invoke();
                } 
            }
       }
    }

    public void Open()
    {
        foreach(DoorMechanim d in doorToOpen)
        {
            d.open = true;
            Invoke(nameof(Opened), gearsDelay);
        }
    }

    void Opened()
    {
        OnOpened.Invoke();
    }
}
