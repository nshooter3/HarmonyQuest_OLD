using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombSetter : PlayerWeapon
{
    public PlayerBomb[] bombs;

    public override void ActivateWeapon(bool dashing)
    {
        if (!dashing)
        {
            if (!weaponActive)
            {
                if (PlayerMovementBattle.instance.bombCount < PlayerMovementBattle.instance.maxBombCount)
                {
                    StartCoroutine(BombAttack());
                    WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration, false);
                }
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

    void Update()
    {
        if (PlayerMovementBattle.instance.bombCount == PlayerMovementBattle.instance.maxBombCount)
        {
            WeaponIconManager.instance.DarkenWeaponIcon(weaponID);
        }
        else
        {
            WeaponIconManager.instance.UnDarkenWeaponIcon(weaponID);
        }
    }
}