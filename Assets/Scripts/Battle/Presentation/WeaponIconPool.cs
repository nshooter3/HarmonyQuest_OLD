using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIconPool : MonoBehaviour {

    //A list of all weapon icons.
    //0. Sword
    //1. Lance
    //2. Axe
    //3. Gun
    //4. Fireball
    //5. Bomb
    public WeaponIcon[] WeaponIcons;

    //Returns a new instance of a weapon icon based on index
    public WeaponIcon GetWeaponFromIndex(int index)
    {
        return Instantiate(WeaponIcons[index]);
    }
}
