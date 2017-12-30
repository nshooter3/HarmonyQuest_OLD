using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortraitDamager : MonoBehaviour {

    float offset = 0, maxOffset = 0.25f;
    Vector3 initPos, hitPos, initScale, hitScale;
    Color initCol, hitCol;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        initPos = new Vector3(0,0,0);
        hitPos = new Vector3(0.25f, 0, 0);
        initScale = transform.localScale;
        hitScale = initScale * 1.025f;
        initCol = sr.color;
        hitCol = Color.red;
    }

    public void TakeHit()
    {
        offset = maxOffset;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(hitPos, initPos, 1.0f - offset / maxOffset);
        transform.localScale = Vector3.Lerp(hitScale, initScale, 1.0f - offset / maxOffset);
        sr.color = Color.Lerp(hitCol, initCol, 1.0f - offset / maxOffset);

        if (offset > 0)
        {
            offset -= Time.deltaTime;
            if (offset <= 0)
            {
                offset = 0;
            }
        }
    }
}
