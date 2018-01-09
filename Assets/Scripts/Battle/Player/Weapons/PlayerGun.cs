using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : PlayerWeapon {

    public override void ActivateWeapon()
    {
        if (!weaponActive)
        {
            StartCoroutine(GunAttack());
            WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration, false);
        }
    }

    IEnumerator GunAttack()
    {
        Debug.Log("Gun!");
        weaponActive = true;
        yield return new WaitForSeconds(duration);
        Debug.Log("Done with gun!");
        weaponActive = false;
    }
}
