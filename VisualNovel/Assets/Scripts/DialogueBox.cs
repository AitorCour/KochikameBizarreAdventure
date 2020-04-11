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
    public bool inScene;

    private bool outNext;
    private string outPosition;

    private int lineNum;

    private Text textUI;
    private Text textName;

    private Image image_L;
    private Image image_R;
    private Image image_RB;

    private DoTween tween;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = "";
        nameCharacter = "";
        parser = GameObject.FindGameObjectWithTag("Parser").GetComponent<DialogueParser>();
        textUI = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        textName = GameObject.FindGameObjectWithTag("TextName").GetComponent<Text>();
        image_L = GameObject.FindGameObjectWithTag("ImageLeft").GetComponent<Image>();
        image_R = GameObject.FindGameObjectWithTag("ImageRight").GetComponent<Image>();
        image_RB = GameObject.FindGameObjectWithTag("ImageRightB").GetComponent<Image>();
        tween = GetComponent<DoTween>();

        lineNum = 0;
        outNext = false;

        ResetImages();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //ResetImages();//Clear Images

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            inScene = parser.GetInScene(lineNum);

            MoveSprite();
            SetImages();
            Read();
            lineNum++;
        }
    }
    private void ResetImages()
    {
        image_L.sprite = null;
        //image_R.sprite = null;
        //image_R.gameObject.SetActive(false);
        tween.MoveImage_R();
        tween.MoveImage_L();
        tween.MoveImage_RB();
        tween.MoveImage_LB();
    }
    private void MoveSprite()
    {
        if(!inScene)
        {
            OutScene();
        }
        if(outNext && inScene)//si el bool temp, esta positivo, y ya hay una imagen nueva, entoces out
        {
            if (outPosition == "L")
            {
                tween.MoveImage_L();
            }
            if (outPosition == "R")
            {
                tween.MoveImage_R();
                Debug.Log("Moving");
            }
            outNext = false;
        }
    }
    private void OutScene()
    {
        if(!outNext)
        {
            outNext = true;
            outPosition = position;
            return;
        }
    }
    private void SetImages()
    {
        //if (!inScene) return;
        if(position == "L") 
        {
            tween.ToScreenImage_L();
            tween.MoveImage_LB();
            image_L.sprite = pose;
        }
        if (position == "R")
        {
            tween.ToScreenImage_R();
            tween.MoveImage_RB();
            image_R.sprite = pose;
        }
        if (position == "LB")
        {
            tween.ToScreenImage_R();
            tween.MoveImage_L();
            image_R.sprite = pose;
        }
        if (position == "RB")
        {
            tween.ToScreenImage_RB();
            tween.MoveImage_R();
            image_RB.sprite = pose;
        }

    }
    private void Read()
    {
        textUI.text = dialogue;
        textName.text = nameCharacter;
    }
}
