using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RenderCam : MonoBehaviour
{
    public Vector2 textureOrigin;
    public bool getFromScreenForOrigin;
    public Vector2 textureDestiny;
    public bool getFromScreenForDestiny;
    private int[] _reso;
    private int[] _resd;
    private Image image;
    private RenderTexture renderTexture;
    private RenderTexture printTexture;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        ApplyResolution();
    }

    // Update is called once per frame
    void Update()
    {
        GetFrame();
    }

    private void ApplyResolution()
    {
        var o = getFromScreenForOrigin ? new Vector2(Screen.width, Screen.height) : textureOrigin;
        var d = getFromScreenForDestiny ? new Vector2(Screen.width, Screen.height) : textureDestiny;
        _reso = new int[] { (int)o.x, (int)o.y };
        _resd = new int[] { (int)d.x, (int)d.y };

        renderTexture = new RenderTexture(_reso[0], _reso[1], 16);
        printTexture = new RenderTexture(_resd[0], _resd[1], 16);
    }

    private void GetFrame()
    {
        var c = Camera.main;
        c.targetTexture = renderTexture;
        c.Render();
        c.targetTexture = null;
        Graphics.Blit(renderTexture, printTexture);
        image.sprite = Sprite.Create(toTexture2D(printTexture), new Rect(0, 0, printTexture.width, printTexture.height), new Vector2(0, 0), 100);
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(_resd[0], _resd[1], TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }


}
