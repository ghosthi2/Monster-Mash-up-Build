using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerTests : MonoBehaviour
{
    private void Start()
    {
       // PlayerInputManager.instance.JoinPlayer();
    }
    private void onPlayerJoined()
    {
        Debug.Log("A player joined");
    }
}
