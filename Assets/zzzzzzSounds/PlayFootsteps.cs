using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;
    public void PlaySound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(footsteps, transform, 1f);
    }
}
