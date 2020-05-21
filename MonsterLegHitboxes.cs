using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLegHitboxes : MonoBehaviour
{
    [Header("Standing LK")]
    public BoxCollider2D[] standingLightHitboxes;
    public BoxCollider2D[] standingLightHurtboxes;

    [Header("Standing MK")]
    public BoxCollider2D[] standingMediumHitboxes;
    public BoxCollider2D[] standingMediumHurtboxes;

    [Header("Standing HK")]
    public BoxCollider2D[] standingHeavyHitboxes;
    public BoxCollider2D[] standingHeavyHurtboxes;

    [Header("Crouching LK")]
    public BoxCollider2D[] crouchingLightHitboxes;
    public BoxCollider2D[] crouchingLightHurtboxes;

    [Header("Crouching MK")]
    public BoxCollider2D[] crouchingMediumHitboxes;
    public BoxCollider2D[] crouchingMediumHurtboxes;

    [Header("Crouching HK")]
    public BoxCollider2D[] crouchingHeavyHitboxes;
    public BoxCollider2D[] crouchingHeavyHurtboxes;

    [Header("Air LK")]
    public BoxCollider2D[] airLightHitboxes;
    public BoxCollider2D[] airLightHurtboxes;

    [Header("Air MK")]
    public BoxCollider2D[] airMediumHitboxes;
    public BoxCollider2D[] airMediumHurtboxes;

    [Header("Air HK")]
    public BoxCollider2D[] airHeavyHitboxes;
    public BoxCollider2D[] airHeavyHurtboxes;
}