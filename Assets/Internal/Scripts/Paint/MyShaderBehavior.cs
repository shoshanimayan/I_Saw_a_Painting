using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MyShaderBehavior : MonoBehaviour
{
    [Tooltip("Number of pixels per 1 unit of size in world coordinates.")]
    [Range(1, 8182)]
    public int textureSize = 64;
    private readonly Color _color = new Color(0, 0, 0, 0);
    private Material _material;
    private Texture2D _texture;
    private bool _isEnabled = false;
    private object m_lockFlag = new object();


    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (null == renderer)
            return;

        foreach (Material material in renderer.materials)
        {
            if (material.shader.name.Contains("Custom/PaintShaders/"))
            {
                _material = material;
                break;
            }
        }

        if (null != _material)
        {
            _texture = new Texture2D(textureSize, textureSize);
           // for (int x = 0; x < textureSize; ++x)
           //     for (int y = 0; y < textureSize; ++y)
           //        _texture.SetPixel(x, y, _color);
            _texture.Apply();
            _material.SetTexture("_DrawingTex", _texture);
            _isEnabled = true;
        }

    }

    public void PaintOnColored(Vector2 textureCoord, float[,] splashTexture, Color color)
    {
        MyPaintOn(textureCoord, splashTexture, color);
    }

    public void RestartColor()
    {

        if (null != _material)
        {
            _texture = new Texture2D(textureSize, textureSize);
            _texture.Apply();
            _material.SetTexture("_DrawingTex", _texture);
            _isEnabled = true;
        }

    }

    private void MyPaintOn(Vector2 textureCoord, float[,] splashTexture, Color targetColor)
    { 
        if (_isEnabled)
        {
            lock (m_lockFlag)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                int reqnx = splashTexture.GetLength(0);
                int reqny = splashTexture.GetLength(1);
                int reqX = (int)(textureCoord.x * textureSize) - (reqnx / 2);
                int reqY = (int)(textureCoord.y * textureSize) - (reqny / 2);
                int right = _texture.width - 1;
                int bottom = _texture.height - 1;

                int x = IntMax(reqX, 0);
                int y = IntMax(reqY, 0);
                int nx = IntMin(x + reqnx, right) - x;
                int ny = IntMin(y + reqny, bottom) - y;
                
                Color[] pixels = _texture.GetPixels(x, y, nx, ny);

                int counter = 0;
                for (int i = 0; i < nx; ++i)
                {
                    for (int j = 0; j < ny; ++j)
                    {
                        float currAlpha = splashTexture[i,j];
                        if (currAlpha == 1)
                            pixels[counter] = targetColor;
                        else
                        {
                            Color currColor = pixels[counter];
                            // resulting color is an addition of splash texture to the texture based on alpha
                            Color newColor = Color.Lerp(currColor, targetColor, currAlpha);
                            // but resulting alpha is a sum of alphas (adding transparent color should not make base color more transparent)
                            newColor.a = pixels[counter].a + currAlpha;
                            pixels[counter] = newColor;
                        }
                        counter++;
                    }
                }
                
                _texture.SetPixels(x, y, nx, ny, pixels);
                _texture.Apply();
                sw.Stop();
            }
        }
    }

    private int IntMax(int a, int b)
    {
        return a > b ? a : b;
    }

    private int IntMin(int a, int b)
    {
        return a < b ? a : b;
    }
}
