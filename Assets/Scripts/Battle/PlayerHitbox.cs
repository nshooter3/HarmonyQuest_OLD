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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (curCooldown <= 0)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && !col.gameObject.GetComponent<Bullet>().friendly)
            {
                BattleUIHandler.instance.DecreaseHealth(col.gameObject.GetComponent<Bullet>().damage);
            }
            curCooldown = maxCooldown;
        }
    }

}
