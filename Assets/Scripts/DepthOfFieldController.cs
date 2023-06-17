using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DepthOfFieldController : MonoBehaviour
{
    public PostProcessProfile profile;
    public LayerMask enviroMask;
    public float closeDistance = 0.1f;
    DepthOfField dof;
    bool dofAvailable;
    string hitObject;

    private void Start()
    {
        dofAvailable = profile.TryGetSettings<DepthOfField>(out dof);
    }

    private void Update()
    {
        RaycastEnviro();
        AdjustDepthOfField();
    }

    private void AdjustDepthOfField()
    {
        if(dofAvailable)
        {
            if (distance < 0.5f)
            {
                distance = closeDistance;
            }
            float current = dof.focusDistance;
            dof.focusDistance.value = Mathf.Lerp(current, distance, Time.deltaTime * focusSpeed);
            //dof.focusDistance.value = distance;

        }
    }

    float distance;
    public float focusSpeed = 1;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.25f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        Gizmos.DrawSphere(transform.position + transform.forward * distance, 0.5f);
    }

    private void RaycastEnviro()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        bool didHit = Physics.Raycast(ray, out hit, 100, enviroMask);

        if(didHit)
        {
            distance = hit.distance;
            hitObject = hit.collider.name;
        }
    }
}
