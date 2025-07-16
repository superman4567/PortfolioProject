using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject vfxUXUI1;
    [SerializeField] private GameObject vfxUXUI2;
    [Space]
    [SerializeField] private GameObject vfx3D1;
    [SerializeField] private GameObject vfx3D2;
    [SerializeField] private GameObject vfx3D3; 
    [Space]
    [SerializeField] private GameObject vfxProgramming1;
    [SerializeField] private GameObject vfxProgramming2;

    private GameObject[] vfxArray;

    private void Awake()
    {
        vfxArray = new GameObject[]
        {
            vfxUXUI1, vfxUXUI2,
            vfx3D1, vfx3D2, vfx3D3,
            vfxProgramming1, vfxProgramming2
        };
    }

    public void PlayUXUI1()
    {
        vfxUXUI1.SetActive(true);
    }

    public void PlayUXUI2()
    {
        vfxUXUI2.SetActive(true);
    }

    public void Play3D1()
    {
        vfx3D1.SetActive(true);
    }

    public void Play3D2()
    {
        vfx3D2.SetActive(true);
    }

    public void Play3D3()
    {
        vfx3D3.SetActive(true);
    }

    public void PlayProgramming1()
    {
        vfxProgramming1.SetActive(true);
    }

    public void PlayProgramming2()
    {
        vfxProgramming2.SetActive(true);
    }

    public void DisableAll()
    {
        foreach (var vfx in vfxArray)
        {
            vfx.SetActive(false);
        }
    }
}
