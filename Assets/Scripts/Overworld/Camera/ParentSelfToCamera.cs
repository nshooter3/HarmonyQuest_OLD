using UnityEngine;
using System.Collections;

public class ParentSelfToCamera : MonoBehaviour {

    public Camera cam;

    // Use this for initialization
    void Start () {
        cam = FindObjectOfType<Camera>();
        if (cam != null)
        {
            transform.parent = cam.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
