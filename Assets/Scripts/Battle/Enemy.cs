using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float maxhealth, health, damage;
    Vector3 dir;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Color initCol, hitCol;
    float hitTimer = 0, maxHitTimer = 0.1f;

	// Use this for initialization
	void Start () {
        dir = new Vector3(1, -1, 0).normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir*2;
        sr = GetComponentInChildren<SpriteRenderer>();
        initCol = sr.color;
        hitCol = Color.red;
        health = maxhealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && col.gameObject.GetComponent<Bullet>().friendly)
        {
            Bullet temp = col.gameObject.GetComponent<Bullet>();
            temp.Reset();
            health -= temp.damage;
            StartCoroutine(GetHit());
            if (health <= 0)
            {
                BattleUIHandler.instance.DecreaseEnemy(maxhealth);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator GetHit()
    {
        //sr.gameObject.transform.localPosition = Vector3.zero;
        Vector3 rot = new Vector3(0, 0, (Random.Range(0, 1) * 2 - 1)*2);
        //Vector3 tempDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * Random.Range(0.04f, 0.06f);
        hitTimer = maxHitTimer;
        for (var f = maxHitTimer; f >= 0; f -= Time.deltaTime)
        {
            sr.color = Color.Lerp(initCol, hitCol, f / maxHitTimer);
            //sr.gameObject.transform.localPosition = Vector3.Lerp(Vector3.zero, tempDir, f / maxHitTimer);
            sr.gameObject.transform.eulerAngles = Vector3.Lerp(Vector3.zero, rot, f / maxHitTimer);
            sr.gameObject.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*1.1f, f / maxHitTimer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.gameObject.transform.eulerAngles = Vector3.zero;
        sr.gameObject.transform.localScale = Vector3.one;
        sr.color = initCol;
        yield return null;
    }

}
