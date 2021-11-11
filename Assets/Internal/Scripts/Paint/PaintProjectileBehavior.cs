using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInfo
{
    public string _name;
    public Vector2 _coordinate;
    public Color _color;
    public int _alphaNumber;

    public SplashInfo(string name, Vector2 coordinate, Color color, int alphaNumber)
    {
        _name = name;
        _coordinate = coordinate;
        _color = color;
        _alphaNumber = alphaNumber;
    }
}

public class PaintProjectileBehavior : MonoBehaviour
{
    public float paintDiameter = 1.5f;
    
    private bool _isActive = false;
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }
    private AudioManager _audioManager { get { return AudioManager.Instance; } }

    private void Start()
    {
        GetComponent<Renderer>().material.SetColor("_Color",_manager.paintBombColor);
        if (paintDiameter > 0)
        {

            _isActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
            return;
        _isActive = false;
        Destroy(gameObject);
        ParticleSystem burstParticle = Instantiate(_manager.burstParticlePrefab);
        burstParticle.transform.position = transform.position;
        var burstSettings = burstParticle.main;
        if (other.gameObject.GetComponent<PaintChangeObjectBehavior>())
        {
            other.gameObject.GetComponent<PaintChangeObjectBehavior>().ChangeColor();
            burstSettings.startColor = _manager.paintBombColor;
            burstParticle.Play();
            _audioManager.PlayClip("sparkle");
            return;
        }

        burstSettings.startColor = _manager.paintBombColor;
        burstParticle.Play();
        _audioManager.PlayClip("splat");



        for (int i = 0; i < 14; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _manager.GetRandomSphereRay(), out hit, paintDiameter))
            {
                if (hit.collider is MeshCollider)
                {
                    MyShaderBehavior script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                    if (null != script)
                    {
                        var alphaNumber = _manager.GetRandomAlphaNumber();

                        SplashInfo splashInfo = new SplashInfo(hit.collider.gameObject.name, hit.textureCoord2, _manager.paintBombColor, alphaNumber);
                        script.PaintOnColored(hit.textureCoord2, _manager.GetProjectileSplash(alphaNumber), _manager.paintBombColor);
                        GameManager.AddSplash(JsonUtility.ToJson(splashInfo));
                    }
                }
            }
        }





    }
}
