using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedHealth : MonoBehaviour
{
    GameObject opponent;
    public HealthBar healthBar;
    UpdatedPlayerController pc;
    RoundHandler rh;
    AudioSource audio;
    public AudioClip hitSound;
    public AudioClip blockSound;

    public int maxHealth = 100;
    public int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        pc = GetComponent<UpdatedPlayerController>();
        rh = GameObject.Find("Canvas/VictoryTally").GetComponent<RoundHandler>();
        opponent = pc.target;
        audio = GetComponent<AudioSource>();

        Physics2D.IgnoreLayerCollision(gameObject.layer, opponent.layer, false); //this player and the other player can collide with each other's layers
        Debug.Log(gameObject.layer + " and " + opponent.layer + " can collide"); //test
    }

    public void TakeDamage(int damage, string name)
    {
        audio.pitch = Random.Range(1, 2);
        if (pc.currAction == UpdatedPlayerController.Action.BLOCKING)
        {
            foreach(Animator ac in pc.animators)
            {
                ac.SetTrigger("blocked");
            }
            pc.efma.SpawnEffect("block", transform);
            pc.StartDisable(5, 1);
            //Take damage
            int chipDamage = (int)((float)damage * 0.2); //recieve 1/5th of damage
            currHealth -= chipDamage; 
            Debug.Log(pc.whichPlayer + " blocked, damage recieved was: " + chipDamage);
            healthBar.SetHealth(currHealth);
            audio.PlayOneShot(blockSound, 0.3f);
        }
        else
        {
            foreach (Animator ac in pc.animators)
            {
                ac.SetTrigger("damaged");
            }
            pc.StartDisable(15, 1);
            HitFX(name);
            //Take damage
            currHealth -= damage;
            healthBar.SetHealth(currHealth);
            //play hurt animation if the player is still fighting, otherwise let the knockout anim play
            if (currHealth > 0)
            {
                //Play damage animation
                //animator.SetTrigger("Hurt");
            }
            audio.PlayOneShot(hitSound, 0.25f);
        }

        if (currHealth <= 0)
        {
            Knockout();
        }
    }

    //This function will call the Victory function for the opponent player
    public void Knockout()
    {
        Debug.Log(this.name + " is defeated"); //test
        //Knockout animation + Victory animation for opponent
        AnimationHandler("Defeated");
        opponent.GetComponent<UpdatedHealth>().Victory();

        //Stop the timer
        GameObject timer = GameObject.Find("Timer");
        timer.GetComponent<Timer>().RoundOver();

        //Disable the character's collision with the opponent but nothing else
        Physics2D.IgnoreLayerCollision(gameObject.layer, opponent.layer); //Makes it so that this player and the other player cannot collide with each other's layers anymore
        Debug.Log(gameObject.layer + " and " + opponent.layer + " can no longer collide"); //test

        //Stop this player
        pc.SetBusy(true);
        Kill();
    }

    void Victory()
    {
        Debug.Log(this.name + " is victorious"); //test
        AnimationHandler("Victorious");
        rh.RoundWinner(this.name);

        pc.SetBusy(true);
        Kill();
    }

    //At the end of a round, stop the character from moving around or attacking
    void Kill()
    {
        //GetComponent<PlayerController>().Immobilize(3600);
        GetComponent<UpdatedPlayerCombat>().enabled = false;
        GetComponent<UpdatedPlayerController>().enabled = false;
    }

    void HitFX(string name)
    {
        if (name.Contains("LK"))
        {
            pc.efma.SpawnEffect("lightk", transform);
        }
        else if (name.Contains("LP"))
        {
            pc.efma.SpawnEffect("lightp", transform);
        }
        else if (name.Contains("MP"))
        {
            pc.efma.SpawnEffect("mediump", transform);
        }
        else if (name.Contains("MK"))
        {
            pc.efma.SpawnEffect("mediumk", transform);
        }
        else if (name.Contains("HP"))
        {
            pc.efma.SpawnEffect("heavyp", transform);
        }
        else if (name.Contains("HK"))
        {
            pc.efma.SpawnEffect("heavyk", transform);
        }
    }
    private void AnimationHandler(string trigger)
    {
        foreach (Animator anim in pc.animators)
        {
            foreach (AnimatorControllerParameter animParam in anim.parameters)
            {
                if (animParam.type == AnimatorControllerParameterType.Trigger)
                {
                    anim.ResetTrigger(animParam.name);
                }
            }
        }
        foreach (Animator anim in pc.animators)
        {
            anim.SetTrigger(trigger);
        }
    }
}
