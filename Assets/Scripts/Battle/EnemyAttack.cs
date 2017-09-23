using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    //One the hitbox associated with this attack
    public Collider2D hitbox;
    //How much damage the player takes upon being hit
    public int damage;
    //Any side effects that getting hit may inflict upon the player (Slowness? Poison? etc)
    public Action sideEffect;

	// Use this for initialization
	void Start () {
        hitbox = GetComponent<Collider2D>();
    }

    //Should only detect collisions with player
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<PlayerHitbox>())
        {
            //Make player take damage
            col.GetComponent<PlayerHitbox>().TakeDamage(damage);
            //If the attack has additional logic associated with it, run said logic
            if (sideEffect != null)
            {
                sideEffect();
            }
        }
    }
}
