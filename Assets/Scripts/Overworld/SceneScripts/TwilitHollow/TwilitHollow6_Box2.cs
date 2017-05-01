using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow6_Box2 : SceneScript
{

    public GameObject dad, box1, box2, box3;

    // Use this for initialization
    public override void StartScene()
    {
        /*
        actionQueue.Enqueue(() => Wait(2f));
        actionQueue.Enqueue(() => CamTimer.instance.SetTimer(10, true, 
            () => CamTimer.instance.StartTimer(() => CamTimer.instance.TimerFlash(1.5f,3, 
            () => CamTimer.instance.ToggleVisibility(false)))));
            */
    }

    // Update is called once per frame
    void Update()
    {
        UpdateQueue();
    }

    public override void UpdateScene()
    {

    }
}