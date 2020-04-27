using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DoTween : MonoBehaviour
{
    private RectTransform image_L;
    private RectTransform image_LB;
    private RectTransform image_R;
    private RectTransform image_RB;

    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        image_L = GameObject.FindGameObjectWithTag("ImageLeft").GetComponent<RectTransform>();
        image_LB = GameObject.FindGameObjectWithTag("ImageLeftB").GetComponent<RectTransform>();
        image_R = GameObject.FindGameObjectWithTag("ImageRight").GetComponent<RectTransform>();
        image_RB = GameObject.FindGameObjectWithTag("ImageRightB").GetComponent<RectTransform>();
        textUI = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveImage_R()
    {
        image_R.DOAnchorPosX(1400, 1, false);
    }
    public void MoveImage_RB()
    {
        image_RB.DOAnchorPosX(1400, 1, false);
    }
    public void MoveImage_L()
    {
        image_L.DOAnchorPosX(-1400, 1, false);
    }
    public void MoveImage_LB()
    {
        image_LB.DOAnchorPosX(-1400, 1, false);
    }
    public void ToScreenImage_R()
    {
        image_R.DOAnchorPosX(685, 1, false);
    }
    public void ToScreenImage_L()
    {
        image_L.DOAnchorPosX(-685, 1, false);
    }
    public void ToScreenImage_RB()
    {
        image_RB.DOAnchorPosX(685, 1, false);
    }
}
