using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundMenu : MonoBehaviour
{
    public bool isChangeBackground;
    public bool isStartGame;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public TextMesh changeText;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            //Debug.Log("Valid SpriteRenderer discovered");
            if (ChoiceHolder.BackgroundSelection == 1)
            {
                ChangeSprite();
            }
        }
    }

    void OnMouseUp()
    {
        if (isChangeBackground)
        {
            changeText = GetComponent<TextMesh>();
            if (changeText == null)
            {
                Debug.Log("Text component not discovered");
            }

            ChoiceHolder.BackgroundSelection++;
            ChoiceHolder.BackgroundSelection %= ChoiceHolder.NumBackgrounds;

            //ChangeSprite();

            switch (ChoiceHolder.BackgroundSelection)
            {
                case 0:
                    GetComponent<Renderer>().material.color = Color.blue;
                    changeText.text = "Change\nBackground\nCurrent: Water";
                    break;
                case 1:
                    GetComponent<Renderer>().material.color = Color.red;
                    changeText.text = "Change\nBackground\nCurrent: Lava";
                    break;
                default:
                    GetComponent<Renderer>().material.color = Color.cyan;
                    changeText.text = "Undefined BackgroundSelection";
                    Debug.Log("Undefined BackgroundSelection");
                    break;
            }
        }
        else if (isStartGame)
        {
            GetComponent<Renderer>().material.color = Color.green;
            SceneManager.LoadScene("CharacterCreation2"); //CharacterCreation2
        }
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
