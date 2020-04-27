using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool menu = true;
    private string mySlot;
    public GameObject sure = null;

    void Start()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        if(!menu)
        {
            //SaveData("1");
            Data.SetScene(scene);
            StartCoroutine(WaitAutoSave(scene));
        }
    }
    public void ChangeScene(int nextScene)
    {
        SceneManager.LoadScene(nextScene);
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
