using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashedObjectEffect : MonoBehaviour
{

    public SpriteRenderer SegmentA, SegmentB;
    public MeshRenderer ShatterRenderer;
    public Material shatterMat;
    public SprayParticles spray;
    public ParticleSystem explodeA, explodeB;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        spray.InitAbsorbParticles(1.0f);
        StartCoroutine(RandomAfterImages(1.0f, 0.75f));


        //Init SegmentA
        GlobalFunctions.instance.CopyTranform(SegmentA.transform, obj.transform);
        SegmentA.transform.position = new Vector3(SegmentA.transform.position.x, SegmentA.transform.position.y, -9.6f);
        SegmentA.sprite = obj.sprite;
        SegmentA.color = obj.color;
        SegmentA.material.SetColor("_MultColor", col);
        SegmentA.material.SetFloat("_MultStrength", colStrength);
        SegmentA.material.SetFloat("_Slope", slope);
        spray.absorbA.transform.parent = SegmentA.transform;
        spray.absorbA.transform.localPosition = new Vector3(0, 0, spray.absorbA.transform.localPosition.z);
        //StartCoroutine(DelayedExplosion(SegmentA.GetComponent<Explodable>(), 2.0f));

        //Init SegmentB
        GlobalFunctions.instance.CopyTranform(SegmentB.transform, obj.transform);
        SegmentB.transform.position = new Vector3(SegmentB.transform.position.x, SegmentB.transform.position.y, -9.6f);
        SegmentB.sprite = obj.sprite;
        SegmentB.color = obj.color;
        SegmentB.material.SetColor("_MultColor", col);
        SegmentB.material.SetFloat("_MultStrength", colStrength);
        SegmentB.material.SetFloat("_Slope", slope);
        spray.absorbB.transform.parent = SegmentB.transform;
        spray.absorbB.transform.localPosition = new Vector3(0, 0, spray.absorbA.transform.localPosition.z);
        //StartCoroutine(DelayedExplosion(SegmentB.GetComponent<Explodable>(), 2.0f));

        //Init segment movement
        if (slope <= 0)
        {
            SegmentA.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * -breakSpeed;
            SegmentB.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * breakSpeed;
        }
        else
        {
            SegmentA.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * breakSpeed;
            SegmentB.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, -1.0f / slope).normalized * -breakSpeed;
        }

        //StartCoroutine(AfterImage(1.75f));
        StartCoroutine(DelayedExplosion(ShatterRenderer.GetComponent<Explodable>(), 2.0f));

        obj.enabled = false;
    }

    IEnumerator AfterImage(float delay)
    {
        yield return new WaitForSeconds(delay);
        AfterImagePool.instance.SpawnShrinkingAfterImage(SegmentA.transform, 0.2f);
        AfterImagePool.instance.SpawnShrinkingAfterImage(SegmentB.transform, 0.2f);
    }

    //Spawn random, flickering after images
    IEnumerator RandomAfterImages(float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        float time = duration;
        float randomDelay = Random.Range(.1f, .4f);
        float fadeTime, xOff, yOff;

        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            randomDelay -= Time.deltaTime;
            if (randomDelay <= 0 && t > 0.2f)
            {
                fadeTime = Random.Range(0.1f, 0.05f);
                xOff = Random.Range(0.3f, -0.3f);
                yOff = Random.Range(0.3f, -0.3f);
                AfterImagePool.instance.SpawnAfterImage(SegmentA.transform, fadeTime, xOff, yOff);
                AfterImagePool.instance.SpawnAfterImage(SegmentB.transform, fadeTime, xOff, yOff);
                randomDelay = Random.Range(.1f, .2f);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator DelayedExplosion(Explodable exp, float delay)
    {
        shatterMat.SetColor("_TintColor", new Color(1, 1, 1, 1));
        yield return new WaitForSeconds(delay);
        AfterImagePool.instance.SpawnGrowingAfterImage(SegmentA.transform, 0.5f);
        AfterImagePool.instance.SpawnGrowingAfterImage(SegmentB.transform, 0.5f);
        //explode.transform.position = new Vector3(SegmentA.transform.position.x, SegmentA.transform.position.y, SegmentA.transform.position.z -1);
        explodeA.transform.position = SegmentA.transform.position;
        explodeA.Play();
        explodeB.transform.position = SegmentB.transform.position;
        explodeB.Play();
        exp.explode();
        exp.GetComponent<MeshRenderer>().enabled = false;
        GlobalFunctions.instance.AdjustColorOverTimeMaterial(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 1.5f, shatterMat);
    }
}
