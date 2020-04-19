using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private DialogueBox dialogue;
    private GameManager gameManager;
    public bool mobile;
    private bool next;
    private bool previous;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        dialogue = GetComponent<DialogueBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogue.NextDialogue();
            }
            if (Input.GetMouseButtonDown(1))
            {
                dialogue.PreviousDialogue();
            }
        }
        /*else
        {
            next = nextButton.pressed;
            previous = previousButton.pressed;

            if (next)
            {
                dialogue.NextDialogue();
            }
            if (previous)
            {
                dialogue.PreviousDialogue();
            }
        }*/
    }
    public void OnclickNextButton()
    {
        if (!mobile) return;
        dialogue.NextDialogue();
    }
    public void OnclickPrevButton()
    {
        if (!mobile) return;
        dialogue.PreviousDialogue();
    }
}
