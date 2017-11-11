using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour {

    void Start()
    {
        //StartAfterImage(0.5f);
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
        AfterImagePool.instance.SpawnShrinkingAfterImage(transform, 0.2f);
    }
}
