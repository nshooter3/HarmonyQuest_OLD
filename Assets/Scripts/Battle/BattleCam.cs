using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : MonoBehaviour {

    public static BattleCam instance;

    Vector3 startPos;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {

    }

    //Single camera shake
    public void GenericCamShake()
    {
        StartCoroutine(Shake(Random.Range(0.3f, 0.4f), Random.Range(1f, 1.5f)));
    }

    //Single camera shake with parameters
    public void SpecificCamShake(float mag, float shakeTimer) {
        StartCoroutine(Shake(mag, shakeTimer));
    }

    IEnumerator Shake(float mag, float shakeTimer)
    {
        Vector3 tempDir = new Vector3(Random.Range(-1f, 1f) * mag, Random.Range(-1f, 1f) * mag, startPos.z);
        for (var f = shakeTimer; f >= 0; f -= Time.deltaTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startPos, tempDir, f / shakeTimer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.transform.localPosition = startPos;
    }

    //Continuous camera shake with parameter ranges
    public void RepeatingCameraShake(float duration, float upperMag = 0.4f, float lowerMag = 0.2f, float upperShakeTimer = 0.02f, float lowerShakeTimer = 0.08f)
    {
        StartCoroutine(RepeatingCameraShakeCo(duration, upperMag, lowerMag, upperShakeTimer, lowerShakeTimer));
    }

    IEnumerator RepeatingCameraShakeCo(float duration, float upperMag, float lowerMag, float upperShakeTimer, float lowerShakeTimer)
    {
        float shakeTimer, mag;
        while (duration > 0)
        {
            if (duration >= lowerShakeTimer)
            {
                if (duration >= upperShakeTimer)
                {
                    shakeTimer = Random.Range(upperShakeTimer, lowerShakeTimer);
                }
                else
                {
                    shakeTimer = duration;
                }
                mag = Random.Range(upperMag, lowerMag);
                StartCoroutine(Shake(mag, shakeTimer));
                yield return new WaitForSeconds(shakeTimer);
                duration -= shakeTimer;
            }
            else
            {
                duration = -1;
            }
        }
    }
}
