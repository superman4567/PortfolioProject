using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfxUXUI1;
    [SerializeField] private ParticleSystem vfxUXUI2;
    [Space]
    [SerializeField] private ParticleSystem vfx3D1;
    [SerializeField] private ParticleSystem vfx3D2;
    [SerializeField] private ParticleSystem vfx3D3;
    [Space]
    [SerializeField] private ParticleSystem vfxProgramming1;
    [SerializeField] private ParticleSystem vfxProgramming2;

    private ParticleSystem[] vfxArray;

    private void Awake()
    {
        vfxArray = new ParticleSystem[]
        {
            vfxUXUI1, vfxUXUI2,
            vfx3D1, vfx3D2, vfx3D3,
            vfxProgramming1, vfxProgramming2
        };
    }

    public void PlayUXUI1()
    {
        vfxUXUI1.Play();
    }

    public void PlayUXUI2()
    {
        vfxUXUI2.Play();
    }

    public void Play3D1()
    {
        vfx3D1.Play();
    }

    public void Play3D2()
    {
        vfx3D2.Play();
    }

    public void Play3D3()
    {
        vfx3D3.Play();
    }

    public void PlayProgramming1()
    {
        vfxProgramming1.Play();
    }

    public void PlayProgramming2()
    {
        vfxProgramming2.Play();
    }
}
