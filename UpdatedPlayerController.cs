using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedPlayerController : MonoBehaviour
{
    //Body Parts made from scriptable objects
    [Header("Body Objects")]
    public MonsterHead playerHead; 
    public MonsterArm playerArm;
    public MonsterLegs playerLegs;
    
    [Header("Body Sprites")]
    //These hold our sprite renderers and our animators
    public GameObject headPiece;
    public GameObject torsoPiece;
    public GameObject legPiece;

    //Opponent
    public GameObject target;

    //Movement Variables
    private float moveInput;
    bool isMoving;
    float lastInput;
    public float moveSpeed;
    public float jumpForce;
    float jumpInput; //determines which way the player moves when jumping; 
    float gravity;
    public float fallingGravity;
    public float maxJumpFrames;//amount of frames you can continue moving upwards when holding the jump key
    float jumpFrames;
    public float maxRedirectFrames;
    public float redirectFrames;
    public bool redirected;
    float goingBack = -1;
    float goingForward = 1;

    bool isDashing;
    bool dashLock;
    public float maxDashFrames;
    public float dashFrames;
    float dashSpeed;
    float dashInput;

    public string whichPlayer; //Determines inputs

    bool isGrounded, wasGrounded;
    bool isFacingRight = true;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //components
    public Rigidbody2D rb;
    private Animator acHead, acTorso, acLegs;//animator controller
    public Animator[] animators = new Animator[3];
    public SoundManager soma;
    public EffectsManager efma;

    //Player States
    public enum Movement { STANDING, BACK, FORWARD, JUMPING, FALLING, CROUCHING, DASHBACK, DASHFORWARD }
    public Movement currMovement;
    private Movement lastMovement;
    public enum Mobility { NORMAL, IMMOBILE, SLOWED, DISABLED }
    public Mobility currMobility;
    public enum Action { ATTACKING, BLOCKING, NONE };
    public Action currAction;

    bool busy = false;

    //Spawnables
    [Header("Spawnables")]
    public GameObject dashBackClouds;
    public GameObject dashBackSparks;
    public GameObject dashFwdClouds;
    public GameObject dashFwdSparks;
    public GameObject lightPunch;
    public GameObject lightKick;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        //get body part animators
        acHead = headPiece.GetComponent<Animator>();
        acHead.runtimeAnimatorController = playerHead.controller;
        acTorso = torsoPiece.GetComponent<Animator>();
        acTorso.runtimeAnimatorController = playerArm.controller;
        acLegs = legPiece.GetComponent<Animator>();
        acLegs.runtimeAnimatorController = playerLegs.controller;

        //store in animators list
        animators[0] = acHead;
        animators[1] = acTorso;
        animators[2] = acLegs;

        rb = GetComponent<Rigidbody2D>();
        soma = GetComponent<SoundManager>();
        efma = GetComponent<EffectsManager>();
        currAction = Action.NONE;
        currMobility = Mobility.NORMAL;
        

        //get values from parts
        moveSpeed = playerLegs.moveSpeed;
        jumpForce = playerLegs.jumpForce;
        maxJumpFrames = playerLegs.maxJumpFrames;
        maxDashFrames = playerLegs.maxDashFrames;
        dashSpeed = playerLegs.dashSpeed;
        gravity = playerLegs.gravity;
        fallingGravity = playerLegs.fallingGravity;
    }

    void FixedUpdate()
    {
        if (IsBusy() || isDashing)
            goto Skip;

        /*
        else
        {
            currMovement = Movement.STANDING;
        }*/

        //Returns true if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //If player isn't currently Attacking or Blocking, let them move
        if (currMobility == Mobility.NORMAL && currAction != Action.ATTACKING)
        {
            moveInput = Input.GetAxisRaw(whichPlayer + "Horizontal");
        }
        else { return; }


        //if the player was moving, and stopped moving
        if (lastInput != 0 && moveInput == 0)
        {
            dashFrames = maxDashFrames;
            dashInput = lastInput;
        }
        //If they still have time in the dash window
        if (dashFrames > 0)
        {
            dashFrames--;
            //And they pressed the same key
            if (moveInput == dashInput)
            {
                dashFrames = 0;
                StartCoroutine(Dash(Time.frameCount, playerLegs.dashTime, dashInput));
                //Return here so that we don't look at any of the other input logic
                return;
            }
        }



        //If player is on the ground
        if (isGrounded)
        {
            //Add velocity to player based on input
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            //Determine whether they are walking or standing
            if (moveInput == goingBack)
            {
                currMovement = Movement.BACK;
                currAction = Action.BLOCKING;
            }
            else if (moveInput == goingForward)
            {
                currMovement = Movement.FORWARD;
            }
            else if (currMovement != Movement.CROUCHING && currAction == Action.NONE)
            {
                currMovement = Movement.STANDING;
            }

            //Untrigger BLOCKING when no longer moving back
            if (lastInput == goingBack && moveInput != goingBack)
            {
                currAction = Action.NONE;
            }
        }
        //If player is not on the ground
        else
        {
            //Give them horizontal velocity at 75% of the normal rate (Horizontal movement is slower in the air)
            rb.velocity = new Vector2(jumpInput * moveSpeed * 0.75f, rb.velocity.y);
        }
        //If they have changed state since last frame
        if (currMovement != lastMovement)
        {
            //Call the animation handler
            //AnimationHandler(currMovement, lastMovement, currAction);
        }

        lastInput = moveInput;
        lastMovement = currMovement;
    Skip:;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBusy())
        {
            goto Skip;
        }
        if(isDashing)
        {
            Debug.Log($"DASHING: {dashInput}");
            Debug.Log($"DASHLOCK: {dashLock}");
            if(dashLock == false)
            {
                foreach(Animator ac in animators)
                { 
                    ac.SetBool("dashing", true);
                    ac.SetFloat("dashInput", dashInput*transform.localScale.x);
                }
                dashLock = true;
            }
            goto Skip;
        }
        isMoving = false;
        if(moveInput != 0)
        {
            isMoving = true;
        }
        if (rb.velocity.y > 0.1)
        {
            currMovement = Movement.JUMPING;
        }
        else if (rb.velocity.y < -0.1)
        {
            currMovement = Movement.FALLING;
        }

        //JUMPING CODE
        if (Input.GetButtonDown(whichPlayer + "Jump") && isGrounded && currAction != Action.ATTACKING)
        {
            Jump();
        }
        if ((currMovement == Movement.JUMPING || currMovement == Movement.FALLING) && !redirected)
        {
            if (moveInput != jumpInput && moveInput != 0)
            {
                jumpInput = moveInput;
                redirected = true;
            }
        }

        //Quick drop
        if (Input.GetButtonDown(whichPlayer + "Crouch") && !isGrounded)
        {
            Drop();
        }

        //Crouching input code
        if ((Input.GetButtonDown(whichPlayer + "Crouch") || Input.GetButton(whichPlayer + "Crouch")) && isGrounded)
        {
            currMovement = Movement.CROUCHING;
            currMobility = Mobility.IMMOBILE;
            rb.velocity = Vector2.zero;
        }
        else if (Input.GetButtonUp(whichPlayer + "Crouch") && isGrounded)
        {
            currMovement = Movement.STANDING;
            currMobility = Mobility.NORMAL;
        }

        //Increase gravity when falling so that players fall faster than they rise
        if (currMovement == Movement.FALLING)
        {
            rb.gravityScale = fallingGravity;
        }
        else
        {
            rb.gravityScale = gravity;
        }

        // If the input is moving the player right and the player is facing left...
        if (target.transform.position.x < transform.position.x && isFacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (target.transform.position.x > transform.position.x && !isFacingRight)
        {
            // ... flip the player.
            Flip();
        }

        /*if (currMovement != lastMovement)
        {
            AnimationHandler(currMovement, lastMovement, currAction);
        }*/

        //if we weren't grounded last frame, but we are this frame, play land FX
        if (!wasGrounded && isGrounded)
        {
            efma.SpawnEffect("land", groundCheck.transform);
        }
        lastMovement = currMovement;
        
        wasGrounded = isGrounded;
        
        foreach(Animator ac in animators)
        {
            ac.SetBool("grounded", isGrounded);
            ac.SetBool("moving", isMoving);
            ac.SetFloat("yInput", Input.GetAxisRaw(whichPlayer + "Vertical"));
            ac.SetFloat("xInput", moveInput*transform.localScale.x);
            ac.SetFloat("yVel", rb.velocity.y);
        }

    Skip:;
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    //JUMP
    private void Jump()
    {
        soma.PlaySound("jump");
        efma.SpawnEffect("jump", groundCheck.transform);
        jumpFrames = maxJumpFrames;
        redirectFrames = maxRedirectFrames;
        redirected = false;
        rb.velocity = Vector2.up * jumpForce;
        //save the direction they were already going in
        jumpInput = moveInput;
    }

    //DROP
    private void Drop()
    {
        rb.velocity = Vector2.down * jumpForce * 2;
    }

    //DASH
    private IEnumerator Dash(float currFrame, float dashTime, float direction)
    {
        //stops update and fixedupdate from running
        isDashing = true;
        dashLock = false;
        float timer = 0;
        float startFrame = currFrame;

        //if they are not facing the same direction they are pressing, they need to dash backwards
        if (direction != transform.localScale.x)
        {
            currMovement = Movement.DASHBACK;
            //AnimationHandler(currMovement, lastMovement, currAction);
            if(isFacingRight == false) //if they are not faceing right, flip their effects
            {
                efma.SpawnEffect("dashb", groundCheck.transform, -1); 
            }
            else
            {
                efma.SpawnEffect("dashb", groundCheck.transform);
            }
        }
        else //dash forwards
        {
            currMovement = Movement.DASHFORWARD;
            // AnimationHandler(currMovement, lastMovement, currAction);
            if (isFacingRight == false) //if they are not faceing right, flip their effects
            {
                efma.SpawnEffect("dashf", groundCheck.transform, -1);
            }
            else
            {
                efma.SpawnEffect("dashf", groundCheck.transform);
            }
        }

       

        soma.PlaySound("dash"); //play sound effect

        while (timer < dashTime)
        {
            rb.velocity = new Vector2(direction * moveSpeed * dashSpeed, rb.velocity.y);
            timer++;
            yield return new WaitForSeconds(1/60);
        }
        isDashing = false;//allow update and fixedupdate to run again
        dashLock = false;

        foreach (Animator ac in animators)
        { 
            ac.SetBool("dashing", false);
        }
        yield return null;
    }

    private void Flip()
    {
        float tempGoing = goingBack;
        goingBack = goingForward;
        goingForward = tempGoing;
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Disable means you can't do anything (stuns, knockbacks, etc)
    /*public void Disable(int duration, int displacement = 0)
    {
        disabledFrames = duration;
        //If facing right, get knocked left
        if (isFacingRight)
        {
            rb.velocity = new Vector2(displacement * -1 * moveSpeed * 2.5f, rb.velocity.y);
        }
        else //get knocked right
        {
            rb.velocity = new Vector2(displacement * 1 * moveSpeed * 2.5f, rb.velocity.y);
        }
    }*/

    public void StartDisable(float duration, float displacement = 0)
    {
        StartCoroutine(Disable(duration, displacement));
    }
    public IEnumerator Disable(float duration, float displacement = 0)
    {
        SetBusy(true);
        currMobility = Mobility.DISABLED;
        float startFrame = (int)(Time.fixedTime * 60);
        while((int)(Time.fixedTime * 60) < startFrame + duration)
        {
            //If facing right, get knocked left
            if (isFacingRight)
            {
                rb.velocity = new Vector2(displacement * -1 * moveSpeed * 2.5f, rb.velocity.y);
            }
            else //get knocked right
            {
                rb.velocity = new Vector2(displacement * 1 * moveSpeed * 2.5f, rb.velocity.y);
            }
            yield return new WaitForFixedUpdate();
        }
        currMobility = Mobility.NORMAL;
        SetBusy(false);
        yield return new WaitForFixedUpdate();
    }

    //Immobile means you can't move but you can do other actions
    /*public void Immobilize(int duration)
    {
        immobileFrames = duration;
        rb.velocity = Vector2.zero;
    }*/
    public IEnumerator Immobilize(float duration)
    {
        currMobility = Mobility.IMMOBILE;
        float startFrame = (int)(Time.fixedTime * 60);
        while ((int)(Time.fixedTime * 60) < startFrame + duration)
        {
            rb.velocity = Vector2.zero;
            yield return null;
        }
        currMobility = Mobility.NORMAL;
        yield return null;
    }


    public void SetBusy(bool val)
    {
        busy = val;
    }

    public bool IsBusy()
    {
        return busy;
    }


    private void OnEnable()
    {
        //Flip the player in the right direction initially
        // If the input is moving the player right and the player is facing left...
        if (target.transform.position.x < transform.position.x && isFacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (target.transform.position.x > transform.position.x && !isFacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
}


/*HIGH JUMP CODE
 * 
 * if (Input.GetButton(whichPlayer + "Jump") && currMovement == Movement.JUMPING)
      {
          //Allow the player to continue moving upwards with 85% of their normal jumpforce
          if (jumpFrames > 0)
          {
              rb.velocity = Vector2.up * (jumpForce * 0.85f);
              jumpFrames--;
          }
          else
          {
              currMovement = Movement.FALLING;
          }
      }

      if (Input.GetButtonUp(whichPlayer + "Jump"))
      {
          currMovement = Movement.FALLING;
          jumpFrames = 0;
      }*/



//ANIMATION HANDLER
/*private void AnimationHandler(Movement movement, Movement last, Action action)
{
    if (action == Action.ATTACKING)
        return;
    //Resets all animation triggers to avoid buggy animations
    foreach (Animator ac in animators)
    {

        foreach (AnimatorControllerParameter param in ac.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
                ac.ResetTrigger(param.name);
            if (param.type == AnimatorControllerParameterType.Bool)
                ac.SetBool(param.name, false);
        }
    }

    if (last == Movement.FALLING && isGrounded)
    {
        foreach (Animator ac in animators)
        {
            ac.SetTrigger("Land");
        }
    }
    switch (movement)
    {
        case Movement.CROUCHING:
            foreach (Animator ac in animators)
            {
                ac.SetBool("Crouch", true);
            }
            break;
        case Movement.DASHFORWARD:
            foreach (Animator ac in animators)
            {
                ac.SetBool("DashFwd", true);
            }
            break;
        case Movement.DASHBACK:
            foreach (Animator ac in animators)
            {
                ac.SetBool("DashBack", true);
            }
            break;
        case Movement.JUMPING:
            foreach (Animator ac in animators)
            {
                ac.SetBool("Jump", true);
            }
            break;
        case Movement.FALLING:
            foreach (Animator ac in animators)
            {
                ac.SetBool("Fall", true);
            }
            break;
        case Movement.STANDING:

            foreach (Animator ac in animators)
            {
                ac.SetBool("Idle", true);
            }
            break;
        default:
            foreach (Animator ac in animators)
            {
                ac.SetTrigger("Idle");
            }
            break;
    }
}*/
