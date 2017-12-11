using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : DynamicCamera
{

    public static BattleCam instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void IntroZoom()
    {
        StartCoroutine(IntroZoomCo());
    }

    IEnumerator IntroZoomCo()
    {
        yield return new WaitForSeconds(beat*4);
        ZoomCameraSmooth(4.5f, 0.25f);
        yield return new WaitForSeconds(beat);
        ZoomCameraSmooth(4.0f, 0.25f);
        yield return new WaitForSeconds(beat);
        ZoomCameraSmooth(3.5f, 0.25f);
        yield return new WaitForSeconds(beat);
        ZoomCameraSmooth(5.0f, 0.5f);
    }
}
