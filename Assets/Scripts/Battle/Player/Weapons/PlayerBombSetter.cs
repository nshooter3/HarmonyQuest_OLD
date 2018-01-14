using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombSetter : PlayerWeapon
{
    public PlayerBomb[] bombs;

    public override void ActivateWeapon()
    {
        if (!weaponActive)
        {
            if (PlayerMovementBattle.instance.bombCount < PlayerMovementBattle.instance.maxBombCount)
            {
                StartCoroutine(BombAttack());
                WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration);
            }
        }
    }

    IEnumerator BombAttack()
    {
        Debug.Log("BombSetter!");
        foreach (PlayerBomb bomb in bombs)
        {
            if (!bomb.active)
            {
                bomb.Init(transform.position);
                break;
            }
        }
        weaponActive = true;
        PlayerMovementBattle.instance.bombCount++;
        yield return new WaitForSeconds(duration);
        Debug.Log("Done with BombSetter!");
        weaponActive = false;
    }
}