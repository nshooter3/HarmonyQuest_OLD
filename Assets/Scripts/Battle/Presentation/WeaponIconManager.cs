using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIconManager : MonoBehaviour {

    public static WeaponIconManager instance;
    public WeaponIconPool weaponIconPool;

    //The positions of active/backup weapon icons
    public Transform active1, active2, backup1, backup2;
    //References to the weapon icons. Loadout 1 consists of index 0 and index 1, Loadout 2 consists of index 2 and index 3
    private WeaponIcon[] weapons;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	void Start () {
        //Loads weapon icons based on save data
        weapons = new WeaponIcon[4];
        weapons[0] = weaponIconPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon1);
        weapons[1] = weaponIconPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon2);
        weapons[2] = weaponIconPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon3);
        weapons[3] = weaponIconPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon4);
        //Init weapon icon transforms
        SwapLoadouts(true);
    }

    public void SwapLoadouts(bool primaryLoadoutActive)
    {
        if (primaryLoadoutActive)
        {
            weapons[0].transform.position =  active1.transform.position;
            weapons[0].transform.parent = active1;
            weapons[1].transform.position = active2.transform.position;
            weapons[1].transform.parent = active2;
            weapons[2].transform.position = backup1.transform.position;
            weapons[2].transform.parent = backup1;
            weapons[3].transform.position = backup2.transform.position;
            weapons[3].transform.parent = backup2;
            weapons[0].LoadoutSizeLerp();
            weapons[1].LoadoutSizeLerp();
        }
        else
        {
            weapons[0].transform.position = backup1.transform.position;
            weapons[0].transform.parent = backup1;
            weapons[1].transform.position = backup2.transform.position;
            weapons[1].transform.parent = backup2;
            weapons[2].transform.position = active1.transform.position;
            weapons[2].transform.parent = active1;
            weapons[3].transform.position = active2.transform.position;
            weapons[3].transform.parent = active2;
            weapons[2].LoadoutSizeLerp();
            weapons[3].LoadoutSizeLerp();
        }
    }

    public void ActivateWeaponIcon(int index, float duration, bool playFill = true)
    {
        weapons[index].Activate(duration, playFill);
    }

    public void DarkenWeaponIcon(string iconType)
    {
        foreach (WeaponIcon weapon in weapons)
        {
            if (weapon.iconName == iconType)
            {
                weapon.Darken();
            }
        }
    }

    public void UnDarkenWeaponIcon(string iconType)
    {
        foreach (WeaponIcon weapon in weapons)
        {
            if (weapon.iconName == iconType)
            {
                weapon.UnDarken();
            }
        }
    }
}
