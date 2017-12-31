using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : PlayerWeapon {

    public override void ActivateWeapon()
    {
        if (!weaponActive)
        {
            StartCoroutine(GunAttack());
        }
    }

    IEnumerator GunAttack()
    {
        Debug.Log("Gun!");
        weaponActive = true;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Done with gun!");
        weaponActive = false;
    }
}
