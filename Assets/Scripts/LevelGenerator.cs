using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D picture;
    public ColorToPrefabMapping[] mappings;
    public float tileSize = 5;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        for(int x=0; x < picture.width; x++)
        {
            for( int y=0; y < picture.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile( int x, int y)
    {
        Color pixel = picture.GetPixel(x, y);

        foreach( var m in mappings )
        {
            if( m.color.Equals( pixel ) )
            {
                GameObject tile = Instantiate(m.prefab, transform);
                Vector3 position = new Vector3(x, 0, y) * tileSize;
                tile.transform.localPosition = position;
            }
        }
    }

    [Serializable]
    public class ColorToPrefabMapping
    {
        public Color color;
        public GameObject prefab;
    }
}
