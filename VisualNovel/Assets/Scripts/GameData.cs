using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//para try y catch
//Utilizar struct cuando se usan para crear datos efimeros, datos que se usaran durante poco tiempo.
//Para datos que tengan que durar mucho en el tiempo, utilizar class. Basicamente tema de optimización.

[System.Serializable] //Utilizar esto para que se muestre en el inspector
public class PlayerData //Datos permanentes entre partida y partida
{
    public int scene;


    //Declar unn constructor. Se hacen automaticamente, pero de esta forma se ponen unos valores por defecto
    public PlayerData()
    {
        scene = 1;
    }
}
public class GameData //Datos permanentes entre partida y partida
{
    public PlayerData pData;


    //Declar unn constructor. Se hacen automaticamente, pero de esta forma se ponen unos valores por defecto
    public GameData()
    {
        pData = new PlayerData();
    }
}

//string path = Application.persistentDataPath + "/Data";

public static class Data
{
    public static GameData gameData;
    public static string currentSlot;

    public static void Save(string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            //DataManager.SaveToXML<GameData>(data, fileName, path);
            DataManager.SaveToText<GameData>(gameData, fileName, path);
            Debug.Log("[GDM] Save succeed!");
        }
        catch(Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Save error: " + e);
        }
        
    }
    public static void Load(string fileName)
    {
        string path = Application.persistentDataPath + "/Data";

        try//nueva funcion, lo usaremos para comprobar si ha fallado
        {
            //data = (GameData)DataManager.LoadToXML<GameData>(fileName, path);
            gameData = (GameData)DataManager.LoadFromText<GameData>(fileName, path);
            Debug.Log("[GDM] Load succeed!");
        }
        catch (Exception e) //guarda el motivo de fallo en exception
        {
            Debug.Log("[GDM] Load error: " + e);
            NewGame();
        }
    }

    public static void NewGame()
    {
        gameData = new GameData();
    }

    public static void Delete(string fileName)
    {
        string filePath = Application.persistentDataPath + "/Data/" + fileName;

        DataManager.DeleteFile(filePath);
    }

    public static bool DataExists(string fileName)
    {
        string filePath = Application.persistentDataPath + "/Data/" + fileName;

        return DataManager.FileExists(filePath);
    }


    public static void SetScene(int scene)
    {
        gameData.pData.scene = scene;
    }
    public static int GetScene()
    {
        return gameData.pData.scene;
    }
}