using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour {

    private float fuse = 1.0f;
    public bool active = false;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        transform.parent = null;
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        active = true;
        sr.enabled = true;
        print("bomb set!");
        //animation 1
        yield return new WaitForSeconds(fuse / 2.0f);
        //animation 2
        yield return new WaitForSeconds(fuse / 2.0f);
        print("bomb explode!");
        //show explosion then disable
        PlayerMovementBattle.instance.bombCount--;
        active = false;
        sr.enabled = false;
    }
}
