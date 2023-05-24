using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanim : MonoBehaviour
{
    public DoorMechanim[] doorToOpen;
    public Key.KeyType keyColor;
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
                } 
            }
       }
    }

    public void Open()
    {
        foreach(DoorMechanim d in doorToOpen)
        {
            d.open = true;
        }
    }
}
