using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponPool : MonoBehaviour {

    //A list of all weapons.
    //0. Sword
    //1. Lance
    //2. Axe
    //3. Gun
    //4. Fireball
    //5. Bomb
    public PlayerWeapon[] weapons;

    //Returns a new instance of a weapon icon based on index
    public PlayerWeapon GetWeaponFromIndex(int index)
    {
        return Instantiate(weapons[index]);
    }
}
