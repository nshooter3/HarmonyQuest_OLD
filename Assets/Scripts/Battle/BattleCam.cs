using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : DynamicCamera
{

    public static BattleCam instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ZoomCameraSmooth(3f, 1f);
        }
    }
}
