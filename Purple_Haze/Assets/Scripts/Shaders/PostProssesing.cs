using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProssesing : MonoBehaviour
{
    public Shader NoirShader;
    private Material MyMat;

    void Update()
    {
        
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (MyMat == null)
        {
            MyMat = new Material(NoirShader);
        }

        Graphics.Blit(source, destination, MyMat);
    }
}
