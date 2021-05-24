using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ContrastShaderScript : MonoBehaviour {
    [Range (0f,1.0f)]public float factor;
    private Material material;
    // Creates a private material used to the effect
    void Awake ()
    {
        material = new Material( Shader.Find("Hidden/ContrastShader") );
    }
    
    // Postprocess the image
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        if (factor == 0)
        {
            Graphics.Blit (source, destination);
            return;
        }
        material.SetFloat("_Factor", factor);
        Graphics.Blit (source, destination, material);
    }
}