using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealShuffle : MonoBehaviour
{
    [SerializeField] private AudioClip sealShuffle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SoundFXManager.instance.PlaySoundFXClip(sealShuffle, transform, 1f);
    }
}

