using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : PlayerWeapon {

    bool reloaded = true;

    public override void ActivateWeaponHeld(bool dashing)
    {
        if (!dashing)
        {
            if (reloaded)
            {
                StartCoroutine(GunAttack());
                if (!playerImmobilized)
                {
                    PlayerMovementBattle.instance.StartImmobilization(.15f);
                }
                playerImmobilized = true;
                weaponActive = true;
                reloaded = false;
                WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration, false);
            }
        }
    }

    public override void ReleaseWeapon()
    {
        playerImmobilized = false;
        weaponActive = false;
    }

    IEnumerator GunAttack()
    {
        Debug.Log("Gun!");
        yield return new WaitForSeconds(duration);
        reloaded = true;
        Debug.Log("Done with gun!");
    }
}
