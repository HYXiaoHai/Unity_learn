using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader_ctrl : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        material.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
