using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyMask : MonoBehaviour {

    public Material mat;

	// Use this for initialization
	void Start () {
        UpdateMatParams();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.hasChanged)
        {
            UpdateMatParams();
        }
	}

    void UpdateMatParams()
    {
        Debug.Log("Update mat params");
        mat.SetFloat("_MinX", transform.position.x - transform.localScale.x / 2f);
        mat.SetFloat("_MaxX", transform.position.x + transform.localScale.x / 2f);
        mat.SetFloat("_MinY", transform.position.y - transform.localScale.y / 2f);
        mat.SetFloat("_MaxY", transform.position.y + transform.localScale.y / 2f);
        transform.hasChanged = false;
    }
}
