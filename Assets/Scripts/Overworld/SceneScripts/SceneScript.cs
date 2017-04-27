using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class SceneScript : MonoBehaviour {

    public abstract void UpdateScene();
    public abstract void StartScene();

    //Actions to be carried out one at a time, in order
    public Queue<Action> actionQueue;
    Action action;

    //Determines whether or not the object is ready for a new command
    public bool commandInProgress;

    public static SceneScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        actionQueue = new Queue<Action>();
    }

    public void UpdateQueue()
    {
        Debug.Log("buh");
        if (!commandInProgress)
        {
            if (actionQueue.Count > 0)
            {
                action = actionQueue.Dequeue();
                action();
            }
        }
    }

}
