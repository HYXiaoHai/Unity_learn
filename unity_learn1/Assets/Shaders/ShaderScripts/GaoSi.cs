using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaoSi : MonoBehaviour
{
    public Shader myShader;
    public Material myMaterial;
    void Start()
    {
        myMaterial = new Material(myShader);
    }

    void Update()
    {
        
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, myMaterial);
    }
}
