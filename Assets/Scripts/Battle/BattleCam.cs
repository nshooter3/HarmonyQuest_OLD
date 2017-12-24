using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : DynamicCamera
{

    public static BattleCam instance;

    public Camera[] subCams;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        foreach (Camera sCam in subCams)
        {
            sCam.orthographicSize = cam.orthographicSize;
        }
    }

    public void IntroZoom()
    {
        StartCoroutine(IntroZoomCo());
    }

    IEnumerator IntroZoomCo()
    {
        cam.orthographicSize = 4.75f;
        ZoomCameraSmooth(5.0f, beat * 2.0f);
        GlobalFunctions.instance.AdjustRotOverTimeSmooth(new Vector3(0, 0, -2.5f), new Vector3(0, 0, 0), beat * 4.0f, transform);
        BattleScreenDimmer.instance.FadeTo(beat, 1.0f, 0.5f);
        yield return new WaitForSeconds(beat * 2);
        BattleSceneManager.instance.FadeInCharacters();
        yield return new WaitForSeconds(beat * 2);
        ZoomCameraSmooth(4.5f, 0.25f);
        GlobalFunctions.instance.AdjustRotOverTimeSmooth(new Vector3(0, 0, 0), new Vector3(0, 0, 2.5f), beat/2.0f, transform);
        yield return new WaitForSeconds(beat);
        ZoomCameraSmooth(4.0f, 0.25f);
        GlobalFunctions.instance.AdjustRotOverTimeSmooth(new Vector3(0, 0, 2.5f), new Vector3(0, 0, -5.0f), beat / 2.0f, transform);
        yield return new WaitForSeconds(beat);
        ZoomCameraSmooth(3.5f, 0.25f);
        GlobalFunctions.instance.AdjustRotOverTimeSmooth(new Vector3(0, 0, -5.0f), new Vector3(0, 0, 7.5f), beat / 2.0f, transform);
        yield return new WaitForSeconds(beat);
        GlobalFunctions.instance.AdjustRotOverTimeSmooth(new Vector3(0, 0, 7.5f), new Vector3(0, 0, 0), 0.5f, transform);
        ZoomCameraSmooth(5.0f, 0.5f);
    }
}
