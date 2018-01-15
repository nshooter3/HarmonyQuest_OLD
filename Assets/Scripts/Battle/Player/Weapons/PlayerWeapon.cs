using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public bool weaponActive = false, heldButtonWeapon = false, canAbort = false, playerImmobilized = false;
    public int weaponID;
    public float duration;

    public void CheckForInput(int weaponSlot, bool dashing = false)
    {
        if (weaponSlot == 1)
        {
            if (InputManager.instance.shoot1Press)
            {
                ActivateWeapon(dashing);
            }
            else if (InputManager.instance.shoot1Held)
            {
                ActivateWeaponHeld(dashing);
            }
            else if (InputManager.instance.shoot1Release)
            {
                ReleaseWeapon();
            }
        }
        else if (weaponSlot == 2)
        {
            if (InputManager.instance.shoot2Press)
            {
                ActivateWeapon(dashing);
            }
            else if (InputManager.instance.shoot2Held)
            {
                ActivateWeaponHeld(dashing);
            }
            else if (InputManager.instance.shoot2Release)
            {
                ReleaseWeapon();
            }
        }
        else
        {
            Debug.LogError("invalid weaponSlot index");
        }
    }

    //Attack button pressed/held
    public virtual void ActivateWeapon(bool dashing) { }

    //Attack button pressed/held
    public virtual void ActivateWeaponHeld(bool dashing) { }

    //Attack button released
    public virtual void ReleaseWeapon() { }

    //Cancel out of weapon behavior
    public virtual void AbortWeapon() { }
}
