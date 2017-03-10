using UnityEngine;
using System.Collections;

public class GlobalVars : MonoBehaviour {

    public static GlobalVars GV;
    public SaveData saveData;

    void Awake()
    {
        if (GV == null)
        {
            Screen.SetResolution(800, 600, true);
            DontDestroyOnLoad(gameObject);
            GV = this;

            //TODO REMOVE THIS ONCE SAVING AND LOADING IS IN
            GV.saveData = new SaveData();
        }
        else if (GV != this)
        {
            Destroy(gameObject);
        }

    }

    public void GetUpdatedDialoguerVars()
    {
        //Demo scene variables
        #region
        saveData.talkedToBanana = Dialoguer.GetGlobalBoolean(0);
        saveData.recruitedTranslator = Dialoguer.GetGlobalBoolean(1);
        saveData.movedBanana = Dialoguer.GetGlobalBoolean(2);
        saveData.talkedToBananaBill = Dialoguer.GetGlobalBoolean(3);

        saveData.hallwayProgress = Dialoguer.GetGlobalFloat(0);
        saveData.potman = Dialoguer.GetGlobalFloat(1);
        #endregion
    }

    public void SetDialoguerVars()
    {
        //Demo scene variables
        #region
        Dialoguer.SetGlobalBoolean(0, saveData.talkedToBanana);
        Dialoguer.SetGlobalBoolean(1, saveData.recruitedTranslator);
        Dialoguer.SetGlobalBoolean(2, saveData.movedBanana);
        Dialoguer.SetGlobalBoolean(3, saveData.talkedToBananaBill);

        Dialoguer.SetGlobalFloat(0, saveData.hallwayProgress);
        Dialoguer.SetGlobalFloat(1, saveData.potman);
        #endregion
    }
}
