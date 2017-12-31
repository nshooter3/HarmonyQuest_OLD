using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public static Bomb instance;

    public Explosion explosion;
    SpriteRenderer sr;
    public bool planted = false, idle = true, boom = true;
    public float staminaCost = 50;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Plant(Vector3 pos)
    {
        idle = false;
        planted = true;
        transform.position = pos;
        sr.enabled = true;
    }

    public void Boom()
    {
        explosion.transform.position = transform.position;
        explosion.StartExplosion();
        planted = false;
        boom = true;
        sr.enabled = false;
    }
}
