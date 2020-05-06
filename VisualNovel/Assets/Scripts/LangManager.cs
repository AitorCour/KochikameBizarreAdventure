using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class LangData
{
    public enum Languages { Spanish, English };
    public Languages currentLanguage;

    [XmlIgnore]//Esta avriable no se guardará
    public Dictionary<string, string> configDict; //diccionario string string

    public LangData()
    {
        //Detectar el idioma del equipo y ponerlo automático
        SystemLanguage systemLang = Application.systemLanguage;
        Debug.Log("sending");
        if (systemLang == SystemLanguage.Spanish) currentLanguage = Languages.Spanish;
        else currentLanguage = Languages.English;

        //Debug.Log(systemLang + "   " + currentLanguage);
        configDict = new Dictionary<string, string>();
    }
}

public static class LangManager
{
    public static LangData langData;
    //langData es accesible desde cualquier lugar, Siempre está en memoria
    //Nunca declarar otra variable del tipo LangData
    public static void LoadLanguage()
    {
        //langData = new LangData();

        try
        {
            string path = Application.persistentDataPath + "/Data";
            langData = (LangData)DataManager.LoadFromText<LangData>("Lang", path);
        }
        catch
        {
            langData = new LangData();
            SaveLanguage();
        }

        LoadConfigText();
        Debug.Log(langData.currentLanguage.ToString());
    }
    public static string SelectedLanguage()
    {
        string current = langData.currentLanguage.ToString();
        return current; 
    }
    public static void SaveLanguage()
    {
        try
        {
            string path = Application.persistentDataPath + "/Data";
            DataManager.SaveToText<LangData>(langData, "Lang", path);
        }
        catch(Exception e)
        {
            Debug.Log("Save lang error" + e);
        }
    }
    public static void LoadConfigText()
    {
        try
        {
            langData.configDict = new Dictionary<string, string>();

            string fullText = LoadTextFromFile("TextData/configText");//Busca el archivo
            string[] lines = ReadLinesFromString(fullText); //Lee las lineas
            for (int  i = 1; i < lines.Length; i++) //en lugar de empezar en 0 empieza en 1
            {
                string[] cols = lines[i].Split('\t');//parte la lineas por cada tabulador

                switch (langData.currentLanguage)
                {
                    case LangData.Languages.Spanish:
                        langData.configDict.Add(cols[0], cols[1]);
                        break;
                    case LangData.Languages.English:
                        langData.configDict.Add(cols[0], cols[2]);
                        break;
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("LoadConfigText ERROR: " + e);
        }
    }
    //DataManager
    public static string LoadTextFromFile(string pathfile)
    {
        TextAsset asset = Resources.Load<TextAsset>(pathfile);
        return asset.text;
    }
    public static string[] ReadLinesFromString(string text)
    {
        StringReader reader = new StringReader(text);

        List<string> lines = new List<string>();
        while(true)
        {
            string oneLine = reader.ReadLine();
            if (oneLine != null) lines.Add(oneLine); //añade a la lista de lineas
            else break;
        }
        return lines.ToArray();
    }
}
