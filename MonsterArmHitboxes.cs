using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make sure hitboxes are set to be triggers
public class MonsterArmHitboxes : MonoBehaviour
{
    [Header("Grab Attack")]
    public BoxCollider2D[] grabHitboxes;
    public BoxCollider2D[] grabHurtboxes;

    [Header("Standing LP")]
    public BoxCollider2D[] standingLightHitboxes;
    public BoxCollider2D[] standingLightHurtboxes;

    [Header("Standing MP")]
    public BoxCollider2D[] standingMediumHitboxes;
    public BoxCollider2D[] standingMediumHurtboxes;

    [Header("Standing HP")]
    public BoxCollider2D[] standingHeavyHitboxes;
    public BoxCollider2D[] standingHeavyHurtboxes;

    [Header("Crouching LP")]
    public BoxCollider2D[] crouchingLightHitboxes;
    public BoxCollider2D[] crouchingLightHurtboxes;

    [Header("Crouching MP")]
    public BoxCollider2D[] crouchingMediumHitboxes;
    public BoxCollider2D[] crouchingMediumHurtboxes;

    [Header("Crouching HP")]
    public BoxCollider2D[] crouchingHeavyHitboxes;
    public BoxCollider2D[] crouchingHeavyHurtboxes;

    [Header("Air LP")]
    public BoxCollider2D[] airLightHitboxes;
    public BoxCollider2D[] airLightHurtboxes;

    [Header("Air MP")]
    public BoxCollider2D[] airMediumHitboxes;
    public BoxCollider2D[] airMediumHurtboxes;

    [Header("Air HP")]
    public BoxCollider2D[] airHeavyHitboxes;
    public BoxCollider2D[] airHeavyHurtboxes;
}