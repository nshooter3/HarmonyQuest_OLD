using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {
        prefab = Instantiate(prefab, transform.parent);
        prefab.transform.position = transform.position;
        prefab.transform.eulerAngles = transform.eulerAngles;
        prefab.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
