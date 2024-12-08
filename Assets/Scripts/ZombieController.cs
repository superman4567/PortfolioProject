using System;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public enum ZombieType
    {
        Idler,
        Runner
    }

    [SerializeField] private ZombieType type;
    [SerializeField] private Animator animator;

    private void Start()
    {
        PlayAnimation(type);
    }

    public void PlayAnimation(ZombieType type)
    {
        float randomNormalizedTime = UnityEngine.Random.Range(0f, 1f);

        if (type == ZombieType.Idler)
        {
            float randomNumber = UnityEngine.Random.Range(0f, 1f);
            {
                if (randomNumber < 0.5f) // 50% chance for the first animation
                {
                    animator.Play("AN_Zombie_Idle", 0, randomNormalizedTime);
                }
                else // 50% chance for the second animation
                {
                    animator.Play("AN_Zombie_Idle2", 0, randomNormalizedTime);
                }
            }
        }

        if (type == ZombieType.Runner)
        {
            animator.Play("AN_Zombie_Run", 0, randomNormalizedTime);
        }
    }

    public void PlayHitByExplosion()
    {
        animator.Play("AN_Zombie_HitByExplosion", 0);

        Debug.Log("HitByExplosion");
    }
}
