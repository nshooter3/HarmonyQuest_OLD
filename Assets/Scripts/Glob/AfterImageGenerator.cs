using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour {

    void Start()
    {
        //StartAfterImage(0.1f);
    }

    public void StartAfterImage(float interval)
    {
        InvokeRepeating("SpawnTrailPart", 0, interval);
    }

    public void StopAfterImage()
    {
        CancelInvoke("SpawnTrailPart");
    }

    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPartRenderer.color = new Color(trailPartRenderer.color.r, trailPartRenderer.color.g, trailPartRenderer.color.b, 0.5f);
        trailPart.transform.position = transform.position;
        trailPart.transform.eulerAngles = transform.eulerAngles;
        trailPart.transform.localScale = transform.localScale;

        GlobalFunctions.instance.AdjustColorOverTime(trailPartRenderer.color, new Color(trailPartRenderer.color.r, trailPartRenderer.color.g, trailPartRenderer.color.b, 0), 0.5f, trailPartRenderer);
        Destroy(trailPart, 1f);
    }
}
