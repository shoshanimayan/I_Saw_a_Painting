using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintProjectileManager : Singleton<PaintProjectileManager>
{
    public ParticleSystem burstParticlePrefab;
    public GameObject paintBombPrefab;
    public Color paintBombColor = Color.black;
    [SerializeField] private  Texture2D[] projectileSplashTextures;

    private List<float[,]> _projSplashTextures;
    private int _projSplashTexturesCount;

    private void Awake()
    {
        // Strip unused color information
        // Later we may need to clip a rectangle from the texture, so we'll store it as two-dimensional array for convenience
        _projSplashTexturesCount = projectileSplashTextures.Length;
        _projSplashTextures = new List<float[,]>(_projSplashTexturesCount);
        for (int i = 0; i < _projSplashTexturesCount; ++i)
        {
            Texture2D texture = projectileSplashTextures[i];
            int textureWidth = texture.width;
            int textureHeight = texture.height;
            Color[] currTexture = texture.GetPixels();
            float[,] textureAlphas = new float[textureWidth, textureHeight];
            int counter = 0;
            for (int x = 0; x < textureWidth; ++x)
            {
                for (int y = 0; y < textureHeight; ++y)
                {
                    textureAlphas[x, y] = currTexture[counter].a;
                    counter++;
                }
            }
            _projSplashTextures.Add(textureAlphas);
        }
    }

    public float[,] GetRandomProjectileSplash()
    {
        return _projSplashTextures[Random.Range(0, _projSplashTexturesCount)];
    }
    public float[,] GetProjectileSplash(int i)
    {
        return _projSplashTextures[i];
    }

    public int GetRandomAlphaNumber()
    { return Random.Range(0, _projSplashTexturesCount); }

    public Vector3 GetRandomSphereRay()
    {
        // it is possible we will want to use predefined set of vectors here
        return Random.onUnitSphere;
    }
}
