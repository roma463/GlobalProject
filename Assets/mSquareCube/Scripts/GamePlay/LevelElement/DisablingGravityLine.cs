using UnityEngine;

public class DisablingGravityLine : InvisibleBlock
{
    [SerializeField] private ParticleSystem[] _particles;

    public override void Start()
    {
        base.Start();
        PlayingStateParticle();
    }
    public override void Launch(bool state)
    {
        base.Launch(state);
        PlayingStateParticle();
    }
    private void PlayingStateParticle()
    {
        foreach (var item in _particles)
        {
            if (!IsActive)
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
