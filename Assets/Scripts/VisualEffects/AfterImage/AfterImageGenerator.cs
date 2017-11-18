using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour {

    void Start()
    {
        //StartAfterImage(1.5f);
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
        AfterImagePool.instance.SpawnDBZAfterImage(transform, 1f, PlayerMovementBattle.instance.transform);
    }
}
