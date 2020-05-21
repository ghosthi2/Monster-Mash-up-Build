using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdatedPlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    GameObject choiceHolder;
    ChoiceHolder choices;
    string whichPlayer;
    UpdatedPlayerController pc;
    const int LP = 0;
    const int LK = 1;
    const int MP = 2;
    const int MK = 3;
    const int HP = 4;
    const int HK = 5;
    /*const int LP = 0;
    const int LP = 0;
    */
    public LayerMask enemyLayers;

    //Attack frame + damage data and hitboxes
    public MonsterArm arms;
    public MonsterArmHitboxes armHitboxes;
    public MonsterLegs legs;
    public MonsterLegHitboxes legHitboxes;

    int nextAttackFrame = 0; //The amount of frames that must pass before the player can attack again

    AttackData[] myAttacks = new AttackData[18];

    //private BoxCollider2D localCollider;
    //public BoxCollider2D[] standingLightHitboxColliders = new BoxCollider2D[2]; //Will go in own prefab

    private void Start()
    {

        pc = GetComponent<UpdatedPlayerController>();
        playerInput = GetComponent<PlayerInput>();

        whichPlayer = pc.whichPlayer;
        arms = pc.playerArm;
        legs = pc.playerLegs;


        Debug.Log("playerInput for " + whichPlayer);//test

        foreach (InputDevice device in playerInput.devices)
        {
            Debug.Log("Device: " + device);//test
        }
        Debug.Log("Past playerInput for " + whichPlayer);//test

        choiceHolder = GameObject.Find("ChoiceHolder");
        if (choiceHolder == null)
        {
            Debug.Log("CAN'T ACCESS PLAYER CHOICES IN PlayerCombat, try playing from the characterCreation screen, dummy parts will be used"); //test
        }
        else
        {
            Debug.Log("Accessed player choices while in PlayerCombat"); //test
            choices = choiceHolder.GetComponent<ChoiceHolder>();
        }

        //Apply choices to this player
        SetChoices();
        //Debug.Log("Past setChoices for " + whichPlayer); //test

        //Set up the localCollider --not in use rn--
        //localCollider = gameObject.AddComponent<BoxCollider2D>();
        //localCollider.isTrigger = true;

        myAttacks[0] = new AttackData(arms.standingLightName, arms.standingLightDamage, arms.standingLightStartup, arms.standingLightActive, arms.standingLightRecovery, armHitboxes.standingLightHitboxes);
        arms.standingLightLength = arms.standingLightStartup + arms.standingLightActive + arms.standingLightRecovery;
        myAttacks[1] = new AttackData(arms.standingMediumName, arms.standingMediumDamage, arms.standingMediumStartup, arms.standingMediumActive, arms.standingMediumRecovery, armHitboxes.standingMediumHitboxes);
        arms.standingMediumLength = arms.standingMediumStartup + arms.standingMediumActive + arms.standingMediumRecovery;
        myAttacks[2] = new AttackData(arms.standingHeavyName, arms.standingHeavyDamage, arms.standingHeavyStartup, arms.standingHeavyActive, arms.standingHeavyRecovery, armHitboxes.standingHeavyHitboxes);
        arms.standingHeavyLength = arms.standingHeavyStartup + arms.standingHeavyActive + arms.standingHeavyRecovery;
        myAttacks[3] = new AttackData(legs.standingLightName, legs.standingLightDamage, legs.standingLightStartup, legs.standingLightActive, legs.standingLightRecovery, legHitboxes.standingLightHitboxes);
        legs.standingLightLength = legs.standingLightStartup + legs.standingLightActive + legs.standingLightRecovery;
        myAttacks[4] = new AttackData(legs.standingMediumName, legs.standingMediumDamage, legs.standingMediumStartup, legs.standingMediumActive, legs.standingMediumRecovery, legHitboxes.standingMediumHitboxes);
        legs.standingMediumLength = legs.standingMediumStartup + legs.standingMediumActive + legs.standingMediumRecovery;
        myAttacks[5] = new AttackData(legs.standingHeavyName, legs.standingHeavyDamage, legs.standingHeavyStartup, legs.standingHeavyActive, legs.standingHeavyRecovery, legHitboxes.standingHeavyHitboxes);
        legs.standingHeavyLength = legs.standingHeavyStartup + legs.standingHeavyActive + legs.standingHeavyRecovery;

        myAttacks[6] = new AttackData(arms.crouchingLightName, arms.crouchingLightDamage, arms.crouchingLightStartup, arms.crouchingLightActive, arms.crouchingLightRecovery, armHitboxes.crouchingLightHitboxes);
        arms.crouchingLightLength = arms.crouchingLightStartup + arms.crouchingLightActive + arms.crouchingLightRecovery;
        myAttacks[7] = new AttackData(arms.crouchingMediumName, arms.crouchingMediumDamage, arms.crouchingMediumStartup, arms.crouchingMediumActive, arms.crouchingMediumRecovery, armHitboxes.crouchingMediumHitboxes);
        arms.crouchingMediumLength = arms.crouchingMediumStartup + arms.crouchingMediumActive + arms.crouchingMediumRecovery;
        myAttacks[8] = new AttackData(arms.crouchingHeavyName, arms.crouchingHeavyDamage, arms.crouchingHeavyStartup, arms.crouchingHeavyActive, arms.crouchingHeavyRecovery, armHitboxes.crouchingHeavyHitboxes);
        arms.crouchingHeavyLength = arms.crouchingHeavyStartup + arms.crouchingHeavyActive + arms.crouchingHeavyRecovery;
        myAttacks[9] = new AttackData(legs.crouchingLightName, legs.crouchingLightDamage, legs.crouchingLightStartup, legs.crouchingLightActive, legs.crouchingLightRecovery, legHitboxes.crouchingLightHitboxes);
        legs.crouchingLightLength = legs.crouchingLightStartup + legs.crouchingLightActive + legs.crouchingLightRecovery;
        myAttacks[10] = new AttackData(legs.crouchingMediumName, legs.crouchingMediumDamage, legs.crouchingMediumStartup, legs.crouchingMediumActive, legs.crouchingMediumRecovery, legHitboxes.crouchingMediumHitboxes);
        legs.crouchingMediumLength = legs.crouchingMediumStartup + legs.crouchingMediumActive + legs.crouchingMediumRecovery;
        myAttacks[11] = new AttackData(legs.crouchingHeavyName, legs.crouchingHeavyDamage, legs.crouchingHeavyStartup, legs.crouchingHeavyActive, legs.crouchingHeavyRecovery, legHitboxes.crouchingHeavyHitboxes);
        legs.crouchingHeavyLength = legs.crouchingHeavyStartup + legs.crouchingHeavyActive + legs.crouchingHeavyRecovery;

        myAttacks[12] = new AttackData(arms.airLightName, arms.airLightDamage, arms.airLightStartup, arms.airLightActive, arms.airLightRecovery, armHitboxes.airLightHitboxes);
        arms.airLightLength = arms.airLightStartup + arms.airLightActive + arms.airLightRecovery;
        myAttacks[13] = new AttackData(arms.airMediumName, arms.airMediumDamage, arms.airMediumStartup, arms.airMediumActive, arms.airMediumRecovery, armHitboxes.airMediumHitboxes);
        arms.airMediumLength = arms.airMediumStartup + arms.airMediumActive + arms.airMediumRecovery;
        myAttacks[14] = new AttackData(arms.airHeavyName, arms.airHeavyDamage, arms.airHeavyStartup, arms.airHeavyActive, arms.airHeavyRecovery, armHitboxes.airHeavyHitboxes);
        arms.airHeavyLength = arms.airHeavyStartup + arms.airHeavyActive + arms.airHeavyRecovery;
        myAttacks[15] = new AttackData(legs.airLightName, legs.airLightDamage, legs.airLightStartup, legs.airLightActive, legs.airLightRecovery, legHitboxes.airLightHitboxes);
        legs.airLightLength = legs.airLightStartup + legs.airLightActive + legs.airLightRecovery;
        myAttacks[16] = new AttackData(legs.airMediumName, legs.airMediumDamage, legs.airMediumStartup, legs.airMediumActive, legs.airMediumRecovery, legHitboxes.airMediumHitboxes);
        legs.airMediumLength = legs.airMediumStartup + legs.airMediumActive + legs.airMediumRecovery;
        myAttacks[17] = new AttackData(legs.airHeavyName, legs.airHeavyDamage, legs.airHeavyStartup, legs.airHeavyActive, legs.airHeavyRecovery, legHitboxes.airHeavyHitboxes);
        legs.airHeavyLength = legs.airHeavyStartup + legs.airHeavyActive + legs.airHeavyRecovery;

        //Disable the player until the timer enables them
        this.enabled = false;
        pc.enabled = false;
        playerInput.enabled = false;
        Debug.Log("Player " + whichPlayer + "'s controller, combat, and input components are disabled until the round starts"); //test

        for (int i = 0; i < myAttacks.Length; i++)
        {
            Debug.Log("Player " + whichPlayer + ": " + i + ": " + myAttacks[i]);
        }
    }

    private void OnMove(InputValue value)
    {
        Debug.Log("Move called"); //test
    }

    private void OnLightPunch()
    {
        int currFrame = (int)(Time.fixedTime * 60); //Current frame

        if (currFrame >= nextAttackFrame 
            && pc.currMobility != UpdatedPlayerController.Mobility.DISABLED)
        {
            AnimSetAttack(LP);
            if (pc.currMovement == UpdatedPlayerController.Movement.STANDING
            || pc.currMovement == UpdatedPlayerController.Movement.FORWARD
            || pc.currMovement == UpdatedPlayerController.Movement.BACK)
            {
                StartCoroutine(DoAttack(currFrame, myAttacks[0]));
                nextAttackFrame = currFrame + arms.standingLightLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.CROUCHING)
            {
                AnimationHandler("CrouchPunchLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[6]));
                nextAttackFrame = currFrame + arms.crouchingLightLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.JUMPING || pc.currMovement == UpdatedPlayerController.Movement.FALLING)
            {
                AnimationHandler("AirPunchLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[12]));
                nextAttackFrame = currFrame + arms.airLightLength;
            }
        }
        else
        {
            Debug.Log("Cannot LP, " + nextAttackFrame + " > " + currFrame);
        }
    }
    private void OnMediumPunch()
    {
        int currFrame = (int)(Time.fixedTime * 60); //Current frame

        if (currFrame >= nextAttackFrame
            && pc.currMobility != UpdatedPlayerController.Mobility.DISABLED)
        {
            if (pc.currMovement == UpdatedPlayerController.Movement.STANDING
            || pc.currMovement == UpdatedPlayerController.Movement.FORWARD
            || pc.currMovement == UpdatedPlayerController.Movement.BACK)
            {
                AnimSetAttack(MP);
                AnimationHandler("PunchMedium");
                StartCoroutine(DoAttack(currFrame, myAttacks[1]));
                nextAttackFrame = currFrame + arms.standingMediumLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.CROUCHING)
            {
                AnimSetAttack(LP);
                AnimationHandler("CrouchPunchLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[6]));
                nextAttackFrame = currFrame + arms.crouchingLightLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.JUMPING || pc.currMovement == UpdatedPlayerController.Movement.FALLING)
            {
                AnimSetAttack(LP);
                AnimationHandler("AirPunchLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[12]));
                nextAttackFrame = currFrame + arms.airLightLength;
            }
        }
        else
        {
            Debug.Log("Cannot MP, " + nextAttackFrame + " > " + currFrame);
        }
    }
    private void OnHeavyPunch()
    {
        int currFrame = (int)(Time.fixedTime * 60); //Current frame

        if (currFrame >= nextAttackFrame
            && pc.currMobility != UpdatedPlayerController.Mobility.DISABLED)
        {
            if (pc.currMovement == UpdatedPlayerController.Movement.STANDING
            || pc.currMovement == UpdatedPlayerController.Movement.FORWARD
            || pc.currMovement == UpdatedPlayerController.Movement.BACK)
            {
                AnimSetAttack(HP);
                AnimationHandler("PunchHeavy");
                StartCoroutine(DoAttack(currFrame, myAttacks[2]));
                nextAttackFrame = currFrame + arms.standingHeavyLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.CROUCHING)
            {
                AnimSetAttack(LP);
                AnimationHandler("CrouchPunchLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[6]));
                nextAttackFrame = currFrame + arms.crouchingLightLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.JUMPING || pc.currMovement == UpdatedPlayerController.Movement.FALLING)
            {
                AnimSetAttack(LP);
                AnimationHandler("AirPunchLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[12]));
                nextAttackFrame = currFrame + arms.airLightLength;
            }
        }
        else
        {
            Debug.Log("Cannot HP, " + nextAttackFrame + " > " + currFrame);
        }
    }
    private void OnLightKick()
    {
        int currFrame = (int)(Time.fixedTime * 60); //Current frame

        if (currFrame >= nextAttackFrame && !pc.IsBusy())
        {
            if (pc.currMovement == UpdatedPlayerController.Movement.STANDING
            || pc.currMovement == UpdatedPlayerController.Movement.FORWARD
            || pc.currMovement == UpdatedPlayerController.Movement.BACK)
            {
                AnimSetAttack(LK);
                AnimationHandler("KickLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[3]));
                nextAttackFrame = currFrame + legs.standingLightLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.CROUCHING)
            {
                AnimSetAttack(HK);
                AnimationHandler("CrouchKickHeavy");
                StartCoroutine(DoAttack(currFrame, myAttacks[11]));
                nextAttackFrame = currFrame + legs.crouchingHeavyLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.JUMPING || pc.currMovement == UpdatedPlayerController.Movement.FALLING)
            {
                AnimSetAttack(LK);
                AnimationHandler("AirKickLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[15]));
                nextAttackFrame = currFrame + legs.airLightLength;
            }
        }
        else
        {
            Debug.Log("Cannot LK, " + nextAttackFrame + " > " + currFrame);
        }
    }
    private void OnMediumKick()
    {
        int currFrame = (int)(Time.fixedTime * 60);

        if (currFrame >= nextAttackFrame && !pc.IsBusy())
        {
            if (pc.currMovement == UpdatedPlayerController.Movement.STANDING
            || pc.currMovement == UpdatedPlayerController.Movement.FORWARD
            || pc.currMovement == UpdatedPlayerController.Movement.BACK)
            {
                AnimSetAttack(MK);
                AnimationHandler("KickMedium");
                StartCoroutine(DoAttack(currFrame, myAttacks[4]));
                nextAttackFrame = currFrame + legs.standingMediumLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.CROUCHING)
            {
                AnimSetAttack(HK);
                AnimationHandler("CrouchKickHeavy");
                StartCoroutine(DoAttack(currFrame, myAttacks[11]));
                nextAttackFrame = currFrame + legs.crouchingHeavyLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.JUMPING || pc.currMovement == UpdatedPlayerController.Movement.FALLING)
            {
                AnimSetAttack(LK);
                AnimationHandler("AirKickLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[15]));
                nextAttackFrame = currFrame + legs.airLightLength;
            }
        }
        else
        {
            Debug.Log("Cannot MK, " + nextAttackFrame + " > " + currFrame);
        }
    }
    private void OnHeavyKick()
    {
        int currFrame = (int)(Time.fixedTime * 60);

        if (currFrame >= nextAttackFrame && !pc.IsBusy())
        {
            if (pc.currMovement == UpdatedPlayerController.Movement.STANDING
            || pc.currMovement == UpdatedPlayerController.Movement.FORWARD
            || pc.currMovement == UpdatedPlayerController.Movement.BACK)
            {
                AnimSetAttack(HK);
                AnimationHandler("KickHeavy");
                StartCoroutine(DoAttack(currFrame, myAttacks[5]));
                nextAttackFrame = currFrame + legs.standingHeavyLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.CROUCHING)
            {
                AnimSetAttack(HK);
                AnimationHandler("CrouchKickHeavy");
                StartCoroutine(DoAttack(currFrame, myAttacks[11]));
                nextAttackFrame = currFrame + legs.crouchingHeavyLength;
            }
            else if (pc.currMovement == UpdatedPlayerController.Movement.JUMPING || pc.currMovement == UpdatedPlayerController.Movement.FALLING)
            {
                AnimSetAttack(LK);
                AnimationHandler("AirKickLight");
                StartCoroutine(DoAttack(currFrame, myAttacks[15]));
                nextAttackFrame = currFrame + legs.airLightLength;
            }
        }
        else
        {
            Debug.Log("Cannot HK, " + nextAttackFrame + " > " + currFrame);
        }
    }

#if false
    public void SetHitBox(int nextHitbox)
    {
        if (nextHitbox >= armHitboxes.standingLightHitboxes.Length || nextHitbox != -1) //the -1 is to make it easy to clear the localCollider using events
        {
            localCollider.size = armHitboxes.standingLightHitboxes[nextHitbox].size;
            return;
        }
        localCollider.size = new Vector2(0, 0);
    }
#endif

    //Attack function that takes the starting frame and the attack object, which has the array of colliders for the attack, the damage, startup, active, and recovery frames
    IEnumerator DoAttack(int startFrame, AttackData attack)
    {
        int currFrame = (int)(Time.fixedTime * 60);
        pc.rb.velocity = Vector3.zero;
        pc.currAction = UpdatedPlayerController.Action.ATTACKING;
        pc.Immobilize(attack.m_length);
        pc.soma.PlaySound("light");
        Debug.Log(pc.currMovement); //test
        Debug.Log(attack); //test
        foreach (Animator ac in pc.animators)
        {
            ac.SetTrigger("attacking");
        }
        while (currFrame < startFrame + attack.m_startup)
        {
            int frameCountdown = startFrame + attack.m_startup - currFrame; //test
            Debug.Log("Frames till " + attack.m_name + " is active " + frameCountdown); //test
            currFrame = (int)(Time.fixedTime * 60);
            yield return new WaitForFixedUpdate();
            //yield return null;
        }
        
        bool dealtDamage = false; //Tracks if the opponent has already been hit so that they don't get hit more than once during an attack
        while (currFrame < startFrame + attack.m_startup + attack.m_active && dealtDamage == false)
        {
            //Detect enemies in range of every hitbox
            foreach (BoxCollider2D box in attack.m_hitboxes)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(box.transform.position, box.transform.localScale, box.transform.eulerAngles.z, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    //If what was hit is a hurtbox
                    if (enemy.tag == "Hurtbox")
                    {
                        Debug.Log(this.name + " hit " + enemy.name + " for " + attack.m_damage + " using " + box.name + " from " + attack.m_name); //test
                        enemy.GetComponent<UpdatedHealth>().TakeDamage(attack.m_damage, attack.m_name);
                        pc.currAction = UpdatedPlayerController.Action.NONE;
                        yield break; //return early
                    }
                    else
                    {
                        Debug.Log(this.name + " hit enemy's " + enemy.name + " using " + box.name + " from " + attack.m_name + " but " + enemy.name + " is not a hurtbox"); //test
                    }
                }
            }
            pc.currAction = UpdatedPlayerController.Action.NONE;
            foreach (Animator ac in pc.animators)
            {
                ac.ResetTrigger("attacking");
            }
            currFrame = (int)(Time.fixedTime * 60);
            yield return new WaitForFixedUpdate();
            //yield return null;
        }
        pc.currAction = UpdatedPlayerController.Action.NONE;
    }

    private void SetChoices()
    {
        Debug.Log("SetChoices called in " + whichPlayer); //test
        string partName = "Dummy"; //default part
        if (choiceHolder == null)
        {
            Debug.Log("choiceHolder is null so using default player parts");
            arms = Resources.Load<MonsterArm>(partName + "Arms");
            armHitboxes = GameObject.Find("/" + whichPlayer + "/" + partName).GetComponent<MonsterArmHitboxes>(); //Get the arm colliders from the monster gameObject
            Debug.Log("Set " + whichPlayer + " arm choice to default choice " + partName); //test
            legs = Resources.Load<MonsterLegs>(partName + "Legs");
            legHitboxes = GameObject.Find("/" + whichPlayer + "/" + partName).GetComponent<MonsterLegHitboxes>(); //Get the colliders from the monster gameObject
            Debug.Log("Set " + whichPlayer + " leg choice to default choice " + partName); //test
            return;
        }

        if (whichPlayer == "P1")
        {
            //Set attacks using p1choice, put in for loop for all 3 parts later
            switch (ChoiceHolder.P1Choice[1])
            {
                case 0:
                    partName = "Dummy";
                    break;
                case 1:
                    partName = "Mummy";
                    break;
                case 2:
                    partName = "Werewolf";
                    break;
                case 3:
                    partName = "Vampire";
                    break;
            }
            arms = Resources.Load<MonsterArm>(partName + "Arms");
            armHitboxes = gameObject.transform.Find(partName).GetComponent<MonsterArmHitboxes>(); //Get the arm colliders from the monster gameObject
            Debug.Log("Set P1 arm choice to " + partName);

            switch (ChoiceHolder.P1Choice[2])
            {
                case 0:
                    partName = "Dummy";
                    break;
                case 1:
                    partName = "Mummy";
                    break;
                case 2:
                    partName = "Werewolf";
                    break;
                case 3:
                    partName = "Vampire";
                    break;
            }
            legs = Resources.Load<MonsterLegs>(partName + "Legs");
            legHitboxes = gameObject.transform.Find(partName).GetComponent<MonsterLegHitboxes>(); //Get the colliders from the monster gameObject
            Debug.Log("Set P1 leg choice to " + partName);
        }
        else
        {
            switch (ChoiceHolder.P2Choice[1])
            {
                case 0:
                    partName = "Dummy";
                    break;
                case 1:
                    partName = "Mummy";
                    break;
                case 2:
                    partName = "Werewolf";
                    break;
                case 3:
                    partName = "Vampire";
                    break;
            }
            arms = Resources.Load<MonsterArm>(partName + "Arms");
            armHitboxes = gameObject.transform.Find(partName).GetComponent<MonsterArmHitboxes>(); //Get the arm colliders from the monster gameObject
            Debug.Log("Set P2 arm choice to " + partName);

            switch (ChoiceHolder.P2Choice[2])
            {
                case 0:
                    partName = "Dummy";
                    break;
                case 1:
                    partName = "Mummy";
                    break;
                case 2:
                    partName = "Werewolf";
                    break;
                case 3:
                    partName = "Vampire";
                    break;
            }
            legs = Resources.Load<MonsterLegs>(partName + "Legs");
            legHitboxes = gameObject.transform.Find(partName).GetComponent<MonsterLegHitboxes>(); //Get the colliders from the monster gameObject
            Debug.Log("Set P2 leg choice to " + partName);
        }
    }

    private void AnimationHandler(string trigger)
    {
        foreach (Animator anim in pc.animators)
        {
            foreach (AnimatorControllerParameter param in anim.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Trigger)
                    anim.ResetTrigger(param.name);
                if (param.type == AnimatorControllerParameterType.Bool)
                    anim.SetBool(param.name, false);
            }
            
        }
        foreach (Animator anim in pc.animators)
        { 
            anim.SetTrigger(trigger);
        }
    } 
    
    private void AnimSetAttack(int attack)
    {
        foreach (Animator ac in pc.animators)
        {
            ac.SetFloat("attack", attack);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(arms.standingLightHitboxes.GetComponent<BoxCollider2D>().transform.position, arms.standingLightHitboxes.GetComponent<BoxCollider2D>().size); //Punch Light
    }     
}