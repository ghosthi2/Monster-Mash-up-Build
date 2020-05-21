using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParts : MonoBehaviour
{
    UpdatedPlayerController pc;
    string whichPlayer;

    [Header("DUMMY PARTS")]
   public MonsterHead dh; //dummy head
   public MonsterArm dt; //dummy torso
   public MonsterLegs dl; //dummy legs
   [Header("MUMMY PARTS")]
   public MonsterHead mh; //mummy head
   public MonsterArm mt; //mummy torso
   public MonsterLegs ml; //mummy legs
   [Header("VAMPIRE PARTS")]
   public MonsterHead vh;
   public MonsterArm vt; 
   public MonsterLegs vl;
   [Header("WEREWOLF PARTS")]
   public MonsterHead wh;
   public MonsterArm wt; 
   public MonsterLegs wl;

    void Awake()
    {
        pc = GetComponent<UpdatedPlayerController>();
        whichPlayer = pc.whichPlayer;

        
        if(whichPlayer == "P1")
        {
            P1Update();
        }
        else
        {
            P2Update();
        }
       
    }

    void P1Update()
    {
        switch(ChoiceHolder.P1Choice[0])
        {
            case 0:
                pc.playerHead = dh;
                break;
            case 1:
                pc.playerHead = mh;
                break;
            case 2:
                pc.playerHead = wh;
                break;
            case 3:
                pc.playerHead = vh;
                break;
        }
        switch (ChoiceHolder.P1Choice[1])
        {
            case 0:
                pc.playerArm = dt;
                break;
            case 1:
                pc.playerArm = mt;
                break;
            case 2:
                pc.playerArm = wt;
                break;
            case 3:
                pc.playerArm = vt;
                break;
        }
        switch (ChoiceHolder.P1Choice[2])
        {
            case 0:
                pc.playerLegs = dl;
                break;
            case 1:
                pc.playerLegs = ml;
                break;
            case 2:
                pc.playerLegs = wl;
                break;
            case 3:
                pc.playerLegs = vl;
                break;
        }
    }

    void P2Update()
    {
        switch (ChoiceHolder.P2Choice[0])
        {
            case 0:
                pc.playerHead = dh;
                break;
            case 1:
                pc.playerHead = mh;
                break;
            case 2:
                pc.playerHead = wh;
                break;
            case 3:
                pc.playerHead = vh;
                break;
        }
        switch (ChoiceHolder.P2Choice[1])
        {
            case 0:
                pc.playerArm = dt;
                break;
            case 1:
                pc.playerArm = mt;
                break;
            case 2:
                pc.playerArm = wt;
                break;
            case 3:
                pc.playerArm = vt;
                break;
        }
        switch (ChoiceHolder.P2Choice[2])
        {
            case 0:
                pc.playerLegs = dl;
                break;
            case 1:
                pc.playerLegs = ml;
                break;
            case 2:
                pc.playerLegs = wl;
                break;
            case 3:
                pc.playerLegs = vl;
                break;
        }
    }
}
