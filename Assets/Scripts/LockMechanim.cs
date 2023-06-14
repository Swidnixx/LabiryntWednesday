using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMechanim : MonoBehaviour
{
    public DoorMechanim[] doorToOpen;
    public Key.KeyType keyColor;

    public Renderer[] lockModel;
    public Material goldMat;
    public Material redMat;
    public Material greenMat;
    
    bool playerInRange;
    bool alreadyOpen = false; 

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        AssignMaterial();
    }
    void AssignMaterial()
    {
        foreach(var r in lockModel)
        {
            switch (keyColor)
            {
                case Key.KeyType.Red:
                    r.material = redMat;
                    break;
                case Key.KeyType.Green:
                    r.material = greenMat;
                    break;
                case Key.KeyType.Gold:
                    r.material = goldMat;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !alreadyOpen)
        {
            DisplayUI.Instance.DisplayInfo("Press E to Unlock");
            playerInRange = true;
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
                    DisplayUI.Instance.ClearInfoText();
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
