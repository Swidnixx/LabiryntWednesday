using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer renderer;
    public PortalTeleport teleport;

    public UnityEvent OnTeleportFrom;
    public UnityEvent OnTeleportTo;

    Camera portalCam;
    Transform playerCam;

    private void Awake()
    {
        teleport.linkedPortal = linkedPortal.teleport;
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

        Vector3 cameraToPortal = transform.position - portalCam.transform.position;
        float nearPlane = cameraToPortal.magnitude;
        if (nearPlane > 0.2f)
            portalCam.nearClipPlane = Mathf.Clamp(nearPlane - 0.2f, 0.01f, 50);
        else
            portalCam.nearClipPlane = 0.01f;

    }
}
