using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapesMixer : MonoBehaviour
{
    public SkinnedMeshRenderer smr;
    public float mixSpeed = 1;
    public float tempoOffset = 0.1f;
    [Range(0,100)]
    public float influence = 100;
    int shapesCount;

    private void Start()
    {
        shapesCount = smr.sharedMesh.blendShapeCount;
    }

    private void Update()
    {
        smr.SetBlendShapeWeight(0, (Mathf.Sin(Time.time * mixSpeed) + 1) *0.5f * influence);

        for(int i=1; i < shapesCount; i++)
        {
            smr.SetBlendShapeWeight(i, (Mathf.Sin(Time.time * mixSpeed + i * tempoOffset) + 1) * 0.5f * influence);
        }
    }
}
