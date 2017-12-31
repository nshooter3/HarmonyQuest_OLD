using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : PlayerWeapon {

    public override void ActivateWeapon()
    {
        if (!weaponActive)
        {
            StartCoroutine(SwordAttack());
        }
    }

    IEnumerator SwordAttack()
    {
        Debug.Log("Sword!");
        weaponActive = true;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Done with sword!");
        weaponActive = false;
    }
}
