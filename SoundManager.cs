using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AllSounds soundPack;
    AudioSource aSrc;
    float dashVol = 0.25f;
    float jumpVol = 0.1f;
    float attackVol = 0.05f;
    private void Start()
    {
        aSrc = GetComponent<AudioSource>();
    }
    public void PlaySound(string name)
    {
        switch (name)
        {
            case "dash":
                aSrc.pitch = Random.Range(1, 2);
                aSrc.PlayOneShot(GetRandom(soundPack.dashClips), dashVol);
                break;
            case "jump":
                aSrc.pitch = Random.Range(1, 2);
                aSrc.PlayOneShot(GetRandom(soundPack.jumpClips), jumpVol);
                break;
            case "fall":
                break;
            case "light":
                aSrc.pitch = Random.Range(1, 2);
                aSrc.PlayOneShot(GetRandom(soundPack.lightClips), attackVol);
                break;
            case "medium":
                break;
            case "heavy":
                break;
        }    

    }

    AudioClip GetRandom(AudioClip[] clipList)
    {
        AudioClip clip;
        int x = Random.Range(0, clipList.Length);
        clip = clipList[x];
        return clip;
    }
}
