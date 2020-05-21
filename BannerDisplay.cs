using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerDisplay : MonoBehaviour
{
    public GameObject victory;
    public GameObject defeat;

    private void Start()
    {
        victory.SetActive(false);
        defeat.SetActive(false);
    }

    public void DisplayBanners()
    {
        GameObject p1 = GameObject.Find("P1");
        int p1Health = p1.GetComponent<UpdatedHealth>().currHealth;
        GameObject p2 = GameObject.Find("P2");
        int p2Health = p2.GetComponent<UpdatedHealth>().currHealth;

        //Add ties later
        if (p1Health < p2Health)
        {
            Vector3 tempVictory = victory.transform.localPosition;
            tempVictory.x *= -1;
            victory.transform.localPosition = tempVictory;
            Vector3 tempDefeat = defeat.transform.localPosition;
            tempDefeat.x *= -1;
            defeat.transform.localPosition = tempDefeat;
        }
        else
        {
            //Do nothing
        }
        victory.SetActive(true);
        defeat.SetActive(true);
    }
}
