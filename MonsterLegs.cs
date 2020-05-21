﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster Legs", menuName = "Monster Legs")]
public class MonsterLegs : ScriptableObject
{
    public RuntimeAnimatorController controller;

    //Movement Variables
    public float moveSpeed;
    public float jumpForce;
    public float maxJumpFrames;//amount of frames you can continue moving upwards when holding the jump key
    public float maxDashFrames;
    public float dashTime;
    public float dashSpeed;
    public float gravity;
    public float fallingGravity;
    

    [Header("Standing LK")]
    public string standingLightName;
    public int standingLightDamage; //prototype name for standing normal attack's damage, discuss naming conventions later
    public int standingLightStartup; //# of windup frames before attack does damage
    public int standingLightActive; //The # of frames the attack can deal damage
    public int standingLightRecovery; //After the attack damage is dealed but before the player can attack again
    public int standingLightLength;

    [Header("Standing MK")]
    public string standingMediumName;
    public int standingMediumDamage; //prototype name for standing normal attack's damage, discuss naming conventions later
    public int standingMediumStartup; //# of windup frames before attack does damage
    public int standingMediumActive; //The # of frames the attack can deal damage
    public int standingMediumRecovery; //After the attack damage is dealed but before the player can attack again
    public int standingMediumLength;

    [Header("Standing HK")]
    public string standingHeavyName;
    public int standingHeavyDamage; //prototype name for standing normal attack's damage, discuss naming conventions later
    public int standingHeavyStartup; //# of windup frames before attack does damage
    public int standingHeavyActive; //The # of frames the attack can deal damage
    public int standingHeavyRecovery; //After the attack damage is dealed but before the player can attack again
    public int standingHeavyLength;

    [Header("Crouching LK")]
    public string crouchingLightName;
    public int crouchingLightDamage; //prototype name for crouching normal attack's damage, discuss naming conventions later
    public int crouchingLightStartup; //# of windup frames before attack does damage
    public int crouchingLightActive; //The # of frames the attack can deal damage
    public int crouchingLightRecovery; //After the attack damage is dealt but before the player can attack again
    public int crouchingLightLength;

    [Header("Crouching MK")]
    public string crouchingMediumName;
    public int crouchingMediumDamage; //prototype name for crouching normal attack's damage, discuss naming conventions later
    public int crouchingMediumStartup; //# of windup frames before attack does damage
    public int crouchingMediumActive; //The # of frames the attack can deal damage
    public int crouchingMediumRecovery; //After the attack damage is dealt but before the player can attack again
    public int crouchingMediumLength;

    [Header("Crouching HK")]
    public string crouchingHeavyName;
    public int crouchingHeavyDamage; //prototype name for crouching normal attack's damage, discuss naming conventions later
    public int crouchingHeavyStartup; //# of windup frames before attack does damage
    public int crouchingHeavyActive; //The # of frames the attack can deal damage
    public int crouchingHeavyRecovery; //After the attack damage is dealed but before the player can attack again
    public int crouchingHeavyLength;

    [Header("Air LK")]
    public string airLightName;
    public int airLightDamage; //prototype name for air normal attack's damage, discuss naming conventions later
    public int airLightStartup; //# of windup frames before attack does damage
    public int airLightActive; //The # of frames the attack can deal damage
    public int airLightRecovery; //After the attack damage is dealt but before the player can attack again
    public int airLightLength;

    [Header("Air MK")]
    public string airMediumName;
    public int airMediumDamage; //prototype name for air normal attack's damage, discuss naming conventions later
    public int airMediumStartup; //# of windup frames before attack does damage
    public int airMediumActive; //The # of frames the attack can deal damage
    public int airMediumRecovery; //After the attack damage is dealt but before the player can attack again
    public int airMediumLength;

    [Header("Air HK")]
    public string airHeavyName;
    public int airHeavyDamage; //prototype name for air normal attack's damage, discuss naming conventions later
    public int airHeavyStartup; //# of windup frames before attack does damage
    public int airHeavyActive; //The # of frames the attack can deal damage
    public int airHeavyRecovery; //After the attack damage is dealed but before the player can attack again
    public int airHeavyLength;
}