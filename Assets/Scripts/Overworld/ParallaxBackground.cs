using UnityEngine;
using System.Collections;

public class ParallaxBackground : MonoBehaviour {

    public GameObject boundHolder;
    public BoxCollider2D cameraBounds;
    Vector3 boundCenter;

    public Camera cam;

    float xMin, xMax, yMin, yMax;
    public float camRatioX = 1, camRatioY;
    float halfWidth, halfHeight;

    public bool doStuff = false;

    // Use this for initialization
    void Start () {

        cam = FindObjectOfType<Camera>();
        boundHolder = GameObject.FindGameObjectWithTag("CameraBounds");

        if (boundHolder != null)
        {
            boundCenter = boundHolder.transform.position;
            cameraBounds = boundHolder.GetComponent<BoxCollider2D>();
            if (cameraBounds != null)
            {
                if (cam != null)
                {
                    transform.parent = cam.transform;
                    doStuff = true;
                    xMax = boundHolder.transform.position.x + cameraBounds.size.x / 2f;
                    xMin = boundHolder.transform.position.x - cameraBounds.size.x / 2f;
                    yMax = boundHolder.transform.position.y + cameraBounds.size.y / 2f;
                    yMin = boundHolder.transform.position.y - cameraBounds.size.y / 2f;
                    halfWidth = cameraBounds.size.x / 2f;
                    halfHeight = cameraBounds.size.y / 2f;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateParallax();
	}

    void UpdateParallax()
    {
        int xDir = 1, yDir = 1;
        float xDif = 0, yDif = 0;
        Vector3 pos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        if (doStuff)
        {
            xDif = transform.position.x - boundCenter.x;
            if (xDif > 0)
            {
                xDir = -1;
            }
            yDif = transform.position.y - boundCenter.y;
            if (yDif > 0)
            {
                yDir = -1;
            }
            pos.x = xDir * ((Mathf.Abs(xDif) / halfWidth) * camRatioX);
            pos.y = yDir * ((Mathf.Abs(yDif) / halfHeight) * camRatioY);
        }
        transform.localPosition = pos;
    }
}
