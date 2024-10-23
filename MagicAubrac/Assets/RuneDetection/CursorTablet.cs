using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTablet : MonoBehaviour
{
    [SerializeField] ParticleSystem _sandParticles;

    public void StartParticles()
    {
        _sandParticles.Play();
    }

    public void StopParticles()
    {
        _sandParticles.Stop();
    }
}
