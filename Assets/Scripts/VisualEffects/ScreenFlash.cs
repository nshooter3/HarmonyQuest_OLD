using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour
{

    SpriteRenderer sr;

    public static ScreenFlash instance;

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

    public void Flash(Color srCol, float time)
    {
        StartCoroutine(ScreenFlashCo(srCol, time));
    }

    IEnumerator ScreenFlashCo(Color srCol, float time)
    {
        sr.enabled = true;
        sr.material.SetColor("_MultColor", Color.white);
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            sr.color = Color.Lerp(GlobalFunctions.instance.ColorMinAlpha(srCol), srCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.enabled = false;
        sr.color = GlobalFunctions.instance.ColorMaxAlpha(srCol);
    }
}
