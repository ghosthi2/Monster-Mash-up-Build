using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effects Pack", menuName = "Effects Pack")]
public class AllEffects : ScriptableObject
{
    [Header("Dash Effects")]
    public GameObject dashBackClouds;
    public GameObject dashBackSparks;
    public GameObject dashFwdClouds;
    public GameObject dashFwdSparks;
    [Header("Jump Effects")]
    public GameObject jumpClouds;
    public GameObject landClouds;
    [Header("Attack Effects")]
    public GameObject lightPunch;
    public GameObject mediumPunch;
    public GameObject heavyPunch;
    public GameObject lightKick;
    public GameObject mediumKick;
    public GameObject heavyKick;
    [Header("Block Effect")]
    public GameObject blocked;
}
