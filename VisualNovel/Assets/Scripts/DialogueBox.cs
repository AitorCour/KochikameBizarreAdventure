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

    private Image image_L;
    private Image image_R;

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

            SetImages();
            Read();
            lineNum++;
        }
    }
    private void ResetImages()
    {
        image_L.sprite = null;
        image_R.sprite = null;
        image_R.gameObject.SetActive(false);
    }
    private void SetImages()
    {
        if(position == "L")
        {
            image_L.sprite = pose;
        }
        if (position == "R")
        {
            image_R.sprite = pose;
            image_R.gameObject.SetActive(true);
        }
    }
    private void Read()
    {
        textUI.text = dialogue;
        textName.text = nameCharacter;
    }
}
