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

    public AudioClip keyUnlockClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
            if (!alreadyOpen)
            {
                DisplayUI.Instance.DisplayInfo("Press E to Open if you have proper key"); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayUI.Instance.ClearInfoText();
           playerInRange = false;
        }
    }
    Transform player;
        float dot = 0;
    private void Update()
    {
        dot = 0;
        if(playerInRange)
        {

            dot = Vector3.Dot(player.forward, (transform.position - player.position).normalized);
            if (dot > 0.5f)
            {
                DisplayUI.Instance.DisplayInfo("Press E to Open if you have proper key");
            }
            else
            {
                DisplayUI.Instance.ClearInfoText();
            }
        }


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
                    DisplayUI.Instance.ClearInfoText();
                    SoundManager.Instance.PlaySFX( keyUnlockClip );
                } 
            }
       }
    }

    public void Open()
    {
        OnUnlocked.Invoke();
        Invoke(nameof(Opened), gearsDelay);
        foreach (DoorMechanim d in doorToOpen)
        {
            d.open = true;
        }
    }

    void Opened()
    {
        OnOpened.Invoke();
    }
}
