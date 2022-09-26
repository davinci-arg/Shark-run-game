using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableDepth : MonoBehaviour
{
	[SerializeField] private Material _mat;
    [SerializeField] private Color _fogColor;
	public int width = 512;
	public int height = 512;

	private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        camera.depthTextureMode = DepthTextureMode.Depth;

#if UNITY_EDITOR
        // Force Game View to render depth in the editor.
        camera.forceIntoRenderTexture = true;
#endif
    }

    private void Update()
    {
        _mat.SetColor("_FogColor", _fogColor);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, _mat);
		//mat is the material which contains the shader
		//we are passing the destination RenderTexture to

	}

	}
