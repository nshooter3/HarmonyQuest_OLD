using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {

    public static PlayerHitbox instance;

    public float maxCooldown = 1f;
    public float curCooldown;
    float flashTimer;
    //Up number to increase flashing speed during invulnerability
    float flashSpeed = 6f;
    //whether or not to play flashing animation after taking hit
    bool shield = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public BoxCollider2D boxCol;

	// Use this for initialization
	void Start () {
        boxCol = GetComponent<BoxCollider2D>();
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

    void OnTriggerStay2D(Collider2D col)
    {
        if (curCooldown <= 0)
        {
            //Check for both bullet and enemy collisions
            if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && !col.gameObject.GetComponent<Bullet>().friendly)
            {
                Bullet temp = col.gameObject.GetComponent<Bullet>();
                TakeDamage(temp.damage);
                temp.Reset();
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                TakeDamage(col.gameObject.GetComponent<Enemy>().damage);
            }
        }
    }

    void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            if (PlayerShield.instance.active)
            {
                BattleUIHandler.instance.DecreaseStamina(damage * 2f);
                BattleCam.instance.CamShake();
                curCooldown = maxCooldown;
                shield = true;
            }
            else
            {
                BattleUIHandler.instance.DecreaseHealth(damage);
                BattleCam.instance.CamShake();
                curCooldown = maxCooldown;
                flashTimer = maxCooldown / flashSpeed;
                PlayerMovementBattle.instance.ren.enabled = false;
                shield = false;
                if (BattleUIHandler.instance.health <= 0)
                {
                    //TODO Add transition to game over state here
                    Destroy(gameObject);
                }
            }
        }
    }

}
