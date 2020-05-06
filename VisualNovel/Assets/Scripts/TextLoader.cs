using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour//poner esto en los texts del menu
{
    private Text text;
    public string key;

    public void Initialize()
    {
        text = GetComponent<Text>();

        string value = "";

        if(!LangManager.langData.configDict.TryGetValue(key, out value))
        {
            value = "ERROR";
        }

        text.text = value;
    }
}
