﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundEnemyAndEnemyBullets : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            col.gameObject.GetComponent<Bullet>().Reset();
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().Destroy();
        }
    }
}
