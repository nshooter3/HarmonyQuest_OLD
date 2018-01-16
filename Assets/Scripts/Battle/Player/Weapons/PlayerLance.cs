using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLance : PlayerWeapon
{
    public override void ActivateWeapon(bool dashing)
    {
        if (!dashing)
        {
            if (!weaponActive)
            {
                StartCoroutine(LanceAttack());
                WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration);
            }
        }
    }

    IEnumerator LanceAttack()
    {
        Debug.Log("Lance!");
        weaponActive = true;
        playerImmobilized = true;
        PlayerMovementBattle.instance.StartImmobilization(.15f);
        yield return new WaitForSeconds(duration);
        Debug.Log("Done with Lance!");
        weaponActive = false;
        playerImmobilized = false;
    }
}
