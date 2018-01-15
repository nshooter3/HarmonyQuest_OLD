﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxe : PlayerWeapon
{
    //How charged up the axe strike is
    int stage = 1;
    //Whether or not the axe attack is currently charging
    bool axeCharging, attacking = false;
    //Timer and thresholds for determing charge level
    float stage2Timer = 1.0f, stage3Timer = 2.0f, timer = 0;

    public override void ActivateWeapon()
    {
        if (!weaponActive)
        {
            Debug.Log("Axe!");
            weaponActive = true;
            axeCharging = true;
            playerImmobilized = true;
            stage = 1;
            timer = 0;
            attacking = false;
        }
    }

    public override void ReleaseWeapon()
    {
        axeCharging = false;
    }

    public override void AbortWeapon()
    {
        Debug.Log("Axe aborted!");
        weaponActive = false;
        axeCharging = false;
    }

    private IEnumerator Attack()
    {
        WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration);
        //attack
        if (stage == 3)
        {
            Debug.Log("Axe attack level 3!");
        }
        else if (stage == 2)
        {
            Debug.Log("Axe attack level 2!");
        }
        else
        {
            Debug.Log("Axe attack level 1!");
        }
        yield return new WaitForSeconds(duration);
        Debug.Log("Axe done!");
        playerImmobilized = false;
        weaponActive = false;
        attacking = false;
    }

    void Update()
    {
        if (weaponActive && !attacking)
        {
            if (!axeCharging)
            {
                attacking = true;
                StartCoroutine(Attack());
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > stage3Timer)
                {
                    stage = 3;
                }
                else if (timer > stage2Timer)
                {
                    stage = 2;
                }
            }
        }
    }
}
