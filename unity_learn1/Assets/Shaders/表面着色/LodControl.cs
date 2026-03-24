using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodControl : MonoBehaviour
{
    public Shader shader;
    public int Lod_value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //当前这个shader最大的lod
        shader.maximumLOD = Lod_value;
    }
}
