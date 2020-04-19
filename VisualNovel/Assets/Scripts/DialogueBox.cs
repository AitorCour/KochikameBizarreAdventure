using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    private DialogueParser parser;
    private string dialogue;
    private string nameCharacter;
    private string comment;
    private Sprite pose;
    private string position;

    private bool inScene;
    private bool isDecision;
    private bool commentary;
    private bool commenting;
    private bool nextCommentary;

    private bool outNext;
    private bool canPass;
    private string outPosition;

    public int lineNum;
    public int currentLine;
    public int commentLine;
    private float textTypeSpeed;
    private float textTypeOriginal = 0.05f;

    private Text textUI;
    private Text textName;
    private Text textComment;

    private GameObject buttonPanel;
    private GameObject commentPanel;

    private Image image_L;
    private Image image_R;
    private Image image_RB;
    private Image image_LB;

    private DoTween tween;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = "";
        nameCharacter = "";
        comment = "";
        parser = GameObject.FindGameObjectWithTag("Parser").GetComponent<DialogueParser>();
        textUI = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        textName = GameObject.FindGameObjectWithTag("TextName").GetComponent<Text>();
        textComment = GameObject.FindGameObjectWithTag("TextComment").GetComponent<Text>();
        buttonPanel = GameObject.FindGameObjectWithTag("ButtonPanel");
        commentPanel = GameObject.FindGameObjectWithTag("CommentPanel");
        image_L = GameObject.FindGameObjectWithTag("ImageLeft").GetComponent<Image>();
        image_R = GameObject.FindGameObjectWithTag("ImageRight").GetComponent<Image>();
        image_RB = GameObject.FindGameObjectWithTag("ImageRightB").GetComponent<Image>();
        image_LB = GameObject.FindGameObjectWithTag("ImageLeftB").GetComponent<Image>();
        tween = GetComponent<DoTween>();

        lineNum = -1;
        currentLine = lineNum;
        commentLine = -1;
        textTypeSpeed = textTypeOriginal;

        outNext = false;
        canPass = true;

        commentPanel.SetActive(false);
        buttonPanel.SetActive(false);
        ResetImages();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(textUI.text == dialogue)
        {
            textTypeSpeed = textTypeOriginal;
            canPass = true;
        }

        if (Input.GetMouseButtonDown(0) && !isDecision && !canPass)
        {
            //canPass = true;
            textTypeSpeed = 0.01f;
        }
        if (Input.GetMouseButtonDown(0) && !isDecision && canPass)
        {
            //ResetImages();//Clear Images
            lineNum++;
            canPass = false;

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            inScene = parser.GetInScene(lineNum);
            isDecision = parser.GetDecision(lineNum);
            commentary = parser.GetCommentary(lineNum);

            MoveSprite();
            SetImages();
            Read();
            //lineNum++;

            if(commentary)
            {
                SetComment();
            }
        }
        if (Input.GetMouseButtonDown(1) && !isDecision)
        {
            //ResetImages();//Clear Images
            lineNum--;
            canPass = false;

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            inScene = parser.GetInScene(lineNum);
            isDecision = parser.GetDecision(lineNum);

            MoveSprite();
            SetImages();
            Read();
        }
    }*/
    public void NextDialogue()
    {
        if (textUI.text == dialogue)
        {
            textTypeSpeed = textTypeOriginal;
            canPass = true;
        }

        if (!isDecision && !canPass)
        {
            //canPass = true;
            textTypeSpeed = 0.01f;
        }
        if(!isDecision && canPass && lineNum != currentLine)
        {
            lineNum++;

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            inScene = parser.GetInScene(lineNum);
            isDecision = parser.GetDecision(lineNum);

            MoveSprite();
            SetImages();
            ReadQuick();
        }
        if (!isDecision && canPass && lineNum == currentLine)
        {
            //ResetImages();//Clear Images
            lineNum++;
            currentLine++;
            canPass = false;

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            inScene = parser.GetInScene(lineNum);
            isDecision = parser.GetDecision(lineNum);
            commentary = parser.GetCommentary(lineNum);

            MoveSprite();
            SetImages();
            Read();
            //lineNum++;

            if (commentary && !commenting)
            {
                commentLine++;
                SetComment();
                commenting = true;
            }
        }
    }
    public void PreviousDialogue()
    {
        if (!isDecision)
        {
            //ResetImages();//Clear Images
            lineNum--;
            canPass = false;

            dialogue = parser.GetContent(lineNum);
            nameCharacter = parser.GetName(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
            inScene = parser.GetInScene(lineNum);
            isDecision = parser.GetDecision(lineNum);

            MoveSprite();
            SetImages();
            ReadQuick();
        }
        else return;
    }
    private void ResetImages()
    {
        image_L.sprite = null;
        image_R.sprite = null;
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
        //textUI.text = dialogue;
        textUI.text = string.Empty;

        StartCoroutine(Type());

        textName.text = nameCharacter;
        if(isDecision)//este rollo, o simplemente dejar preparadas las preguntas. Total, se va a otra escena
        {
            buttonPanel.SetActive(true);
        }
    }
    private void ReadQuick()
    {
        canPass = true;
        textTypeSpeed = 0;
        textUI.text = string.Empty;
        textName.text = nameCharacter;
        textUI.text = dialogue;
    }
    private void SetComment()
    {
        Debug.Log("comment");
        comment = parser.GetCommentContent(commentLine);//coge la frfase de la linea 20
        nextCommentary = parser.GetNextComment(commentLine);
        //

        //textComment.text = comment;
        commentPanel.SetActive(true);
        StartCoroutine(TypeComment());
    }
    IEnumerator Type()
    {
        foreach(char letter in dialogue.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(textTypeSpeed);
            if (canPass)
            {
                break;
            }
        }
    }
    IEnumerator TypeComment()
    {
        foreach (char letter in comment.ToCharArray())
        {
            textComment.text += letter;
            yield return new WaitForSeconds(textTypeSpeed);
        }
        yield return new WaitForSeconds(5);
        if(!nextCommentary)
        {
            commentPanel.SetActive(false);
            textComment.text = string.Empty;
            commenting = false;
        }
        else
        {
            textComment.text = string.Empty;
            commentLine++;
            SetComment();
        }
    }
    public void ResetLines()
    {
        lineNum = -1;
        currentLine = lineNum;
    }
}
