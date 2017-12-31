using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public bool weaponActive = false, heldButtonWeapon = false;

    public void CheckForInput(int weaponSlot)
    {
        if (weaponSlot == 1)
        {
            if (heldButtonWeapon)
            {
                if (InputManager.instance.shoot1Held)
                {
                    ActivateWeapon();
                }
            }
            else
            {
                if (InputManager.instance.shoot1Press)
                {
                    ActivateWeapon();
                }
            }
        }
        else if (weaponSlot == 2)
        {
            if (heldButtonWeapon)
            {
                if (InputManager.instance.shoot2Held)
                {
                    ActivateWeapon();
                }
            }
            else
            {
                if (InputManager.instance.shoot2Press)
                {
                    ActivateWeapon();
                }
            }
        }
        else
        {
            Debug.LogError("invalid weaponSlot index");
        }
    }

    public virtual void ActivateWeapon() { }
}
