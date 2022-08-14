using UnityEngine;

public class DisablingGravityLine : InvisibleBlock
{
    [SerializeField] private ParticleSystem[] _particles;

    public override void Start()
    {
        base.Start();
        PlayingStateParticle();
    }
    public override void launch()
    {
        base.launch();
        PlayingStateParticle();
    }
    private void PlayingStateParticle()
    {
        foreach (var item in _particles)
        {
            if (!CurrentActivity)
            {
                item.Stop();
            }
            else
            {
                item.Play();
            }
        }
    }
}
