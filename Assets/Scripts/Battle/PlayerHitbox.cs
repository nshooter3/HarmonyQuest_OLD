using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {

    public float maxCooldown = 1f;
    public float curCooldown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (curCooldown > 0)
        {
            curCooldown -= Time.deltaTime;
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (curCooldown <= 0)
        {
            Debug.Log("hit");
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
                if (BattleUIHandler.instance.health <= 0)
                {
                    //TODO Add transition to game over state here
                    Destroy(gameObject);
                }
            }
        }
    }

}
