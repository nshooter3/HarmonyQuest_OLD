using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {

    public float maxCooldown = 1f;
    public float curCooldown;
    float flashTimer;
    //Up number to increase flashing speed during invulnerability
    float flashSpeed = 6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (curCooldown > 0)
        {
            curCooldown -= Time.deltaTime;
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

    void OnTriggerStay2D(Collider2D col)
    {
        if (curCooldown <= 0)
        {
            //Check for both bullet and enemy collisions
            float damage = 0;
            if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && !col.gameObject.GetComponent<Bullet>().friendly)
            {
                Bullet temp = col.gameObject.GetComponent<Bullet>();
                damage = temp.damage;
                temp.Reset();
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                damage = col.gameObject.GetComponent<Enemy>().damage;
            }

            if (damage > 0)
            {
                BattleUIHandler.instance.DecreaseHealth(damage);
                BattleCam.instance.CamShake();
                curCooldown = maxCooldown;
                flashTimer = maxCooldown / flashSpeed;
                PlayerMovementBattle.instance.ren.enabled = false;
                if (BattleUIHandler.instance.health <= 0)
                {
                    //TODO Add transition to game over state here
                    Destroy(gameObject);
                }
            }
        }
    }

}
