using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashedObjectEffect : MonoBehaviour {

    public SpriteRenderer SegmentA, SegmentB;
    public MeshRenderer ShatterRenderer;
    public Material shatterMat;
    public SprayParticles spray;

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
            //InitSlashObject(test, Color.white, 1);
        }
	}

    //Used to override current segment color
    public void ChangeSegmentColor(Color col)
    {
        SegmentA.material.SetColor("_MultColor", col);
        SegmentB.material.SetColor("_MultColor", col);
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

        //Init delayed particle effect
        spray.InitParticles(obj.transform, slope, 0.05f);

        //Init SegmentA
        GlobalFunctions.instance.CopyTranform(SegmentA.transform, obj.transform);
        SegmentA.transform.position = new Vector3(SegmentA.transform.position.x, SegmentA.transform.position.y, -9.6f);
        SegmentA.sprite = obj.sprite;
        SegmentA.color = obj.color;
        SegmentA.material.SetColor("_MultColor", col);
        SegmentA.material.SetFloat("_MultStrength", colStrength);
        SegmentA.material.SetFloat("_Slope", slope);
        //StartCoroutine(DelayedExplosion(SegmentA.GetComponent<Explodable>(), 2.0f));

        //Init SegmentB
        GlobalFunctions.instance.CopyTranform(SegmentB.transform, obj.transform);
        SegmentB.transform.position = new Vector3(SegmentB.transform.position.x, SegmentB.transform.position.y, -9.6f);
        SegmentB.sprite = obj.sprite;
        SegmentB.color = obj.color;
        SegmentB.material.SetColor("_MultColor", col);
        SegmentB.material.SetFloat("_MultStrength", colStrength);
        SegmentB.material.SetFloat("_Slope", slope);
        //StartCoroutine(DelayedExplosion(SegmentB.GetComponent<Explodable>(), 2.0f));

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

        StartCoroutine(DelayedExplosion(ShatterRenderer.GetComponent<Explodable>(), 2.0f));

        obj.enabled = false;
    }

    IEnumerator DelayedExplosion(Explodable exp, float delay)
    {
        shatterMat.SetColor("_TintColor", new Color(1, 1, 1, 1));
        yield return new WaitForSeconds(delay);
        exp.explode();
        exp.GetComponent<MeshRenderer>().enabled = false;
        GlobalFunctions.instance.AdjustColorOverTimeMaterial(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1.5f, shatterMat);
    }
}
