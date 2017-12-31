﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox_OLD : MonoBehaviour {
    /*
    public static PlayerHitbox instance;

    public float maxCooldown = 1f;
    public float curCooldown;
    float flashTimer;
    //Up number to increase flashing speed during invulnerability
    float flashSpeed = 6f;
    //whether or not to play flashing animation after taking hit
    bool shield = false;

    BoxCollider2D boxCol;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        boxCol = GetComponent<BoxCollider2D>();
	}

    public void ToggleBoxCol(bool toggle)
    {
        boxCol.enabled = toggle;
    }
	
	// Update is called once per frame
	void Update () {
        if (curCooldown > 0)
        {
            curCooldown -= Time.deltaTime;
            if (!shield)
            {
                if (flashTimer > 0)
                {
                    flashTimer -= Time.deltaTime;
                }
                else
                {
                    flashTimer = maxCooldown / flashSpeed;
                    PlayerMovementBattle.instance.ren.enabled = !PlayerMovementBattle.instance.ren.enabled;
                }
                if (curCooldown <= 0)
                {
                    PlayerMovementBattle.instance.ren.enabled = true;
                }
            }
        }
	}

    public void TakeDamage(float damage, bool shieldActive = false)
    {
        if (curCooldown <= 0)
        {
            if (damage > 0)
            {
                if (shieldActive)
                {
                    BattleUIHandler.instance.DecreaseStamina(damage * 2f);
                    BattleCam.instance.GenericCamShake();
                    //Prevent shield hit particle from firing twice upon breaking
                    if (BattleUIHandler.instance.stamina > 0)
                    {
                        PlayerMovementBattle.instance.shieldHit.Play();
                    }
                    curCooldown = maxCooldown;
                    shield = true;
                }
                else
                {
                    BattleUIHandler.instance.DecreaseHealth(damage);
                    BattleCam.instance.GenericCamShake();
                    curCooldown = maxCooldown;
                    flashTimer = maxCooldown / flashSpeed;
                    PlayerMovementBattle.instance.ren.enabled = false;
                    shield = false;
                    if (BattleUIHandler.instance.health <= 0)
                    {
                        //TODO Add transition to game over state here
                        Destroy(PlayerMovementBattle.instance.gameObject);
                        //Destroy(gameObject);
                    }
                }
            }
        }
    }*/

}