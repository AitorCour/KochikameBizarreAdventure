using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private DialogueParser parser;
    public string dialogue;
    public string nameCharacter;
    public Sprite pose;
    public string position;

    private int lineNum;

    private Text textUI;
    private Text textName;

    /*private Image image_L;
    private Image image_R;*/
    //public GUIStyle customStyle;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = "";
        nameCharacter = "";
        parser = GameObject.FindGameObjectWithTag("Parser").GetComponent<DialogueParser>();
        textUI = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        textName = GameObject.FindGameObjectWithTag("TextName").GetComponent<Text>();
        //image_L = GameObject.FindGameObjectWithTag("ImageLeft").GetComponent<Image>();
        //image_R = GameObject.FindGameObjectWithTag("ImageRight").GetComponent<Image>();
        lineNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ResetImages();//Clear Images

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);

            DisplayImages();
            Read();
            lineNum++;
        }
    }

    /*private void OnGUI()
    {
        dialogue = GUI.TextField(new Rect(100, 400, 600, 200), dialogue, customStyle);
    }*/
    private void ResetImages()
    {
        //image_L.sprite = null;
        //image_R.sprite = null;
        if(nameCharacter != "")
        {
            GameObject character = GameObject.Find(nameCharacter);
            SpriteRenderer currentSprite = character.GetComponent<SpriteRenderer>();//poner esto en el start
            currentSprite.sprite = null;
        }
    }
    private void DisplayImages()
    {
        if(nameCharacter != "")
        {
            GameObject character = GameObject.Find(nameCharacter);

            SetSpritePositions(character);

            SpriteRenderer currentSprite = character.GetComponent<SpriteRenderer>();//poner esto en el start
            currentSprite.sprite = pose;
        }
    }
    /*private void SetImage()
    {
        if(position == "L")
        {
            image_L.sprite = pose;
        }
        if (position == "R")
        {
            image_R.sprite = pose;
        }
    }*/
    void SetSpritePositions(GameObject spriteObject)
    {
        if (position == "L")
        {
            spriteObject.transform.position = new Vector3(-3, 2, 0);
        }
        if (position == "R")
        {
            spriteObject.transform.position = new Vector3(3, 2, 0);
        }
    }
    private void Read()
    {
        textUI.text = dialogue;
        textName.text = nameCharacter;
    }
}
