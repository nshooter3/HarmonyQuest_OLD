using UnityEngine;
using System.Collections;

public class Oscillator : MonoBehaviour {

    Vector3 initPos;
    public float xDis, yDis, speed;
    public bool isOffset = false;
    private float offset = 0;

	// Use this for initialization
	void Start () {
        initPos = gameObject.transform.localPosition;
        if (isOffset)
        {
            offset = Random.Range(1f, 10f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localPosition = new Vector3(initPos.x + xDis * Mathf.Sin((Time.time + offset) * speed), initPos.y + yDis * Mathf.Sin((Time.time + offset) * speed), initPos.z);
	}

}
