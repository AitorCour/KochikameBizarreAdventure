using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool menu = true;
    private string mySlot;
    public GameObject sure = null;
    private TextLoader[] texts;
    private DialogueParser dialogueParser;

    void Start()
    {
        LangManager.LoadLanguage();
        
        texts = GetComponentsInChildren<TextLoader>();
        int scene = SceneManager.GetActiveScene().buildIndex;
        //SetLanguage(1);
        if (!menu)
        {
            //SaveData("1");
            dialogueParser = GameObject.FindObjectOfType<DialogueParser>();
            Data.SetScene(scene);
            StartCoroutine(WaitAutoSave(scene));
            string language = LangManager.SelectedLanguage();
            dialogueParser.Initialize(language);
        }
    }
    public void ChangeScene(int nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
    public void SetLanguage(int id)
    {
        //coje el current language 
        if (id < 0 || id > 1) id = 0;

        LangManager.langData.currentLanguage = (LangData.Languages)id;//convierte entero a enumerable
        LangManager.LoadConfigText();//Se vuelve a cargar el texto para cambiarlo

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Initialize();//Se inicializan los texts loaders
        }

        LangManager.SaveLanguage();
    }
    public void CheckifDataExist(string slotName)
    {
        if (Data.DataExists(slotName))
        {
            //Preguntar para borrar
            Debug.Log("Data already exist");
            mySlot = slotName;
            sure.SetActive(true);
        }
    }
    public void NewGame()
    {
        Data.NewGame();
        Data.currentSlot = mySlot;

        ChangeScene(1);
    }
    public void LoadData(string slot)
    {
        Data.Load(slot);
        Data.GetScene();
        ChangeScene(Data.gameData.pData.scene);
    }
    public void SaveData(string slot)
    {
        Data.Save(slot);
    }
    IEnumerator WaitAutoSave(int scene)
    {
        yield return new WaitForSeconds(3);
        //Data.SetScene(scene);
        if(Data.currentSlot == "Slot_1")
        {
            SaveData("Slot_1");
        }
        if (Data.currentSlot == "Slot_2")
        {
            SaveData("Slot_2");
        }
        if (Data.currentSlot == "Slot_3")
        {
            SaveData("Slot_3");
        }
    }
}
