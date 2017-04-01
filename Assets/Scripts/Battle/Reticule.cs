using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour {

    SpriteRenderer sr;
    LineRenderer lr;
    Color initCol;
    Vector4 col;

    public bool active = false;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        initCol = sr.color;
        col = initCol;
        col.w = 0;
        sr.enabled = false;
        lr.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (lr.enabled)
        {
            lr.SetPosition(1, PlayerMovementBattle.instance.transform.position);
        }
	}

    public void Trigger()
    {
        active = true;
        StartCoroutine(ReticuleAction());
    }

    IEnumerator ReticuleAction()
    {
        lr.enabled = true;
        sr.enabled = true;
        float maxTime = 0.25f;
        Vector3 rot = new Vector3(0, 0, 90);
        for (float f = maxTime; f > 0; f -= Time.deltaTime)
        {
            sr.color = Color.Lerp(col, initCol, f / maxTime);
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, rot, f / maxTime);
            yield return new WaitForSeconds(Time.deltaTime);
            if (f < maxTime / 2 && lr.enabled)
            {
                lr.enabled = false;
            }
        }
        active = false;
    }
}
