using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public bool weaponActive = false, heldButtonWeapon = false, canAbort = false, playerImmobilized = false;
    public int weaponID;
    public float duration;

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
                else if (InputManager.instance.shoot1Release)
                {
                    ReleaseWeapon();
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
                else if (InputManager.instance.shoot2Release)
                {
                    ReleaseWeapon();
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

    //Attack button pressed/held
    public virtual void ActivateWeapon() { }

    //Attack button released
    public virtual void ReleaseWeapon() { }

    //Cancel out of weapon behavior
    public virtual void AbortWeapon() { }
}
