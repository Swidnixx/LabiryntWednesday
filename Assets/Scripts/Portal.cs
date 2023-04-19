using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer renderer;

    Camera portalCam;
    Transform playerCam;

    private void Awake()
    {
        playerCam = Camera.main.transform;
        portalCam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        RenderTexture rt = new RenderTexture( Screen.width, Screen.height, 0);
        portalCam.targetTexture = rt;
        linkedPortal.renderer.material.SetTexture("_MainTex", rt);
    }

    private void Update()
    {
        Matrix4x4 m = transform.localToWorldMatrix *
            linkedPortal.transform.worldToLocalMatrix *
            playerCam.localToWorldMatrix;

        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }
}
