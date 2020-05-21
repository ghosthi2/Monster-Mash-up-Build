using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundHandler : MonoBehaviour
{
    public GameObject[] p1Marks = new GameObject[2];
    public GameObject[] p2Marks = new GameObject[2];

    //Number of rounds won for this match for the players
    public static int p1Wins = 0;
    public static int p2Wins = 0;

    [SerializeField] int p1WinsTest = 0; //test
    [SerializeField] int p2WinsTest = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetTallies();
    }

    // Update is called once per frame
    void Update()
    {
        p1WinsTest = p1Wins;
        p2WinsTest = p2Wins;

    }

    public void RoundWinner(string player)
    {
        if (player == "P1")
        {
            p1Wins++;
        }
        else if (player == "P2")
        {
            p2Wins++;
        }
        else
        {
            Debug.LogWarning("Player names are not P1 or P2");
        }

        SetTallies();
    }

    public void ResetWins()
    {
        p1Wins = 0;
        p2Wins = 0;
    }

    void SetTallies()
    {
        for (int i = 0; i < p1Marks.Length; i++)
        {
            if (p1Wins <= i)
            {
                p1Marks[i].SetActive(false);
            }
            else
            {
                p1Marks[i].SetActive(true);
            }
            if (p2Wins <= i)
            {
                p2Marks[i].SetActive(false);
            }
            else
            {
                p2Marks[i].SetActive(true);
            }
        }
    }
}
