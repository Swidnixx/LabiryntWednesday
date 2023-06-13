using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensivity = 250;
    public Transform player;

    float xRotation = 0;
    private void Start()
    {
       // player = transform.parent;
       // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.unscaledDeltaTime * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70, 60);
        transform.localRotation = Quaternion.Euler( xRotation, 0, 0 );

        player.Rotate(0, mouseX, 0);
    }
}
