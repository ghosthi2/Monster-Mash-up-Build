using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Don't use this... ever... it lags the game so bad
//Counts every 1/60th of a second using fixedUpdate, which runs in 60 fps, so we have a counter for 60 fps
public class FrameCounter : MonoBehaviour
{
    int currFrame = 0;
    int currDisplayFPS;

    private void Awake()
    {
        Time.fixedDeltaTime = 1 / 60;
    }
    void FixedUpdate()
    {
        currFrame = (int)(Time.fixedTime * 60);
    }
    private void Update()
    {
        currDisplayFPS = (int)(1f / Time.unscaledDeltaTime);
    }
}
