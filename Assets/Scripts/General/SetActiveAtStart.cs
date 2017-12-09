using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveAtStart : MonoBehaviour {

    //Used to enable a group of objects upon awake. Hopefully this will help clean up my dev view
    public GameObject[] objects;

    void Awake()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
