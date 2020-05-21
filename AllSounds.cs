using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Pack", menuName = "Sound Pack")]
public class AllSounds : ScriptableObject
{
    [Header("Wooshes")]
    public AudioClip[] dashClips = new AudioClip[3];
    public AudioClip[] jumpClips = new AudioClip[3];
    public AudioClip[] lightClips = new AudioClip[3];


}
