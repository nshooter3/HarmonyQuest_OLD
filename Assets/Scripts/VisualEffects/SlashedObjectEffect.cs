using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashedObjectEffect : MonoBehaviour {

    public SpriteRenderer SegmentA, SegmentB;

    float slope;

    public SpriteRenderer test;

    public static SlashedObjectEffect instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){
            InitSlashObject(test, Color.white, 1);
        }
	}

    //Used to initialize a slashed object effect
    public void InitSlashObject(SpriteRenderer obj, Color col, float colStrength, float breakSpeed = 0.1f)
    {
        //Init Slope
        slope = Random.Range(-3.0f, 3.0f);
        if (slope == 0)
        {
            //workaround to prevent division by zero
            slope = 0.001f;
        }

        //Init SegmentA
        GlobalFunctions.instance.CopyTranform(SegmentA.transform, obj.transform);
        SegmentA.sprite = obj.sprite;
        SegmentA.color = obj.color;
        SegmentA.material.SetColor("_MultColor", col);
        SegmentA.material.SetFloat("_MultStrength", colStrength);
        SegmentA.material.SetFloat("_Slope", slope);

        //Init SegmentB
        GlobalFunctions.instance.CopyTranform(SegmentB.transform, obj.transform);
        SegmentB.sprite = obj.sprite;
        SegmentB.color = obj.color;
        SegmentB.material.SetColor("_MultColor", col);
        SegmentB.material.SetFloat("_MultStrength", colStrength);
        SegmentB.material.SetFloat("_Slope", slope);

        //Init segment movement
        if (slope <= 0)
        {
            SegmentB.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * breakSpeed;
            SegmentA.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * -breakSpeed;
        }
        else
        {
            SegmentB.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * -breakSpeed;
            SegmentA.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * breakSpeed;
        }

        obj.enabled = false;
    }
}
