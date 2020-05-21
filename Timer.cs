using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Timer : MonoBehaviour
{
    public float targetTime = 100.0f;
    public float beforeRoundTime = 4.0f;
    public float afterRoundTime = 8.0f;
    public GameObject Tens;
    public GameObject Ones;

    public GameObject CountdownDisplay;
    public Sprite FightImage;
    RoundHandler rh;
    
    public Sprite[] numbers = new Sprite[10];

    bool roundStarted = false;
    bool stopTime = false;

    private void Start()
    {
        rh = GameObject.Find("Canvas/VictoryTally").GetComponent<RoundHandler>();
    }

    // Update is called once per frame
    void Update()
    {        
        //test
        //For displaying progess in sample scene
        /*if (Input.GetKeyDown("0"))
        {
            SceneManager.LoadScene("SampleScene");
        }*/

        if (roundStarted == false)
        {
            RoundCountdown();
        }
        else
        {
            RoundTimer();
        }
    }

    //The 3 second countdown till the round starts
    void RoundCountdown()
    {
        beforeRoundTime -= Time.deltaTime;

        if (beforeRoundTime >= 0.0f)
        {
            int onesPlace = (int)beforeRoundTime % 10;

            if (onesPlace > 0)
                CountdownDisplay.GetComponent<Image>().sprite = numbers[onesPlace];
            else if (onesPlace == 0)
            {
                Vector2 FightImageSize = new Vector2(FightImage.texture.width, FightImage.texture.height);
                CountdownDisplay.transform.GetComponent<RectTransform>().sizeDelta = FightImageSize;
                CountdownDisplay.GetComponent<Image>().sprite = FightImage;
            }
        }
        else
        {
            //Get the components to enable them once the round starts
            UpdatedPlayerController p1Controller = GameObject.Find("Players/P1").GetComponent<UpdatedPlayerController>();
            UpdatedPlayerCombat p1Combat = GameObject.Find("Players/P1").GetComponent<UpdatedPlayerCombat>();
            PlayerInput p1Input = GameObject.Find("Players/P1").GetComponent<PlayerInput>();
            UpdatedPlayerController p2Controller = GameObject.Find("Players/P2").GetComponent<UpdatedPlayerController>();
            UpdatedPlayerCombat p2Combat = GameObject.Find("Players/P2").GetComponent<UpdatedPlayerCombat>();
            PlayerInput p2Input = GameObject.Find("Players/P2").GetComponent<PlayerInput>();

            roundStarted = true;
            CountdownDisplay.GetComponent<Image>().enabled = false;
            p1Controller.enabled = true;
            p1Combat.enabled = true;
            p1Input.enabled = true;
            p2Controller.enabled = true;
            p2Combat.enabled = true;
            p2Input.enabled = true;
        }
    }
    //The 99 sec match timer
    void RoundTimer()
    {
        targetTime -= Time.deltaTime;

        if (targetTime > 0.0f && stopTime == false)
        {
            int tensPlace = (int)targetTime / 10;
            int onesPlace = (int)targetTime % 10;

            //May want to change later so the image is only set when a second has passed, not every frame
            if (tensPlace >= 0)
                Tens.GetComponent<Image>().sprite = numbers[tensPlace];
            if (onesPlace >= 0)
                Ones.GetComponent<Image>().sprite = numbers[onesPlace];
        }
        else if (targetTime <= 0.0f && stopTime == false)
        {
            Debug.Log("Time exceeded");
            GameObject p1 = GameObject.Find("P1");
            UpdatedHealth p1HealthComp = p1.GetComponent<UpdatedHealth>();
            GameObject p2 = GameObject.Find("P2");
            UpdatedHealth p2HealthComp = p2.GetComponent<UpdatedHealth>();

            if (p1HealthComp.currHealth < p2HealthComp.currHealth)
            {
                p1HealthComp.Knockout();
            }
            else
            {
                p2HealthComp.Knockout();
            }
        }
        else
        {
            afterRoundTime -= Time.deltaTime;

            if (afterRoundTime <= 0.0f)
            {
                if (RoundHandler.p1Wins >= 2 || RoundHandler.p2Wins >= 2)
                {
                    rh.ResetWins();
                    SceneManager.LoadScene("ControlsScene"); //Background Selection
                }
                else
                {
                    SceneManager.LoadScene("FightScene"); //Fight Scene
                }
            }
        }
    }

    public void RoundOver()
    {
        Debug.Log("Match Over");
        stopTime = true;
        GameObject.Find("Banners").GetComponent<BannerDisplay>().DisplayBanners(); //Check which player has more health and give them the victory
    }
}
