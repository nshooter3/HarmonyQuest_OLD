using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyMask : MonoBehaviour {

    public Material[] mat;

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
        foreach (Material m in mat)
        {
            m.SetFloat("_MinX", transform.position.x - transform.localScale.x / 2f);
            m.SetFloat("_MaxX", transform.position.x + transform.localScale.x / 2f);
            m.SetFloat("_MinY", transform.position.y - transform.localScale.y / 2f);
            m.SetFloat("_MaxY", transform.position.y + transform.localScale.y / 2f);
        }
        transform.hasChanged = false;
    }
}
