using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour {

    //Saves variables in this script to a playerPref file
    public void Save(int fileNum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/globalVars" + fileNum + ".dat");

        SaveData saveData = PrepareSaveData();
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Reset(int fileNum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/globalVars" + fileNum + ".dat");

        SaveData saveData = new SaveData();
        bf.Serialize(file, saveData);
        file.Close();
    }

    //Loads variables in this script from a playerPref file
    public void Load(int fileNum)
    {
        if (File.Exists(Application.persistentDataPath + "/globalVars" + fileNum + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/globalVars" + fileNum + ".dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            UnpackSaveData(saveData);
        }
    }

    SaveData PrepareSaveData()
    {
        SaveData saveData = new SaveData();
        //General variables
        #region
        saveData.experience = GlobalVars.GV.saveData.experience;
        saveData.level = GlobalVars.GV.saveData.level;
        saveData.playerName = GlobalVars.GV.saveData.playerName;
        saveData.curScene = GlobalVars.GV.saveData.curScene;
        saveData.deathCount = GlobalVars.GV.saveData.deathCount;
        saveData.destination = GlobalVars.GV.saveData.destination;
        #endregion

        //Demo scene variables
        #region
        saveData.talkedToBanana = GlobalVars.GV.saveData.talkedToBanana;
        saveData.recruitedTranslator = GlobalVars.GV.saveData.recruitedTranslator;
        saveData.movedBanana = GlobalVars.GV.saveData.movedBanana;
        saveData.talkedToBananaBill = GlobalVars.GV.saveData.talkedToBananaBill;

        saveData.hallwayProgress = GlobalVars.GV.saveData.hallwayProgress;
        saveData.potman = GlobalVars.GV.saveData.potman;
        #endregion
        return saveData;
    }

    void UnpackSaveData(SaveData saveData)
    {
        #region
        GlobalVars.GV.saveData.experience = saveData.experience;
        GlobalVars.GV.saveData.level = saveData.level;
        GlobalVars.GV.saveData.playerName = saveData.playerName;
        GlobalVars.GV.saveData.curScene = saveData.curScene;
        GlobalVars.GV.saveData.deathCount = saveData.deathCount;
        GlobalVars.GV.saveData.destination = saveData.destination;
        #endregion

        //Demo scene variables
        #region
        GlobalVars.GV.saveData.talkedToBanana = saveData.talkedToBanana;
        GlobalVars.GV.saveData.recruitedTranslator = saveData.recruitedTranslator;
        GlobalVars.GV.saveData.movedBanana = saveData.movedBanana;
        GlobalVars.GV.saveData.talkedToBananaBill = saveData.talkedToBananaBill;

        GlobalVars.GV.saveData.hallwayProgress = saveData.hallwayProgress;
        GlobalVars.GV.saveData.potman = saveData.potman;
        #endregion
    }


}

[Serializable]
public class SaveData
{
    //General variables
    #region
    public int experience = 0;
    public int level = 1;
    public string playerName = "Bill";
    public string curScene = "demo1";
    public string destination = "s1";
    public int deathCount = 0;
    #endregion

    //Demo scene variables
    #region
    public bool talkedToBanana = false;
    public bool talkedToBananaBill = false;
    public bool recruitedTranslator = false;
    public bool movedBanana = false;

    public float hallwayProgress = -1f;
    public float potman = 0f;
    #endregion
}
