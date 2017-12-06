using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : DynamicCamera
{

    public static BattleCam instance;

    public GameObject target;

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
            //ZoomCamera(4.5f, 2f, target.transform.position, 0.001f);
        }
    }
}
