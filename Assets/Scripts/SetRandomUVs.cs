using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomUVs : MonoBehaviour
{
    public MeshFilter mf;

    private void Start()
    {
        if (Random.Range(0,1)==0)
        {
            mf.mesh.uv = Random.Range(0, 1) == 0 ? mf.mesh.uv2 : mf.mesh.uv3; 
        }
    }
}
