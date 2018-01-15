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
            if (heldButtonWeapon)
            {
                if (InputManager.instance.shoot1Held)
                {
                    ActivateWeapon(dashing);
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
                    ActivateWeapon(dashing);
                }
            }
        }
        else if (weaponSlot == 2)
        {
            if (heldButtonWeapon)
            {
                if (InputManager.instance.shoot2Held)
                {
                    ActivateWeapon(dashing);
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
                    ActivateWeapon(dashing);
                }
            }
        }
        else
        {
            Debug.LogError("invalid weaponSlot index");
        }
    }

    //Attack button pressed/held
    public virtual void ActivateWeapon(bool dashing) { }

    //Attack button released
    public virtual void ReleaseWeapon() { }

    //Cancel out of weapon behavior
    public virtual void AbortWeapon() { }
}
