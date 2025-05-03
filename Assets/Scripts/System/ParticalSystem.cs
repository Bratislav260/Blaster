using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public Particle[] Particles;
    public static ParticleManager Instance { get; private set; }

    public void Initialize()
    {
        Instance = this;
    }

    public void CallPartical(string nameParticle, Transform target, bool isChangeColor = false)
    {
        foreach (var p in Particles)
        {
            if (p.name == nameParticle)
            {
                ParticleSystem tempPartical = Instantiate(p.particle, target.position, Quaternion.identity);

                if (isChangeColor && target.TryGetComponent<IColorful>(out var colorful))
                {
                    var main = tempPartical.main;

                    Color newColor = colorful.color;
                    newColor.a = 1f;
                    main.startColor = newColor;
                }
            }
        }
    }
}