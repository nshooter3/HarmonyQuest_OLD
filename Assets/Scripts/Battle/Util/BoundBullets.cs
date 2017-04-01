using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBullets : MonoBehaviour {

    //Used to reset bullets that leave the game area.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col) {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && col.gameObject.GetComponent<Bullet>().friendly)
        {
            col.gameObject.GetComponent<Bullet>().Reset();
        }
    }
}
