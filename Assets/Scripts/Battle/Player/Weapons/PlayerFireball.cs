using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : PlayerWeapon
{

    //How charged up the fireball is
    int stage = 1, maxStage = 3;
    //Timer and thresholds for determing charge level
    float stage2Timer = 1.0f, stage3Timer = 2.0f, timer = 0;

    public override void ActivateWeapon(bool dashing)
    {
        print("activate fireball");
        if (!dashing)
        {
            if (!weaponActive && stage != maxStage)
            {
                Debug.Log("fireball!");
                weaponActive = true;
                playerImmobilized = true;
                PlayerMovementBattle.instance.StartImmobilization(.2f);
                //Adjust timer starting point based on last charge stage reached
                if (stage == 3)
                {
                    timer = stage3Timer;
                }
                else if (stage == 2)
                {
                    timer = stage2Timer;
                }
                else
                {
                    timer = 0;
                }
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    public override void AbortWeapon()
    {
        if (weaponActive)
        {
            Debug.Log("fireball aborted!");
            weaponActive = false;
            playerImmobilized = false;
        }
    }

    private IEnumerator Attack()
    {
        WeaponIconManager.instance.ActivateWeaponIcon(weaponID, duration);
        //attack
        if (stage == 3)
        {
            Debug.Log("fireball level 3!");
        }
        else if (stage == 2)
        {
            Debug.Log("fireball level 2!");
        }
        else
        {
            Debug.Log("fireball level 1!");
        }
        stage = 1;
        timer = 0;
        yield return new WaitForSeconds(duration);
        Debug.Log("fireball done!");
        playerImmobilized = false;
        weaponActive = false;
    }

    void Update()
    {
        if (weaponActive)
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
