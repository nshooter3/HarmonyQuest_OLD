using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReticule : MonoBehaviour {

    SpriteRenderer sr;

    float rotationSpeed = 200, scalingSpeed = 5, scalingRatio = 0.1f, colorSpeed = 10;

    Vector3 initScale;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        initScale = Vector3.one;
        //StartTracking(PlayerMovementBattle.instance.transform, 0.1f, 2);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){
            //StartTracking(PlayerMovementBattle.instance.transform, 0.1f, 2);
        }
	}

    public void StartTracking(Transform target, float speed, float duration)
    {
        StartCoroutine(Track(target, speed, duration));
    }

    public IEnumerator Track(Transform target, float speed, float duration)
    {
        initScale = transform.localScale;
        float afterImageTimer = 0;
        for (float t = duration; t > 0; t -= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
            transform.localScale = new Vector3(initScale.x + scalingRatio * Mathf.Sin((Time.time) * scalingSpeed), initScale.y + scalingRatio * Mathf.Sin((Time.time) * scalingSpeed), initScale.z);
            sr.material.SetFloat("_MultStrength", Mathf.Lerp(0, 1, 0.5f + Mathf.Sin((Time.time) * colorSpeed) /2.0f));
            afterImageTimer += Time.deltaTime;
            if (afterImageTimer > 0.025f)
            {
                AfterImagePool.instance.SpawnAfterImage(transform, 0.05f);
                afterImageTimer = 0;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StartCoroutine(StopTracking(0.2f));
    }

    public IEnumerator StopTracking(float duration)
    {
        GlobalFunctions.instance.AdjustSizeOverTime(initScale * 2.5f, initScale, duration/2.0f, transform);
        float afterImageTimer = 0;
        for (float t = duration; t > 0; t -= Time.deltaTime)
        {
            sr.material.SetFloat("_MultStrength", Mathf.Lerp(0, 1, 0.5f + Mathf.Sin((Time.time) * colorSpeed*10) / 2.0f));
            afterImageTimer += Time.deltaTime;
            if (afterImageTimer > 0.05f)
            {
                AfterImagePool.instance.SpawnGrowingAfterImage(transform, 0.2f);
                afterImageTimer = 0;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.enabled = false;
    }
}
