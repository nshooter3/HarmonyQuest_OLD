using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour {

    public static DataManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    //Saves variables in this script to a playerPref file
    public void Save(int fileNum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/globalVars" + fileNum + ".dat");

        SaveData saveData = PrepareSaveData(GlobalVars.instance.saveData);
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("save successful");
    }

    public void Reset(int fileNum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/globalVars" + fileNum + ".dat");

        SaveData saveData = new SaveData();
        bf.Serialize(file, saveData);
        file.Close();
    }

    //Loads variables in this script from a file
    public void Load(int fileNum)
    {
        if (File.Exists(Application.persistentDataPath + "/globalVars" + fileNum + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/globalVars" + fileNum + ".dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            GlobalVars.instance.saveData = PrepareSaveData(saveData);
            Debug.Log("load successful");
        }
    }

    public static T PrepareSaveData<T>(T saveData)
    {
        return (T) saveData;
    }


}

[Serializable]
public class SaveData
{
    //General variables
    #region
        public int experience = 0;
        public int level = 1;
        public string playerName = "Melody";
        public string curScene = "demo1";
        public string destination = "s1";
        public int deathCount = 0;
        //States: baby, child, adult
        public string age = "baby";
    #endregion

    //Combat Variables
    #region
        //Which weapons are equiped for loadouts one and two
        public int weapon1 = 0;
        public int weapon2 = 3;
        public int weapon3 = 1;
        public int weapon4 = 2;

        //Whether or not a weapon slot has been unlocked. Two are available by default
        public bool[] weaponsAvailable = { true, false, false, true, false, false };

        //Whether or not the player has access to the second weapon loadout. Inactive until they find their first new weapon.
        public bool twoLoadouts = true;
    #endregion

    //TwilitHollow variables
    #region
        public int hollowProgress = 0;
    #endregion

    /*
    //Demo scene variables
    #region
        public bool talkedToBanana = false;
        public bool talkedToBananaBill = false;
        public bool recruitedTranslator = false;
        public bool movedBanana = false;

        public float hallwayProgress = -1f;
        public float potman = 0f;
    #endregion*/
}
