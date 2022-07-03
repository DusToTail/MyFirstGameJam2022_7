using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogProjector : MonoBehaviour
{
    public Material projectorMaterial;
    public float blendSpeed;
    public int textureScale;

    public RenderTexture fogTexture;

    private RenderTexture _prevTexture;
    private RenderTexture _currTexture;
    private Projector _projector;

    private float blendAmount;

    private void Awake()
    {
        _projector = GetComponent<Projector>();
        _projector.enabled = true;

        _prevTexture = GenerateTexture();
        _currTexture = GenerateTexture();
        // Projector materials aren't instanced, resulting in the material asset getting changed.
        // Instance it here to prevent us from having to check in or discard these changes manually.
        _projector.material = new Material(projectorMaterial);
        _projector.material.SetTexture("_PrevTexture", _prevTexture);
        _projector.material.SetTexture("_CurrTexture", _currTexture);

        StartNewBlend();
    }

    RenderTexture GenerateTexture()
    {
        RenderTexture rt = new RenderTexture(
            fogTexture.width * textureScale,
            fogTexture.height * textureScale,
            0,
            fogTexture.format)
        { filterMode = FilterMode.Bilinear };
        rt.antiAliasing = fogTexture.antiAliasing;

        return rt;
    }

    public void StartNewBlend()
    {
        StopCoroutine(BlendFog());
        blendAmount = 0;
        // Swap the textures
        Graphics.Blit(_currTexture, _prevTexture);
        Graphics.Blit(fogTexture, _currTexture);

        StartCoroutine(BlendFog());
    }

    IEnumerator BlendFog()
    {
        while (blendAmount < 1)
        {
            // increase the interpolation amount
            blendAmount += Time.deltaTime * blendSpeed;
            // Set the blend property so the shader knows how much to lerp
            // by when checking the alpha value
            _projector.material.SetFloat("_Blend", blendAmount);
            yield return null;
        }
        // once finished blending, swap the textures and start a new blend
        StartNewBlend();
    }
}
