using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReticule : MonoBehaviour {

    SpriteRenderer sr;

    float rotationSpeed = 200, scalingSpeed = 5, scalingRatio = 0.1f, colorSpeed = 10;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        StartTracking(PlayerMovementBattle.instance.transform, 0.1f, 10);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void StartTracking(Transform target, float speed, float duration)
    {
        StartCoroutine(Track(target, speed, duration));
    }

    public IEnumerator Track(Transform target, float speed, float duration)
    {
        Vector3 initScale = transform.localScale;
        for (float t = duration; t > 0; t -= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
            transform.localScale = new Vector3(initScale.x + scalingRatio * Mathf.Sin((Time.time) * scalingSpeed), initScale.y + scalingRatio * Mathf.Sin((Time.time) * scalingSpeed), initScale.z);
            sr.material.SetFloat("_MultStrength", Mathf.Lerp(0, 1, 0.5f + Mathf.Sin((Time.time) * colorSpeed) /2.0f));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator StopTracking(float duration)
    {
        yield return null;
    }
}
