using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustPaticleControll : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true;
    [SerializeField] private ParticleSystem dustParticleSystem;

    public void CreateParticles()
    {
        if (createDustOnWalk)
        {
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}
