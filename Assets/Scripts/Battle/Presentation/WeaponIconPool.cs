using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIconPool : MonoBehaviour {

    //A list of all weapon icons.
    //1. Sword
    //2. Lance
    //3. Axe
    //4. Gun
    //5. Fireball
    //6. Bomb
    public WeaponIcon[] WeaponIcons;

    //Returns a new instance of a weapon icon based on index
    public WeaponIcon GetWeaponFromIndex(int index)
    {
        return Instantiate(WeaponIcons[index]);
    }
}
