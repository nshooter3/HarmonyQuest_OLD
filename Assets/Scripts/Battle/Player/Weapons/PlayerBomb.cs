using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour {

    public float fuse = 2.0f;
    public bool active = false;

    public void Init(Vector3 position)
    {
        transform.position = position;
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        active = true;
        print("bomb set!");
        //animation 1
        yield return new WaitForSeconds(fuse / 2.0f);
        //animation 2
        yield return new WaitForSeconds(fuse / 2.0f);
        print("bomb explode!");
        //show explosion then disable
        PlayerMovementBattle.instance.bombCount--;
        active = false;
    }
}
