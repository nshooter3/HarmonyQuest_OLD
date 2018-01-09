﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : PlayerWeapon {

    public override void ActivateWeapon()
    {
        if (!weaponActive)
        {
            StartCoroutine(SwordAttack());
            WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration);
        }
    }

    IEnumerator SwordAttack()
    {
        Debug.Log("Sword!");
        weaponActive = true;
        yield return new WaitForSeconds(duration);
        Debug.Log("Done with sword!");
        weaponActive = false;
    }
}
