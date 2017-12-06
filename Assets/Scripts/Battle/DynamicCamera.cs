using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour {

    Vector3 startPos;
    Camera cam;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        cam = GetComponent<Camera>();
    }

    //Single camera shake
    public void GenericCamShake()
    {
        StartCoroutine(Shake(Random.Range(0.3f, 0.4f), Random.Range(1f, 1.5f)));
    }

    //Single camera shake with parameters
    public void SpecificCamShake(float mag, float shakeTimer)
    {
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

    //Zooms in/out camera over time
    public void ZoomCamera(float newSize, float duration, Vector3 target, float targetSpeed = 0)
    {
        StartCoroutine(ZoomCameraCo(newSize, duration, target, targetSpeed));
    }

    IEnumerator ZoomCameraCo(float newSize, float duration, Vector2 target, float targetSpeed = 0)
    {
        float startSize = cam.orthographicSize;
        for (var f = duration; f >= 0; f -= Time.deltaTime)
        {
            cam.orthographicSize = Mathf.Lerp(newSize, startSize, f / duration);
            if (target != null)
            {
                cam.transform.position = Vector3.Lerp(transform.position, new Vector3(target.x, target.y, transform.position.z), targetSpeed);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cam.orthographicSize = newSize;
    }

    //Zooms in/out camera over time smoothly
    public void ZoomCameraSmooth(float newSize, float duration, Vector2 target, float targetSpeed = 0)
    {
        StartCoroutine(ZoomCameraSmoothCo(newSize, duration, target, targetSpeed));
    }

    IEnumerator ZoomCameraSmoothCo(float newSize, float duration, Vector2 target, float targetSpeed = 0)
    {
        float startSize = cam.orthographicSize;
        for (var f = duration; f >= 0; f -= Time.deltaTime)
        {
            cam.orthographicSize = Mathf.SmoothStep(newSize, startSize, f / duration);
            if (target != null)
            {
                cam.transform.position = Vector3.Lerp(transform.position, new Vector3(target.x, target.y, transform.position.z), targetSpeed);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cam.orthographicSize = newSize;
    }

    public void RotateCamera(Vector3 direction, float rotSpeed, float duration, float adjustSpeed = 0)
    {
        StartCoroutine(RotateCameraCo(direction, rotSpeed, duration, adjustSpeed = 0));
    }

    //Rotates camera towards a direction over time for a duration. Optional adjustSpeed parameter can be used to increase/decrease rotspeed by a value every frame
    IEnumerator RotateCameraCo(Vector3 direction, float rotSpeed, float duration, float adjustSpeed = 0)
    {
        for (float t = duration; t >= 0; t -= Time.deltaTime)
        {
            cam.transform.Rotate(direction * Time.deltaTime * rotSpeed);
            if (adjustSpeed != 0)
            {
                rotSpeed += adjustSpeed * Time.deltaTime;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
