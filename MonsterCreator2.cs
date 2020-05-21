using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterCreator2 : MonoBehaviour
{
    public GameObject P1Grave;
    public GameObject P2Grave;
    Animator p1GraveAnim;
    Animator p2GraveAnim;

    public GameObject[] P1Arrows = new GameObject[3];
    public GameObject[] P2Arrows = new GameObject[3];

    public int[] p1Choice;
    public int[] p2Choice;

    public Sprite[] heads;
    public Sprite[] arms;
    public Sprite[] legs;

    public GameObject[] p1Body = new GameObject[3];
    public GameObject[] p2Body = new GameObject[3];

    int currP1Option;
    int currP2Option;

    bool gameStarting = false;
    public UIPlayer uiPlayer;
    public Canvas canvas;
    public TextMeshProUGUI text;

    private AsyncOperation operation;

    // Start is called before the first frame update
    void Start()
    {
        currP1Option = 0;
        currP2Option = 0;

        p1GraveAnim = P1Grave.GetComponent<Animator>();
        p2GraveAnim = P2Grave.GetComponent<Animator>();

        //Set every other button to inactive at the start
        for (int i = 1; i < P1Arrows.Length; i++)
        {
            P1Arrows[i].SetActive(false);
            P2Arrows[i].SetActive(false);
        }
        //canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarting == true)
        {
            StartCoroutine(WaitABit(3));
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < p1Choice.Length; i++)
                {
                    ChoiceHolder.P1Choice[i] = p1Choice[i];
                    ChoiceHolder.P2Choice[i] = p2Choice[i];
                }
                uiPlayer.Select();
                Debug.Log("Ready to go into Grave Anims"); //test
                PlayGraveAnims();
            }

            if (Input.GetKeyDown(KeyCode.S) && currP1Option < P1Arrows.Length - 1)
            {
                currP1Option++;
                uiPlayer.Right();
            }
            else if (Input.GetKeyDown(KeyCode.S) && currP1Option >= P1Arrows.Length - 1)
            {
                uiPlayer.RightDud();
            }
            else if (Input.GetKeyDown(KeyCode.W) && currP1Option > 0)
            {
                currP1Option--;
                uiPlayer.Right();
            }
            else if (Input.GetKeyDown(KeyCode.W) && currP1Option <= 0)
            {
                uiPlayer.RightDud();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && currP2Option < P2Arrows.Length - 1)
            {
                currP2Option++;
                uiPlayer.Right(1.2f);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currP2Option >= P2Arrows.Length - 1)
            {
                uiPlayer.RightDud(1.2f);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && currP2Option > 0)
            {
                currP2Option--;
                uiPlayer.Right(1.2f);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && currP2Option <= 0)
            {
                uiPlayer.RightDud(1.2f);
            }

            UpdateOption(currP1Option, currP2Option);

            //choice @ currP1Option
            if (Input.GetKeyDown(KeyCode.D) && p1Choice[currP1Option] < heads.Length - 1)
            {
                p1Choice[currP1Option]++;
                uiPlayer.Left();

            }
            else if (Input.GetKeyDown(KeyCode.D) && p1Choice[currP1Option] >= heads.Length - 1)
            {
                uiPlayer.LeftDud();

            }
            else if (Input.GetKeyDown(KeyCode.A) && p1Choice[currP1Option] > 0)
            {
                p1Choice[currP1Option]--;
                uiPlayer.Left();
            }
            else if (Input.GetKeyDown(KeyCode.A) && p1Choice[currP1Option] <= 0)
            {
                uiPlayer.LeftDud();
            }

            //Choice @ currP2Option
            if (Input.GetKeyDown(KeyCode.RightArrow) && p2Choice[currP2Option] < heads.Length - 1)
            {
                p2Choice[currP2Option]++;
                uiPlayer.Left(1.2f);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && p2Choice[currP2Option] >= heads.Length - 1)
            {
                uiPlayer.LeftDud(1.2f);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && p2Choice[currP2Option] > 0)
            {
                p2Choice[currP2Option]--;
                uiPlayer.Left(1.2f);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && p2Choice[currP2Option] <= 0)
            {
                uiPlayer.LeftDud(1.2f);
            }

            UpdateSprite(p1Choice, p2Choice);
        }
    }


    void UpdateOption(int p1Option, int p2Option)
    {

        for (int i = 0; i < P1Arrows.Length; i++)
        {
            if (P1Arrows[i] == P1Arrows[p1Option])
            {
                P1Arrows[i].SetActive(true);
            }
            else
            {
                P1Arrows[i].SetActive(false);
            }

            if (P2Arrows[i] == P2Arrows[p2Option])
            {
                P2Arrows[i].SetActive(true);
            }
            else
            {
                P2Arrows[i].SetActive(false);
            }

        }
    }

    void UpdateSprite(int[] p1Pick, int[] p2Pick)
    {
        p1Body[0].GetComponent<Image>().sprite = heads[p1Pick[0]];
        p1Body[1].GetComponent<Image>().sprite = arms[p1Pick[1]];
        p1Body[2].GetComponent<Image>().sprite = legs[p1Pick[2]];

        p2Body[0].GetComponent<Image>().sprite = heads[p2Pick[0]];
        p2Body[1].GetComponent<Image>().sprite = arms[p2Pick[1]];
        p2Body[2].GetComponent<Image>().sprite = legs[p2Pick[2]];
    }

    void PlayGraveAnims()
    {
        gameStarting = true;

        if (p1Choice[1] == 0)
        {
           p1GraveAnim.SetTrigger("Dummy Hand");
        }
        else if (p1Choice[1] == 1)
        {
            p1GraveAnim.SetTrigger("Mummy Hand");
        }
        else if (p1Choice[1] == 2)
        {
            p1GraveAnim.SetTrigger("Werewolf Hand");
        }
        else if (p1Choice[1] == 3)
        {
            p1GraveAnim.SetTrigger("Vampire Hand");
        }

        if (p2Choice[1] == 0)
        {
            p2GraveAnim.SetTrigger("Dummy Hand");
        }
        else if (p2Choice[1] == 1)
        {
            p2GraveAnim.SetTrigger("Mummy Hand");
        }
        else if (p2Choice[1] == 2)
        {
            p2GraveAnim.SetTrigger("Werewolf Hand");
        }
        else if (p2Choice[1] == 3)
        {
            p2GraveAnim.SetTrigger("Vampire Hand");
        }
    }

    /*
    Debug.Log("Ready to go into LoadNext");
            yield return null;
            canvas.gameObject.SetActive(true);
            StartCoroutine("LoadNext");
    */

    private IEnumerator LoadNext()
    {
        Debug.Log("into LoadNext");
        operation = SceneManager.LoadSceneAsync("FightScene");
        while(!operation.isDone)
        {
            text.text = $"Loading: {(int)(operation.progress * 100f)} %";
            yield return null;
        }
        text.text = $"Loading: {(int)(operation.progress * 100f)} %";
        operation = null;
        canvas.gameObject.SetActive(false);
    }

    private IEnumerator WaitABit(int waiting)
    {
        yield return new WaitForSeconds(waiting);
        //SceneManager.LoadScene("FightScene");
        SceneManager.LoadScene("FightScene");
    }
}

