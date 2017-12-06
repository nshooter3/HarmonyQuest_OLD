using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWhiteout : MonoBehaviour
{

    public GameObject slashedObject;
    SpriteRenderer sr;

    public static ScreenWhiteout instance;

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
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //InitSlashSequence();
        }
    }

    //Start post battle slash sequence
    public void InitSlashSequence()
    {
        sr.enabled = true;
        sr.material.SetColor("_MultColor", Color.black);
        SlashedObjectEffect.instance.InitSlashObject(slashedObject.GetComponent<SpriteRenderer>(), Color.white, 1);
        StartCoroutine(DelayedColorChangeCo(Color.white, Color.black, 0.05f));
        StartCoroutine(DelayedColorChangeCo(Color.black, Color.white, 1.95f));
        StartCoroutine(DelayedColorChangeCo(Color.white, Color.black, 2.00f));
        StartCoroutine(DelayedColorChangeCo(Color.black, Color.white, 2.05f));
        StartCoroutine(DelayedColorChangeCo(Color.white, Color.black, 2.10f));
        StartCoroutine(DelayedColorChangeCo(Color.black, Color.white, 2.15f));
        StartCoroutine(DelayedColorChangeCo(Color.white, Color.black, 2.20f));
        StartCoroutine(DelayedColorChangeCo(Color.black, Color.white, 2.25f));
        StartCoroutine(DelayedColorChangeCo(Color.white, Color.black, 2.30f));
        StartCoroutine(DelayedColorChangeCo(Color.black, Color.white, 2.35f));
        StartCoroutine(DelayedColorChangeCo(Color.white, Color.black, 2.40f));
    }

    //Used to change colors after a brief delay
    IEnumerator DelayedColorChangeCo(Color srCol, Color slashedCol, float delay)
    {
        yield return new WaitForSeconds(delay);
        sr.material.SetColor("_MultColor", srCol);
        SlashedObjectEffect.instance.ChangeSegmentColor(slashedCol);
    }
}
