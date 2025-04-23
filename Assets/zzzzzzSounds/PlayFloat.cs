using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFloat : MonoBehaviour
{
    [SerializeField] private AudioClip flap;
    public void PlayFlapSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(flap, transform, 1f);
    }
}
